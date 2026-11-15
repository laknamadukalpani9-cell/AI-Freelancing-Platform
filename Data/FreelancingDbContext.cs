using System;
using System.Collections.Generic;
using AIFreelancingPlatform.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace AIFreelancingPlatform.Data;

public partial class FreelancingDbContext : DbContext
{
    public FreelancingDbContext(DbContextOptions<FreelancingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AI_Service> AI_Services { get; set; }

    public virtual DbSet<AI_Skill> AI_Skills { get; set; }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Freelancer> Freelancers { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Project_Requirement> Project_Requirements { get; set; }

    public virtual DbSet<Proposal> Proposals { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<vw_FreelancerDetail> vw_FreelancerDetails { get; set; }

    public virtual DbSet<vw_ProjectDetail> vw_ProjectDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AI_Service>(entity =>
        {
            entity.HasKey(e => e.Service_ID).HasName("PK__AI_Servi__BD1A239C9CE45665");

            entity.ToTable("AI_Service");

            entity.HasIndex(e => e.Service_Name, "UQ__AI_Servi__D4BB225C750038F9").IsUnique();

            entity.HasIndex(e => e.Service_Name, "UQ__AI_Servi__D4BB225CF868C6A3").IsUnique();

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Service_Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AI_Skill>(entity =>
        {
            entity.HasKey(e => e.Skill_ID).HasName("PK__AI_Skill__B4A9E2B053FD2677");

            entity.ToTable("AI_Skill");

            entity.HasIndex(e => e.Skill_Name, "UQ__AI_Skill__8100EB551D41311C").IsUnique();

            entity.HasIndex(e => e.Skill_Name, "UQ__AI_Skill__8100EB55CD4280ED").IsUnique();

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Skill_Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.Assignment_ID).HasName("PK__Assignme__9E0E9F0F37D62D11");

            entity.ToTable("Assignment", tb => tb.HasTrigger("trg_UpdateProjectStatus"));

            entity.HasOne(d => d.Freelancer).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.Freelancer_ID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Assignmen__Freel__06CD04F7");

            entity.HasOne(d => d.Project).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.Project_ID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Assignmen__Proje__07C12930");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Client_ID).HasName("PK__Client__75A5D71868C41F3D");

            entity.ToTable("Client");

            entity.HasIndex(e => e.Email, "UQ__Client__A9D105348FAE2C62").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Client__A9D10534BA7D403F").IsUnique();

            entity.Property(e => e.Company_Name)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Freelancer>(entity =>
        {
            entity.HasKey(e => e.Freelancer_ID).HasName("PK__Freelanc__92E57CE0BF611B19");

            entity.ToTable("Freelancer");

            entity.HasIndex(e => e.Email, "UQ__Freelanc__A9D1053427D122AF").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Freelanc__A9D105342E4F8928").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasMany(d => d.Services).WithMany(p => p.Freelancers)
                .UsingEntity<Dictionary<string, object>>(
                    "Freelancer_Service",
                    r => r.HasOne<AI_Service>().WithMany()
                        .HasForeignKey("Service_ID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Freelance__Servi__09A971A2"),
                    l => l.HasOne<Freelancer>().WithMany()
                        .HasForeignKey("Freelancer_ID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Freelance__Freel__08B54D69"),
                    j =>
                    {
                        j.HasKey("Freelancer_ID", "Service_ID").HasName("PK__Freelanc__4934DED992698010");
                        j.ToTable("Freelancer_Service");
                    });

            entity.HasMany(d => d.Skills).WithMany(p => p.Freelancers)
                .UsingEntity<Dictionary<string, object>>(
                    "Freelancer_Skill",
                    r => r.HasOne<AI_Skill>().WithMany()
                        .HasForeignKey("Skill_ID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Freelance__Skill__0B91BA14"),
                    l => l.HasOne<Freelancer>().WithMany()
                        .HasForeignKey("Freelancer_ID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Freelance__Freel__0A9D95DB"),
                    j =>
                    {
                        j.HasKey("Freelancer_ID", "Skill_ID").HasName("PK__Freelanc__89AFE2CB8E084FB4");
                        j.ToTable("Freelancer_Skill");
                    });
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Payment_ID).HasName("PK__Payment__DA6C7FE1E2B57B20");

            entity.ToTable("Payment");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Payment_Status)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Assignment).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Assignment_ID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__Assignm__0C85DE4D");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Project_ID).HasName("PK__Project__1CB92FE3F031C147");

            entity.ToTable("Project");

            entity.Property(e => e.Budget).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Project_Name)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.Client).WithMany(p => p.Projects)
                .HasForeignKey(d => d.Client_ID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Project__Client___0D7A0286");
        });

        modelBuilder.Entity<Project_Requirement>(entity =>
        {
            entity.HasKey(e => e.Requirement_ID).HasName("PK__Project___C14945CFF0B0917B");

            entity.ToTable("Project_Requirement");

            entity.Property(e => e.Requirement_Details)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Project).WithMany(p => p.Project_Requirements)
                .HasForeignKey(d => d.Project_ID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Project_R__Proje__0E6E26BF");

            entity.HasOne(d => d.Skill).WithMany(p => p.Project_Requirements)
                .HasForeignKey(d => d.Skill_ID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Project_R__Skill__0F624AF8");
        });

        modelBuilder.Entity<Proposal>(entity =>
        {
            entity.HasKey(e => e.Proposal_ID).HasName("PK__Proposal__C9D0461F0A8CAFC5");

            entity.ToTable("Proposal");

            entity.Property(e => e.Proposal_Status)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue("Pending");
            entity.Property(e => e.Proposed_Amount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Freelancer).WithMany(p => p.Proposals)
                .HasForeignKey(d => d.Freelancer_ID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Proposal__Freela__10566F31");

            entity.HasOne(d => d.Project).WithMany(p => p.Proposals)
                .HasForeignKey(d => d.Project_ID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Proposal__Projec__114A936A");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Review_ID).HasName("PK__Review__F85DA7EB611D68F7");

            entity.ToTable("Review", tb => tb.HasTrigger("trg_CheckReviewRating"));

            entity.Property(e => e.Comment)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Assignment).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Assignment_ID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Review__Assignme__123EB7A3");
        });

        modelBuilder.Entity<vw_FreelancerDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_FreelancerDetails");

            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Freelancer_ID).ValueGeneratedOnAdd();
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<vw_ProjectDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_ProjectDetails");

            entity.Property(e => e.Budget).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Client_Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Project_Name)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
