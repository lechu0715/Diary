using Diary.Models.Configurations;
using Diary.Models.Domains;
using Diary.Properties;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace Diary
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        //: base("name=ApplicationDbContext")
        {
            string connString = ConfigurationManager.ConnectionStrings["ApplicationDbContext"].ConnectionString;
            //connString = $@"Server=({Settings.Default.AdressServer})\{Settings.Default.ServerName};Database={Settings.Default.DataBase};User Id={Settings.Default.Login};Password={Settings.Default.Password};";
            connString = $@"Server=(local)\SQLEXPRESS;Database=Diary;User Id=leszek;Password=12345;";
            this.Database.Connection.ConnectionString = connString;

        }



        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new RatingConfiguration());
        }

    }

}