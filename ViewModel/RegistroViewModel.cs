using ClinicaVet.Repositories;
using ClinicaVet.Model;
using ClinicaVet.View;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;


namespace ClinicaVet.ViewModel;
public partial class PagRegistroViewModel : ObservableObject
{
    private readonly IUnitOfWork _unitOfWork;

    public ICommand RegistroCommand { get; }

    [ObservableProperty]
    private string nome;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string senha;

    [ObservableProperty]
    private bool colaborador;

    public PagRegistroViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RegistroCommand = new Command(async () => await OnRegistroClicked());
    }

    private async Task OnRegistroClicked()
    {
        try
        {
            var usuario = new Usuario(Nome, Email, Senha, false);

            await _unitOfWork.UsuarioRepository.Add(usuario);
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