#nullable disable
using ClinicaVet.Repositories;
using ClinicaVet.Model;
using ClinicaVet.View;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;


namespace ClinicaVet.ViewModel;
public partial class RegistroAgendamentoViewModel : ObservableObject
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly Usuario _usuario;
    private readonly Agendamento _agedamento;
    private readonly string _statusAgendamentoAgendado = string.Empty;

    [ObservableProperty]
    private bool fluxoEdicao;

    [ObservableProperty]
    private string textoBotao;

    [ObservableProperty]
    private string bannerAgendamento;

    [ObservableProperty]
    private List<string> tiposPet;

    public ICommand RegistroAgendamentoCommand { get; }

    [ObservableProperty]
    private DateTime dataAgendamento;

    [ObservableProperty]
    private string tipoPet;

    [ObservableProperty]
    private int idTutor;

    [ObservableProperty]
    private int idColaborador;

    [ObservableProperty]
    private List<string> status;

    [ObservableProperty]
    private Usuario usuario;

    public RegistroAgendamentoViewModel(IUnitOfWork unitOfWork, Usuario usuario, bool fluxoEdicao)
    {
        FluxoEdicao = fluxoEdicao;

        TextoBotao = "Agendar";

        BannerAgendamento = "agendar_banner.png";

        TiposPet = [.. Enum.GetNames(typeof(EspeciePet))];

        _unitOfWork = unitOfWork;

        _usuario = usuario;

        _statusAgendamentoAgendado = StatusAgendamento.AGENDADO.ToString();

        DataAgendamento = DateTime.Today.Date;

        RegistroAgendamentoCommand = new Command(async () => await OnRegistroAgendamentoClicked(FluxoEdicao));
    }

    public RegistroAgendamentoViewModel(IUnitOfWork unitOfWork, Agendamento agendamento, bool fluxoEdicao)
    {
        FluxoEdicao = fluxoEdicao;

        _agedamento = agendamento;

        TextoBotao = "Salvar";

        BannerAgendamento = "editar_agendamento.png";

        TiposPet = [.. Enum.GetNames(typeof(EspeciePet))];

        _unitOfWork = unitOfWork;

        DataAgendamento = _agedamento.DataAgendamento;

        TipoPet = _agedamento.TipoPet;

        RegistroAgendamentoCommand = new Command(async () => await OnRegistroAgendamentoClicked(FluxoEdicao));
    }

    public async Task OnRegistroAgendamentoClicked(bool fluxoEdicao)
    {
        try
        {
            if (fluxoEdicao) // Diferenciação para saber se está registrando ou alterando um agendamento.
            {
                _agedamento.DataAgendamento = DataAgendamento;
                _agedamento.TipoPet = TipoPet;
                _unitOfWork.AgendamentoRepository.Update(_agedamento);
                await Application.Current.MainPage.DisplayAlert("Sucesso", "Agendamento alterado com êxito!", "OK"); //Linha deve ser comentada.
                await AtualizarUsuario();
                await Application.Current.MainPage.Navigation.PushAsync(new PagPrincipal(Usuario, _unitOfWork)); //Linha deve ser comentada.
            }
            else
            {
                if (ValidarCadastroAgendamento())
                {
                    var agendamento = new Agendamento(DataAgendamento, _statusAgendamentoAgendado, TipoPet, _usuario.Id, _usuario.Nome, 0);
                    await _unitOfWork.AgendamentoRepository.Add(agendamento);
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Agendamento realizado com êxito!", "OK"); //Linha deve ser comentada.
                    await Application.Current.MainPage.Navigation.PushAsync(new PagPrincipal(_usuario, _unitOfWork)); //Linha deve ser comentada.
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Tipo de pet não pode estar vazio!", "OK"); //Linha deve ser comentada.
                }
            }
        }
        catch (Exception ex)
        {
            // Exibir mensagem de erro
            await Application.Current.MainPage.DisplayAlert("Erro", $"{ex}\n Ocorreu um erro ao cadastrar. Por favor contate o suporte.", "OK"); //Linha deve ser comentada.
        }

    }
        public async Task AtualizarUsuario() // Atualiza o usuário para retornar novamente para a página principal.
        {
            Usuario = await _unitOfWork.UsuarioRepository.Get(_agedamento.IdTutor);
        }

    private bool ValidarCadastroAgendamento() // Impede que o usuário cadastre um agendamento sem o tipo de pet.
    {
        if (TipoPet == null)
        {
            return false;
        }
        return true;
    }
}