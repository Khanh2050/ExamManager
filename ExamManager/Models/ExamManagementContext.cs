﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExamManager.Models
{
    public partial class ExamManagementContext : DbContext
    {
        public ExamManagementContext()
        {
        }

        public ExamManagementContext(DbContextOptions<ExamManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionAnswerMap> QuestionAnswerMaps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-69FDS3D\\SQLEXPRESS;Initial Catalog=ExamManagement;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");

                entity.Property(e => e.Id).HasMaxLength(20);

                entity.Property(e => e.Answer1)
                    .IsRequired()
                    .HasColumnName("Answer")
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(20);

                entity.Property(e => e.Question1)
                    .IsRequired()
                    .HasColumnName("Question")
                    .HasMaxLength(300);

                entity.Property(e => e.QuestionLevel)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<QuestionAnswerMap>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("QuestionAnswerMap");

                entity.HasIndex(e => new { e.QuestionId, e.AnswerId })
                    .HasName("Unique_QuestionAnswerMap")
                    .IsUnique();

                entity.Property(e => e.AnswerId)
                    .IsRequired()
                    .HasColumnName("AnswerID")
                    .HasMaxLength(20);

                entity.Property(e => e.QuestionId)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.QuestionAnswerMaps)
                    .HasForeignKey(d => d.AnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answer_QuestionAnswerMap1");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionAnswerMaps)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question_QuestionAnswerMap");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}