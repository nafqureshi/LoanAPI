using LoanAPI.Data.DBRepository.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Transactions;

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
            this.Attach(student).State = EntityState.Detached;
            this.Attach(institution).State = EntityState.Detached;

            loan.StudentId = student.StudentId;
            loan.InstitutionId = institution.InstitutionId;
            Loan.Add(loan);
            SaveChanges();
            this.Attach(loan).State = EntityState.Detached;
        }

        public void UpdateLoanDetails(Student student, Institution institution, Loan loan)
        {            
            Update(student);
            Update(institution);
            SaveChanges();
            this.Attach(student).State = EntityState.Detached;
            this.Attach(institution).State = EntityState.Detached;

            Update(loan);
            SaveChanges();
            this.Attach(loan).State = EntityState.Detached;
        }
    }
}
