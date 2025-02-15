﻿using Microsoft.EntityFrameworkCore;
using ResumeManagementAPI.Models;

namespace ResumeManagementAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {}

        public DbSet<Company> Companies { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Candidate> Candidates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Job>()
                .HasOne(job => job.Company)
                .WithMany(company => company.Jobs)
                .HasForeignKey(job => job.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Candidate>()
                .HasOne(candidate=> candidate.Job)
                .WithMany(job=> job.Candidates)
                .HasForeignKey(candidate=> candidate.JobId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Job>()
                .Property(job => job.Level)
                .HasConversion<string>();

            modelBuilder.Entity<Company>()
              .Property(company => company.Size)
              .HasConversion<string>();
        }
    }
}
