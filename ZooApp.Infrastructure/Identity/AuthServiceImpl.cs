using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZooApp.Application.Auth;
using ZooApp.Domain.Managers;
using ZooApp.Domain.Vets;
using ZooApp.Domain.ZooKeeper;
using ZooApp.Infrastructure.Persistance;

namespace ZooApp.Infrastructure.Identity;

public class AuthServiceImpl : IAuthService
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IZooKeeperRepository _zooKeeperRepository;
    private readonly IVetRepository _vetRepository;
    private readonly IManagerRepository _managerRepository;
    private readonly ZooDbContext _context; 
    private readonly IConfiguration _configuration;

    public AuthServiceImpl(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, IZooKeeperRepository zooKeeperRepository, IVetRepository vetRepository, IManagerRepository managerRepository, ZooDbContext context, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _zooKeeperRepository = zooKeeperRepository;
        _vetRepository = vetRepository;
        _managerRepository = managerRepository;
        _context = context;
        _configuration = configuration;
    }

    public async Task RegisterZooKeeperAsync(string email, string password, string firstName, string lastName, int hoursLimit)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var user = new ApplicationUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded) throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            await AddToRoleAsync(user, "ZooKeeper");

            var zooKeeper = ZooKeeper.CreateNew(firstName, lastName, hoursLimit, user.Id);
            await _zooKeeperRepository.Save(zooKeeper);

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            throw new Exception("Invalid credentials");

        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email!),
            
        };
        foreach (var role in roles) claims.Add(new Claim(ClaimTypes.Role, role));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            //expires: DateTime.Now.AddDays(1),
            expires: DateTime.Now.AddMinutes(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task AddToRoleAsync(ApplicationUser user, string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
            await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));

        await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task RegisterVetAsync(string email, string password, string firstName, string lastName, int monthlyHoursLimit)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            await AddToRoleAsync(user, "Vet");

            var vet = Vet.CreateNew(firstName, lastName, monthlyHoursLimit, user.Id);
            await _vetRepository.SaveAsync(vet);

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task RegisterManagerAsync(string email, string password, string firstName, string lastName)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
           
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            await AddToRoleAsync(user, "Manager");

            var manager = Manager.CreateNew(firstName, lastName, user.Id);

            await _managerRepository.SaveAsync(manager);

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
