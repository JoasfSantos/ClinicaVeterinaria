using Microsoft.EntityFrameworkCore;
using ClinicaVet.Data;
using ClinicaVet.Model;

namespace ClinicaVet.Repositories
{
    public class AgendamentoRepository : Repository<Agendamento>
    {
        public AgendamentoRepository(MyDbContext context) : base(context)
        {
        }

/*        public async Task<List<Agendamento>> GetAgendamentosByIdTutor(int idTutor)
        {
            return await Context.Set<Agendamento>().Where(a => a.IdTutor == idTutor).ToListAsync();
        }*/

        public async Task<List<Agendamento>> GetAgendamentosByIdColaborador(int idColaborador)
        {
            return await Context.Set<Agendamento>().Where(a => a.IdColaborador == idColaborador).ToListAsync();
        }
    }
}