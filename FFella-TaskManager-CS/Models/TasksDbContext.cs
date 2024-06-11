using Microsoft.EntityFrameworkCore;  // Provide access to Entity Framework components

/**********************************************************************************************
 * Data Base Context used by Entity Framework to access data source
 **********************************************************************************************/

namespace FFella_TaskManager_CS.Models
{
    public partial class TasksDbContext : DbContext
    {
        public TasksDbContext()
        {
        }
        // Define constructror to take options passed when instatiated by application
        //        and pass those options to base (DbContext) cclass
        public TasksDbContext(DbContextOptions<TasksDbContext> options)
            : base(options)
        {
        }

        // Define property to represent the collection of data in the data source
        public virtual DbSet<Task> Tasks { get; set; }

        // Define table and columns to be accessed using Entity Framework
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("task");
                
                entity.Property(e => e.TaskId).HasColumnName("taskId");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.DueDate)
                    .HasColumnName("dueDate")
                    .HasColumnType("date");

                entity.Property(e => e.Iscomplete)
                    .HasColumnName("iscomplete")
                    .HasDefaultValueSql("((0))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
