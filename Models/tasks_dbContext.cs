using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TasksApi.Models
{
    public partial class tasks_dbContext : DbContext
    {
        public tasks_dbContext()
        {
        }

        public tasks_dbContext(DbContextOptions<tasks_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost; DataBase=tasks_db; Username=postgres; Password=password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("logins");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Login1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("tasks");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Enddate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("enddate");

                entity.Property(e => e.Ended).HasColumnName("ended");

                entity.Property(e => e.Theader)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("theader");

                entity.Property(e => e.Ttext)
                    .IsRequired()
                    .HasMaxLength(3000)
                    .HasColumnName("ttext");

                entity.Property(e => e.Userid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tasks_userid_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
