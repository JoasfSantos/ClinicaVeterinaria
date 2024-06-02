#nullable disable
using ClinicaVet.Repositories;
using ClinicaVet.Model;
using ClinicaVet.View;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using ClinicaVet.Utilidades;


namespace ClinicaVet.ViewModel;
public partial class PagRegistroViewModel : ObservableObject
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly bool _isColaborador;
    private readonly ValidadorEmail _validadorEmail;

    public ICommand RegistroCommand { get; }
    [ObservableProperty]
    private string nome;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string senha;

    public PagRegistroViewModel(IUnitOfWork unitOfWork, bool colaborador)
    {
        _unitOfWork = unitOfWork;

        _isColaborador = colaborador;

        RegistroCommand = new Command(async () => await OnRegistroClicked());

        _validadorEmail = new ValidadorEmail();
    }

    private async Task OnRegistroClicked()
    {
        try
        {
            bool emailValido = _validadorEmail.ValidarEmail(Email);
            bool senhaValida = ValidarSenha(Senha);

            if (emailValido && senhaValida)
            {
                var usuarioRetornado = await _unitOfWork.UsuarioRepository.GetUserByEmail(Email);

                if (usuarioRetornado == null)
                {
                    var usuario = new Usuario(Nome, Email, Senha, _isColaborador);
                    await _unitOfWork.UsuarioRepository.Add(usuario);
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Cadastro realizado com êxito!", "OK");
                    if (!_isColaborador)
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new PagLogin());
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new PagPrincipal(usuario, _unitOfWork));
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", $"O e-mail: {Email}, já existente na base de dados", "OK");
                }
            }
            else
            {
                if (!emailValido)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", $"O e-mail: {Email} é inválido.", "OK");
                }

                if (!senhaValida)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "A senha deve ter oito caracteres.", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            // Exibir mensagem de erro genérica
            await Application.Current.MainPage.DisplayAlert("Erro", $"Ocorreu um erro ao cadastrar: {ex.Message}", "OK");
        }
    }

    private static bool ValidarSenha(string senha)
    {
        return senha.Length == 8;
    }
}