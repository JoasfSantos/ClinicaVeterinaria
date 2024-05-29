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
    private readonly StatusAgendamento _statusAgendamentoAgendado;

    public ICommand RegistroAgendamentoCommand { get; }

    [ObservableProperty]
    private DateTime dataAgendamento;

    [ObservableProperty]
    StatusAgendamento status;

    [ObservableProperty]
    private EspeciePet tipoPet;

    [ObservableProperty]
    private int idTutor;

    [ObservableProperty]
    private int idColaborador;

    [ObservableProperty]
    public List<string> tiposPet;

    public RegistroAgendamentoViewModel(IUnitOfWork unitOfWork, Usuario usuario)
    {
        _unitOfWork = unitOfWork;

        _usuario = usuario;

        _statusAgendamentoAgendado = StatusAgendamento.Agendado;

        RegistroAgendamentoCommand = new Command(async () => await OnRegistroAgendamentoClicked());

        TiposPet = Enum.GetNames(typeof(EspeciePet)).ToList();
    }

    private async Task OnRegistroAgendamentoClicked()
    {
        try
        {
            var agendamento = new Agendamento(DataAgendamento, _statusAgendamentoAgendado, TipoPet, IdTutor, 0);

            await _unitOfWork.AgendamentoRepository.Add(agendamento);
            await _unitOfWork.CommitAsync();

            // Exibir mensagem de sucesso
            await Application.Current.MainPage.DisplayAlert("Sucesso", "Cadastro realizado com êxito!", "OK");

            // Retornar para a página de login (substitua PagLogin por sua página real)
            await Application.Current.MainPage.Navigation.PushAsync(new PagLogin());
        }
        catch (Exception ex)
        {
            // Exibir mensagem de erro
            await Application.Current.MainPage.DisplayAlert("Erro", $"Ocorreu um erro ao cadastrar: {ex.Message}", "OK");
        }
    }
}