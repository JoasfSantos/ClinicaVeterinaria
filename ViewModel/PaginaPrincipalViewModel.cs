using ClinicaVet.Model;
using ClinicaVet.View;
using CommunityToolkit.Mvvm.ComponentModel;
using ClinicaVet.Repositories;
using CommunityToolkit.Mvvm.Input;


namespace ClinicaVet.ViewModel
{
    public partial class PaginaPrincipalViewModel : ObservableObject
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Usuario _usuario;

        [ObservableProperty]
        private bool fluxoColaborador;

        [ObservableProperty]
        private string bannerAgendamentos;

        public AsyncRelayCommand<Agendamento> ExcluirCommand { get; }

        public AsyncRelayCommand<Agendamento> EditarCommand { get; }

        [ObservableProperty]
        private List<Agendamento> agendamentos;

        [ObservableProperty]
        private string tipoPet;

        [ObservableProperty]
        private string dataAgendamento;

        [ObservableProperty]
        private string nomeTutor;

        [ObservableProperty]
        private string status;

        public PaginaPrincipalViewModel(Usuario usuario, IUnitOfWork unitOfWork, bool fluxoColaborador)
        {
            FluxoColaborador = fluxoColaborador;

            _unitOfWork = unitOfWork;

            _usuario = usuario;

            if (FluxoColaborador) { 
                LoadAgendamentosAsync();
                BannerAgendamentos = "agendamento_solo.png";
            } else
            {
                LoadAgendamentosTutor();
                BannerAgendamentos = "agendamentos_banner.png";
            }

            ExcluirCommand = new AsyncRelayCommand<Agendamento>(OnExcluirClickedAsync);
            EditarCommand = new AsyncRelayCommand<Agendamento>(OnEditarClickedAsync);
        }

        public async Task LoadAgendamentosAsync()
        {
            var agendamentoEnumerable = await _unitOfWork.AgendamentoRepository.GetAll();
            Agendamentos = agendamentoEnumerable.ToList();
        }

        public async Task LoadAgendamentosTutor()
        {
            var agendamentoEnumerable = await _unitOfWork.AgendamentoRepository.GetAgendamentosByIdTutor(_usuario.Id);
            Agendamentos = agendamentoEnumerable.ToList();

        }

        private async Task OnExcluirClickedAsync(Agendamento agendamentoSelecionado)
        {
            _unitOfWork.AgendamentoRepository.Remove(agendamentoSelecionado);
            verificarFluxo();
        }


        private async Task OnEditarClickedAsync(Agendamento agendamentoSelecionado)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PagRegistroAgendamento(_unitOfWork, agendamentoSelecionado, true));            
        }

        private async Task verificarFluxo()
        {
            if (FluxoColaborador)
            {
                await LoadAgendamentosAsync();
            }
            else
            {
                LoadAgendamentosTutor();
            }

        }
    }
}