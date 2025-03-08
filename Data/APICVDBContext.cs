using Microsoft.EntityFrameworkCore;
using RestAPICVHantering.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using System;

namespace RestAPICVHantering.Data
{
    public class APICVDBContext : DbContext
    {
        public APICVDBContext(DbContextOptions<APICVDBContext> options) : base(options)
        {
        }
        
        public DbSet<Person> Persons { get; set; }
        public DbSet<Education> Educations { get; set; }

        public DbSet<WorkExperience> WorkExperiences { get; set; }
    }
}
