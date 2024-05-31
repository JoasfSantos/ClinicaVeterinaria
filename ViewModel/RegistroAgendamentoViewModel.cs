using ClinicaVet.Repositories;
using ClinicaVet.Model;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;


namespace ClinicaVet.ViewModel;
public partial class RegistroAgendamentoViewModel : ObservableObject
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly Usuario _usuario;
    private readonly StatusAgendamento _statusAgendamentoAgendado;

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
    StatusAgendamento status;

    [ObservableProperty]
    private string tipoPet;

    [ObservableProperty]
    private int idTutor;

    [ObservableProperty]
    private int idColaborador;

    public RegistroAgendamentoViewModel(IUnitOfWork unitOfWork, Usuario usuario, bool fluxoEdicao)
    {
        FluxoEdicao = fluxoEdicao;

        if (FluxoEdicao)
        {
            TextoBotao = "SALVAR";
            BannerAgendamento = "editar_agendamento.png";
        }
        else
        {
            TextoBotao = "AGENDAR";
            BannerAgendamento = "agendar_banner.png";
        }

        TiposPet = Enum.GetNames(typeof(EspeciePet)).ToList();

        _unitOfWork = unitOfWork;

        _usuario = usuario;

        DataAgendamento = DateTime.Today.Date;

        _statusAgendamentoAgendado = StatusAgendamento.Agendado;

        RegistroAgendamentoCommand = new Command(async () => await OnRegistroAgendamentoClicked());
    }

    private async Task OnRegistroAgendamentoClicked()
    {
        try
        {
            
            var agendamento = new Agendamento(DataAgendamento, _statusAgendamentoAgendado, TipoPet, _usuario.Id, _usuario.Nome, 0);

            await _unitOfWork.AgendamentoRepository.Add(agendamento);
            await _unitOfWork.CommitAsync();

            // Exibir mensagem de sucesso
            await Application.Current.MainPage.DisplayAlert("Sucesso", "Cadastro realizado com êxito!", "OK");

            // Retornar para a página de login (substitua PagLogin por sua página real)
            //await Application.Current.MainPage.Navigation.PushAsync(new PagLogin());
        }
        catch (Exception ex)
        {
            // Exibir mensagem de erro
            await Application.Current.MainPage.DisplayAlert("Erro", $"Ocorreu um erro ao cadastrar: {ex.Message}\nDetalhes: {ex.InnerException?.Message}", "OK");
        }
    }
}