
using InventorySystem.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;
namespace InventorySystem.Data;

public partial class InventoryDb : DbContext
{
    public InventoryDb()
    {
    }

    public InventoryDb(DbContextOptions<InventoryDb> options)
        : base(options)
    {
    }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Desktop> Desktops { get; set; }

    public virtual DbSet<Laptop> Laptops { get; set; }

    public virtual DbSet<LogsHistory> LogsHistories { get; set; }

    public virtual DbSet<Site> Sites { get; set; }

    public virtual DbSet<ViewModels.Type> Types { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<AppUsers> AppUsers{ get; set; }

    public virtual DbSet<Maintenance> Maintenances { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(e => e.AssetId).HasName("PK__Assets__434923527B32AE7A");

            entity.HasIndex(e => e.SerialNumber, "UQ__Assets__048A00086856E8DF").IsUnique();

            entity.Property(e => e.Brand).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ModelVersion).HasMaxLength(100);
            entity.Property(e => e.SerialNumber).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");


            entity.HasOne(d => d.Type).WithMany(p => p.Assets)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Assets__TypeId__4E88ABD4");
        });

        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__Assignme__32499E779B91476A");

            entity.Property(e => e.AssignedDate).HasColumnType("datetime");
            entity.Property(e => e.ReturnedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Asset).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Assignmen__Asset__5812160E");

            entity.HasOne(d => d.User).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Assignmen__UserI__59063A47");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BEDE4A8FBF5");

            entity.HasIndex(e => e.Name, "UQ__Departme__737584F6B358F3ED").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Desktop>(entity =>
        {
            entity.HasKey(e => e.AssetId).HasName("PK__Desktops__43492352A57D4EA2");

            entity.ToTable(tb => tb.HasTrigger("PreventDuplicateAsset_Desktops"));

            entity.Property(e => e.AssetId).ValueGeneratedNever();
            entity.Property(e => e.Cpu)
                .HasMaxLength(50)
                .HasColumnName("CPU");
            entity.Property(e => e.Gpu)
                .HasMaxLength(50)
                .HasColumnName("GPU");
            entity.Property(e => e.MacEthernet).HasMaxLength(50);
            entity.Property(e => e.MacWifi).HasMaxLength(50);
            entity.Property(e => e.Ram)
                .HasMaxLength(50)
                .HasColumnName("RAM");

            entity.HasOne(d => d.Asset).WithOne(p => p.Desktop)
                .HasForeignKey<Desktop>(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Desktops__AssetI__5535A963");
        });

        modelBuilder.Entity<Laptop>(entity =>
        {
            entity.HasKey(e => e.AssetId).HasName("PK__Laptops__43492352E8BB9645");

            entity.ToTable(tb => tb.HasTrigger("PreventDuplicateAsset_Laptops"));

            entity.Property(e => e.AssetId).ValueGeneratedNever();
            entity.Property(e => e.Cpu)
                .HasMaxLength(50)
                .HasColumnName("CPU");
            entity.Property(e => e.Gpu)
                .HasMaxLength(50)
                .HasColumnName("GPU");
            entity.Property(e => e.HardDisk).HasMaxLength(50);
            entity.Property(e => e.MacEthernet).HasMaxLength(50);
            entity.Property(e => e.MacWifi).HasMaxLength(50);
            entity.Property(e => e.Ram)
                .HasMaxLength(50)
                .HasColumnName("RAM");
            entity.Property(e => e.ScreenSize).HasMaxLength(50);

            entity.HasOne(d => d.Asset).WithOne(p => p.Laptop)
                .HasForeignKey<Laptop>(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Laptops__AssetId__52593CB8");
        });

        modelBuilder.Entity<LogsHistory>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__LogsHist__5E54864892641767");

            entity.ToTable("LogsHistory");

            entity.Property(e => e.ChangeDate)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Asset).WithMany(p => p.LogsHistories)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LogsHisto__Asset__5CD6CB2B");

         
        });

        modelBuilder.Entity<Site>(entity =>
        {
            entity.HasKey(e => e.SiteId).HasName("PK__Sites__B9DCB963051D94EB");

            entity.HasIndex(e => e.Name, "UQ__Sites__737584F650DF6B5D").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<InventorySystem.ViewModels.Type>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__Types__516F03B5868C8A62");

            entity.HasIndex(e => e.Name, "UQ__Types__737584F6BA0704D3").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C6FE17A3E");

            entity.HasIndex(e => e.Fingerprint, "UQ__Users__5000B8ECDC093F90").IsUnique();

            entity.HasIndex(e => e.Phone, "UQ__Users__5C7E359E8AFBFB2D").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053494A7C99C").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Fingerprint).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.JobTitle).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e=>e.LastName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValue("User");

            entity.HasOne(d => d.Department).WithMany(p => p.Users)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__Departmen__4316F928");

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__Users__ManagerId__44FF419A");

            entity.HasOne(d => d.Site).WithMany(p => p.Users)
                .HasForeignKey(d => d.SiteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__SiteId__440B1D61");
        });

        OnModelCreatingPartial(modelBuilder);
    }
 
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
