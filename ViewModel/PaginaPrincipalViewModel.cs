using ClinicaVet.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using ClinicaVet.Repositories;
using System.Collections.ObjectModel;


namespace ClinicaVet.ViewModel
{
    public partial class PaginaPrincipalViewModel : ObservableObject
    {
        [ObservableProperty]
        public List<Agendamento> agendamentos;

        [ObservableProperty]
        public List<Agendamento> tipoPet;
        [ObservableProperty]
        public List<Agendamento> dataAgendamento;

        public PaginaPrincipalViewModel(Usuario usuario, IUnitOfWork unitOfWork)
        {
            LoadAgendamentosAsync(unitOfWork);

        }
        public async Task LoadAgendamentosAsync(IUnitOfWork unitOfWork)
        {
            var agendamentoEnumerable = await unitOfWork.AgendamentoRepository.GetAll();
            Agendamentos = agendamentoEnumerable.ToList();
        }
    }
}