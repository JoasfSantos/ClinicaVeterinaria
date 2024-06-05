#nullable disable
using ClinicaVet.Model;
using ClinicaVet.View;
using ClinicaVet.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace ClinicaVet.ViewModel
{
    public partial class PagAgendamentosViewModel : ObservableObject
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Usuario _usuario;

        public AsyncRelayCommand<Agendamento> ExcluirCommand { get; }

        public AsyncRelayCommand<Agendamento> EditarCommand { get; }

        public AsyncRelayCommand<Agendamento> AtualizarCommand { get; }

        [ObservableProperty]
        private bool fluxoColaborador;

        [ObservableProperty]
        private string bannerAgendamentos;

        [ObservableProperty]
        private IEnumerable<Agendamento> agendamentos;

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

            _ = VerificarFluxo();

            ExcluirCommand = new AsyncRelayCommand<Agendamento>(OnExcluirClickedAsync);
            EditarCommand = new AsyncRelayCommand<Agendamento>(OnEditarClickedAsync);
            AtualizarCommand = new AsyncRelayCommand<Agendamento>(OnEditarStatusAgendamento);
        }

        public async Task LoadAgendamentosAsync()
        {
            var agendamentoEnumerable = await _unitOfWork.AgendamentoRepository.GetAll();
            Agendamentos = agendamentoEnumerable;
            AtualizarTutor(Agendamentos);
        }

        public async Task LoadAgendamentosTutor()
        {
            try
            {
                var agendamentoEnumerable = await _unitOfWork.AgendamentoRepository.GetAgendamentosByIdTutor(_usuario.Id);
                Agendamentos = agendamentoEnumerable;
                AtualizarTutor(Agendamentos);

            }
            catch (Exception ex)
            {
                //await Application.Current.MainPage.DisplayAlert("Erro", $"{ex}\n Ocorreu um erro ao cadastrar. Por favor contate o suporte.", "OK");
            }
        }

        public async Task OnExcluirClickedAsync(Agendamento agendamentoSelecionado)
        {
            //var confirmacao = await Application.Current.MainPage.DisplayAlert("Confirmação!", "Tem certeza que deseja excluir o agendamento?", "Sim", "Cancelar");
            //if (!confirmacao)
            //{
            //    return;
            //}
            _unitOfWork.AgendamentoRepository.Remove(agendamentoSelecionado);
            await VerificarFluxo();
        }

        public async Task OnEditarClickedAsync(Agendamento agendamentoSelecionado)
        {
            var confirmacao = await Application.Current.MainPage.DisplayAlert("Confirmação!", "Tem certeza que deseja editar o agendamento?", "Sim", "Cancelar");
            if (!confirmacao)
            {
                return;
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushAsync(new PagRegistroAgendamento(_unitOfWork, agendamentoSelecionado, true));
            }
        }

        public async Task OnEditarStatusAgendamento(Agendamento agendamento)
        {
            var statusAtual = agendamento.Status;
            var novoStatus = "";
            //if (statusAtual.Equals("CONCLUÍDO"))
            //{
            //    await Application.Current.MainPage.DisplayAlert("Informação!", "Não é possível mais alterar o status.", "OK");
            //    return;
            //}

            //var confirmacao = await Application.Current.MainPage.DisplayAlert("Confirmação!", "Tem certeza que deseja alterar o status do agendamento?", "OK", "Cancelar");
            //if (!confirmacao)
            //{
            //    return;
            //}
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
            _unitOfWork.AgendamentoRepository.Update(agendamento);
            await VerificarFluxo();

        }

        public async Task VerificarFluxo()
        {
            if (FluxoColaborador)
            {
                BannerAgendamentos = "agendamento_solo.png";
                await LoadAgendamentosAsync();
            }
            else
            {
                BannerAgendamentos = "agendamentos_banner.png";
                await LoadAgendamentosTutor();
            }
        }

        public void AtualizarTutor(IEnumerable<Agendamento> agendamentos)
        {
            foreach (var agendamento in agendamentos)
            {
                agendamento.IsTutor = FluxoColaborador;
            }
        }
    }
}