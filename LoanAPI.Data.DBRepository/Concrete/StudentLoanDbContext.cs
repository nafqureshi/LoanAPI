﻿using LoanAPI.Data.DBRepository.Entity;
using Microsoft.EntityFrameworkCore;

namespace LoanAPI.Data.DBRepository.Concrete
{
    public class StudentLoanDbContext : DbContext
    {
        public StudentLoanDbContext(DbContextOptions<StudentLoanDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>().HasKey(l => new { l.StudentId, l.InstitutionId });
        }

        public DbSet<Student> Student { get; set; }

        public DbSet<Institution> Institution { get; set; }

        public DbSet<Loan> Loan { get; set; }

        public void InsertLoanDetails(Student student, Institution institution, Loan loan)
        {
            Student.Add(student);
            Institution.Add(institution);
            SaveChanges();

            loan.StudentId = student.StudentId;
            loan.InstitutionId = institution.InstitutionId;
            Loan.Add(loan);

            SaveChanges();
        }

        public void UpdateLoanDetails(Student student, Institution institution, Loan loan)
        {
            Update(student);
            Update(institution);
            Update(loan);

            SaveChanges();
        }
    }
}
