using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCreateTaskProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var procedureSql = @"
    CREATE OR REPLACE PROCEDURE sp_create_and_assign_task(
        p_description TEXT,
        p_duration INTERVAL,
        p_task_type VARCHAR(50),
        p_scheduled_at TIMESTAMPTZ,  -- <--- NOWY PARAMETR (Data wizyty)
        p_animal_id INT DEFAULT NULL -- <--- NOWY PARAMETR (Opcjonalne ID zwierzęcia)
    )
    LANGUAGE plpgsql
    AS $$
    DECLARE
        v_zookeeper_id INT;
    BEGIN
        -- 1. ZNAJDOWANIE NAJLEPSZEGO PRACOWNIKA
        -- Logika: najmniej zadań w ogóle. 
        -- (Można tu rozbudować SQL, żeby sprawdzał obciążenie w miesiącu z p_scheduled_at!)
        SELECT ""Id"" INTO v_zookeeper_id
        FROM ""ZooKeepers""
        ORDER BY (
            SELECT COUNT(*) 
            FROM ""ZooKeeperTasks"" 
            WHERE ""ZooKeeperId"" = ""ZooKeepers"".""Id""
        ) ASC
        LIMIT 1;

        IF v_zookeeper_id IS NULL THEN
            RAISE EXCEPTION 'No ZooKeepers available to assign the task.';
        END IF;

        -- 2. TWORZENIE ZADANIA
        INSERT INTO ""ZooKeeperTasks"" (
            ""Description"", 
            ""Duration"", 
            ""TaskType"", 
            ""ZooKeeperId"", 
            ""IsCompleted"", 
            ""ScheduledAt"",      -- <--- Wstawiamy do kolumny
            ""TargetAnimalId""    -- <--- Mapowanie kolumny dla AnimalId (EF Core domyślnie tak ją nazwie przy dziedziczeniu TPH)
        )
        VALUES (
            p_description, 
            p_duration, 
            p_task_type, 
            v_zookeeper_id, 
            FALSE, 
            p_scheduled_at,       -- <--- Używamy parametru
            p_animal_id           -- <--- NULL dla OtherTask, INT dla AnimalRelated
        );
    END;
    $$;";

            migrationBuilder.Sql(procedureSql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_create_and_assign_task(TEXT, INTERVAL, VARCHAR);");
        }
    }
}
