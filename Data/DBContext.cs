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
            string pathDbSqlite = Path.Combine(FileSystem.AppDataDirectory, "teste.db3"); //Essa linha deve ser comentada.
            _connectionString = $"Filename={pathDbSqlite}"; //Essa linha deve ser comentada.

            //Database.EnsureDeleted(); //Essa linha deve FICAR comentada.

            Database.EnsureCreated(); //Essa linha deve ser comentada.
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}