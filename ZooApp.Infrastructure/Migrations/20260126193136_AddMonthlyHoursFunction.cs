using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMonthlyHoursFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION fn_get_zookeeper_monthly_hours(
                    p_zookeeper_id INT,
                    p_month INT,
                    p_year INT
                )
                RETURNS DOUBLE PRECISION
                LANGUAGE plpgsql
                STABLE
                AS $$
                DECLARE
                    v_total_hours DOUBLE PRECISION;
                BEGIN
            
                    SELECT COALESCE(SUM(EXTRACT(EPOCH FROM ""Duration"") / 3600.0), 0)
                    INTO v_total_hours
                    FROM ""ZooKeeperTasks"" 
                    WHERE ""ZooKeeperId"" = p_zookeeper_id
                      AND EXTRACT(MONTH FROM ""ScheduledAt"") = p_month
                      AND EXTRACT(YEAR FROM ""ScheduledAt"") = p_year;

                    RETURN v_total_hours;
                END;
                $$;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS fn_get_zookeeper_monthly_hours(INT, INT, INT);");
        }
    }
}
