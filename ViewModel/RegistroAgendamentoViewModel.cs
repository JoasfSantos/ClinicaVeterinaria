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
    private readonly string _statusAgendamentoAgendado;

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

        TextoBotao = "AGENDAR";

        BannerAgendamento = "agendar_banner.png";

        TiposPet = Enum.GetNames(typeof(EspeciePet)).ToList();

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

        TextoBotao = "SALVAR";

        BannerAgendamento = "editar_agendamento.png";

        TiposPet = Enum.GetNames(typeof(EspeciePet)).ToList();

        _unitOfWork = unitOfWork;

        DataAgendamento = _agedamento.DataAgendamento;

        TipoPet = _agedamento.TipoPet;

        RegistroAgendamentoCommand = new Command(async () => await OnRegistroAgendamentoClicked(FluxoEdicao));
    }

    private async Task OnRegistroAgendamentoClicked(bool fluxoEdicao)
    {
        try
        {
            if (fluxoEdicao)
            {
                _agedamento.DataAgendamento = DataAgendamento;
                _agedamento.TipoPet = TipoPet;
                await _unitOfWork.AgendamentoRepository.Update(_agedamento);
                await Application.Current.MainPage.DisplayAlert("Sucesso", "Agendamento alterado com êxito!", "OK");
                atualizarUsuario();
                await Application.Current.MainPage.Navigation.PushAsync(new PagPrincipal(usuario, _unitOfWork));
            }
            else
            {
                if (validarCadastroAgendamento())
                {
                    var agendamento = new Agendamento(DataAgendamento, _statusAgendamentoAgendado, TipoPet, _usuario.Id, _usuario.Nome, 0);
                    await _unitOfWork.AgendamentoRepository.Add(agendamento);
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Cadastro realizado com êxito!", "OK");
                    await Application.Current.MainPage.Navigation.PushAsync(new PagPrincipal(_usuario, _unitOfWork));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Tipo de pet não pode estar vazio!", "OK");
                }
            }

        }
        catch (Exception ex)
        {
            // Exibir mensagem de erro
            await Application.Current.MainPage.DisplayAlert("Erro", $"Ocorreu um erro ao cadastrar: {ex.Message}\nDetalhes: {ex.InnerException?.Message}", "OK");
        }

    }
        private async Task atualizarUsuario()
        {
            usuario = await _unitOfWork.UsuarioRepository.Get(_agedamento.IdTutor);
        }

    private bool validarCadastroAgendamento()
    {
        // Verifica se algum atributo é nulo
        if (TipoPet == null)
        {
            return false;
        }
        // Se nenhum atributo for nulo, retorna true
        return true;
    }


}