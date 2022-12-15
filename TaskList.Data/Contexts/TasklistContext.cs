using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskList.Data.Models;
using Task = TaskList.Data.Models.Task;

namespace TaskList.Data.Contexts;

public partial class TasklistContext : DbContext
{

    public TasklistContext()
    {
    }

    public TasklistContext(DbContextOptions<TasklistContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Folder> Folders { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseSqlServer(GetConnectionStringFromConfiguration());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Folder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Folders");

            entity.ToTable("folders");

            entity.HasIndex(e => e.FolderName, "UC_FolderName").IsUnique();

            entity.Property(e => e.FolderName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Tasks");

            entity.ToTable("tasks");

            entity.Property(e => e.DueDate).HasColumnType("date");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TaskName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Folder).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.FolderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tasks_Folders");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    private static string GetConnectionStringFromConfiguration()
    {
        string currentDirectory = Environment.CurrentDirectory;
        string configFileName = "appsettings.json";
        string fullPathToConfigFile = Path.Combine(currentDirectory, @"..\TaskList.Data", configFileName);

        IConfigurationRoot builder = new ConfigurationBuilder()
            .AddJsonFile(fullPathToConfigFile, optional: false)
            .Build();

        return builder.GetConnectionString("Project") ?? string.Empty;
    }
}
