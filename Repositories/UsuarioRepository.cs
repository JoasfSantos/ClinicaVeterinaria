#nullable disable
using Microsoft.EntityFrameworkCore;
using ClinicaVet.Data;
using ClinicaVet.Model;


namespace ClinicaVet.Repositories;
public class UsuarioRepository(MyDbContext context) : Repository<Usuario>(context)
{
    public async Task<Usuario> GetUserByEmailAndPassword(string email, string senha)
    {
        return await Context.Set<Usuario>().FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);
    }

    public async Task<Usuario> GetUserByEmail(string email)
    {
        return await Context.Set<Usuario>().FirstOrDefaultAsync(u => u.Email == email);
    }
}