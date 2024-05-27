using ClinicaVet.Model;
using ClinicaVet.Utilidades;
using Microsoft.EntityFrameworkCore;



namespace ClinicaVet.Data
{
    public class MyDbContext : DbContext
    {

        public DbSet<Usuario> Usuarios { get; set; }

        private readonly string _connectionString;

        public MyDbContext()
        {
            _connectionString = $"Filename={PathDB.GetPath("teste.db3")}";

            Database.EnsureDeleted();

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}