using ClinicaVet.Model;
using Microsoft.EntityFrameworkCore;


namespace ClinicaVet.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }

        private readonly string _connectionString;

        public MyDbContext()
        {
           /*string pathDbSqlite = Path.Combine(FileSystem.AppDataDirectory, "teste.db3");
           _connectionString = $"Filename={pathDbSqlite}";*/

            //Database.EnsureDeleted();

            /*Database.EnsureCreated();*/
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*optionsBuilder.UseSqlite(_connectionString);*/
        }
    }
}