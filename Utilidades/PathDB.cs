namespace ClinicaVet.Utilidades
{
    public static class PathDB //Classe que define o diretório do banco SQL lite.
    {
        public static string GetPath(string nameDB)
        {
            string pathDbSqlite = Path.Combine(FileSystem.AppDataDirectory, nameDB);
            return pathDbSqlite;
        }
    }
}