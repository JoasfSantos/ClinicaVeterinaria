using Microsoft.EntityFrameworkCore;
using ClinicaVet.Data;
using ClinicaVet.Model;

namespace ClinicaVet.Repositories
{
    public class AgendamentoRepository(MyDbContext context) : Repository<Agendamento>(context)
    {
        public async Task<IEnumerable<Agendamento>> GetAgendamentosByIdTutor(int idTutor)
        {
            return await Context.Set<Agendamento>().Where(a => a.IdTutor == idTutor).ToListAsync();
        }

        public async Task<IEnumerable<Agendamento>> GetAgendamentosByIdColaborador(int idColaborador)
        {
            return await Context.Set<Agendamento>().Where(a => a.IdColaborador == idColaborador).ToListAsync();
        }
    }
}