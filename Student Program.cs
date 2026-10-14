using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StudentDatabaseApp
{
    public class Student
    {
        public int StudentId { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }

    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=StudentDb;Trusted_Connection=True;");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SchoolContext())
            {
                context.Database.EnsureCreated();
                Console.WriteLine("Database initialized successfully.");

                if (!context.Students.Any())
                {
                    var newStudent = new Student
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        EnrollmentDate = DateTime.Now
                    };
                    context.Students.Add(newStudent);
                    context.SaveChanges();
                    Console.WriteLine($"Successfully added student: {newStudent.FirstName}");
                }
            }
        }
    }
}
