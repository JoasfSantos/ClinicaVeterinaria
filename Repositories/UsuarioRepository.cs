#nullable disable
using Microsoft.EntityFrameworkCore;
using ClinicaVet.Data;
using ClinicaVet.Model;


namespace ClinicaVet.Repositories;
public class UsuarioRepository(MyDbContext context) : Repository<Usuario>(context)
{
    public virtual async Task<Usuario> GetUserByEmailAndPassword(string email, string senha)
    {
        return await Context.Set<Usuario>().FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);
    }

    public virtual async Task<Usuario> GetUserByEmail(string email)
    {
        return await Context.Set<Usuario>().FirstOrDefaultAsync(u => u.Email == email);
    }
    public virtual async Task<List<Usuario>> GetNonColaboradores()
    {
        return await Context.Set<Usuario>().Where(u => !u.Colaborador).ToListAsync();
    }
}