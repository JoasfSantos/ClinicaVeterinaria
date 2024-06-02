using ClinicaVet.Model;
using ClinicaVet.View;
using CommunityToolkit.Mvvm.ComponentModel;
using ClinicaVet.Repositories;
using CommunityToolkit.Mvvm.Input;


namespace ClinicaVet.ViewModel
{
    public partial class PagAgendamentosViewModel : ObservableObject
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Usuario _usuario;


        [ObservableProperty]
        private bool fluxoColaborador;

        [ObservableProperty]
        private string bannerAgendamentos;

        public AsyncRelayCommand<Agendamento> ExcluirCommand { get; }

        public AsyncRelayCommand<Agendamento> EditarCommand { get; }

        public AsyncRelayCommand<Agendamento> AtualizarCommand { get; }

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

        [ObservableProperty]
        private bool isTutor;

        public PagAgendamentosViewModel(Usuario usuario, IUnitOfWork unitOfWork, bool fluxoColaborador)
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
            AtualizarCommand = new AsyncRelayCommand<Agendamento>(OnEditarStatusAgendamento);
        }

        public async Task LoadAgendamentosAsync()
        {
            var agendamentoEnumerable = await _unitOfWork.AgendamentoRepository.GetAll();
            Agendamentos = agendamentoEnumerable.ToList();
            foreach (var agendamento in Agendamentos)
            {
                agendamento.IsTutor = FluxoColaborador;
            }
        }

        public async Task LoadAgendamentosTutor()
        {
            var agendamentoEnumerable = await _unitOfWork.AgendamentoRepository.GetAgendamentosByIdTutor(_usuario.Id);
            Agendamentos = agendamentoEnumerable;
            foreach (var agendamento in Agendamentos)
            {
                agendamento.IsTutor = FluxoColaborador;   
            }

        }

        private async Task OnExcluirClickedAsync(Agendamento agendamentoSelecionado)
        {
            var confirmacao = await Application.Current.MainPage.DisplayAlert("Confirmação!", "Tem certeza que deseja excluir o agendamento?", "OK", "Cancelar");
            if (!confirmacao)
            {
                return;
            }
            else
            {
                _unitOfWork.AgendamentoRepository.Remove(agendamentoSelecionado);
                verificarFluxo();
            }
        }

        private async Task OnEditarClickedAsync(Agendamento agendamentoSelecionado)
        {
            var confirmacao = await Application.Current.MainPage.DisplayAlert("Confirmação!", "Tem certeza que deseja editar o agendamento?", "OK", "Cancelar");
            if (!confirmacao)
            {
                return;
            }
            else
            {
            await Application.Current.MainPage.Navigation.PushAsync(new PagRegistroAgendamento(_unitOfWork, agendamentoSelecionado, true));            
            }
        }

        private async Task OnEditarStatusAgendamento(Agendamento agendamento)
        {
            var confirmacao = await Application.Current.MainPage.DisplayAlert("Confirmação!", "Tem certeza que deseja alterar o status do agendamento?", "OK", "Cancelar");
            if (!confirmacao)
            {
                return;
            }
            var novoStatus = "";
            var statusAtual = agendamento.Status;
            switch (statusAtual)
            {
                case "AGENDADO":
                    novoStatus = "EM ANDAMENTO";
                    break;
                case "EM ANDAMENTO":
                    novoStatus = "CONCLUÍDO";
                    break;
            }
            agendamento.Status = novoStatus;
            await _unitOfWork.AgendamentoRepository.Update(agendamento);
            verificarFluxo();
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