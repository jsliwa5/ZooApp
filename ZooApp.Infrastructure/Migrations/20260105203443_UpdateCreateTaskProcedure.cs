using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCreateTaskProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var procedureSql = @"
                CREATE OR REPLACE PROCEDURE sp_create_and_assign_task(
                    p_description TEXT,
                    p_duration INTERVAL,
                    p_task_type VARCHAR(50),
                    p_scheduled_at TIMESTAMPTZ,
                    p_animal_id INT DEFAULT NULL
                )
                LANGUAGE plpgsql
                AS $$
                DECLARE
                    v_zookeeper_id INT;
                    v_target_month INT;
                    v_target_year INT;
                BEGIN
                    -- Pobieramy miesiąc i rok z daty planowanego zadania
                    v_target_month := EXTRACT(MONTH FROM p_scheduled_at);
                    v_target_year := EXTRACT(YEAR FROM p_scheduled_at);

                    -- 1. SZUKANIE NAJLEPSZEGO PRACOWNIKA
                    -- Kryteria: 
                    -- a) Musi mieć wystarczająco dużo wolnych godzin w tym miesiącu.
                    -- b) Wybieramy tego, który ma NAJWIĘCEJ wolnych godzin (najmniej obciążonego).
                    
                    SELECT z.""Id"" INTO v_zookeeper_id
                    FROM ""ZooKeepers"" z
                    LEFT JOIN ""ZooKeeperTasks"" t 
                        ON z.""Id"" = t.""ZooKeeperId""
                        AND EXTRACT(MONTH FROM t.""ScheduledAt"") = v_target_month
                        AND EXTRACT(YEAR FROM t.""ScheduledAt"") = v_target_year
                    GROUP BY z.""Id"", z.""MonthlyHoursLimit""
                    -- Kluczowy warunek: Limit godzin (zamieniony na interval) minus suma zużytych godzin (lub 0) musi pomieścić nowe zadanie
                    HAVING (make_interval(hours => z.""MonthlyHoursLimit"") - COALESCE(SUM(t.""Duration""), '0 hours')) >= p_duration
                    
                    -- Sortujemy od tego, który ma najwięcej wolnego miejsca
                    ORDER BY (make_interval(hours => z.""MonthlyHoursLimit"") - COALESCE(SUM(t.""Duration""), '0 hours')) DESC
                    LIMIT 1;

                    -- Jeśli nie znaleziono nikogo (wszyscy przekroczyli limit lub zadanie jest za długie)
                    IF v_zookeeper_id IS NULL THEN
                        RAISE EXCEPTION 'Cannot schedule task. No ZooKeeper has enough available hours in %/% for this duration.', v_target_month, v_target_year;
                    END IF;

                    -- 2. INSERT (Bez zmian)
                    INSERT INTO ""ZooKeeperTasks"" (
                        ""Description"", ""Duration"", ""TaskType"", ""ZooKeeperId"", ""IsCompleted"", ""ScheduledAt"", ""TargetAnimalId""
                    )
                    VALUES (
                        p_description, p_duration, p_task_type, v_zookeeper_id, FALSE, p_scheduled_at, p_animal_id
                    );
                END;
                $$;";

            migrationBuilder.Sql(procedureSql);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_create_and_assign_task(TEXT, INTERVAL, VARCHAR, TIMESTAMPTZ, INT);");
        }
    }
}
