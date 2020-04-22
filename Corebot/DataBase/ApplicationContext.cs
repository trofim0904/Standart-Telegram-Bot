using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corebot.DataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Sticker> Stickers { get; set; }

        private string _host;
        private string _user;
        private string _password;
        private string _database;

        public ApplicationContext(string host, string user, string password, string database)
        {
            _host = host;
            _user = user;
            _password = password;
            _database = database;

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql($"server={_host};UserId={_user};Password={_password};database={_database};");
        }
    }
}
