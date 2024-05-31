using ClinicaVet.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using ClinicaVet.Repositories;
using System.Windows.Input;


namespace ClinicaVet.ViewModel
{
    public partial class PaginaPrincipalViewModel : ObservableObject
    {
        public ICommand AtualizarCommand { get; }
        public ICommand TappedCommand { get; }

        [ObservableProperty]
        private List<Agendamento> agendamentos;

        [ObservableProperty]
        private string tipoPet;

        [ObservableProperty]
        private string dataAgendamento;

        [ObservableProperty]
        private string nomeTutor;

        [ObservableProperty]
        private string imagemPet;

        public PaginaPrincipalViewModel(Usuario usuario, IUnitOfWork unitOfWork)
        {

            LoadAgendamentosAsync(unitOfWork);
            AtualizarCommand = new Command(async () => await LoadAgendamentosAsync(unitOfWork));
            TappedCommand = new Command<Agendamento>(OnItemTapped);
            AtualizarImagemPet();


        }
        public async Task LoadAgendamentosAsync(IUnitOfWork unitOfWork)
        {
            var agendamentoEnumerable = await unitOfWork.AgendamentoRepository.GetAll();
            Agendamentos = agendamentoEnumerable.ToList();

        }

        private void OnItemTapped(Agendamento agendamento)
        {
            var agendamentoSelecionado = agendamento;
        }

        public void AtualizarImagemPet()
        {
            if (TipoPet.Equals("GATO"))
            {
                ImagemPet = "gato.png";
            }
            else if (TipoPet.Equals("CACHORRO"))
            {
                ImagemPet = "cachorro.png";
            }
            else
            {
                ImagemPet = "papagaio.png";
            }
        }


    }
}