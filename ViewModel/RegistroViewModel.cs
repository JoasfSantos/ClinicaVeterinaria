using ClinicaVet.Repositories;
using ClinicaVet.Model;
using ClinicaVet.View;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.RegularExpressions;


namespace ClinicaVet.ViewModel;
public partial class PagRegistroViewModel : ObservableObject
{
    private readonly string _regexEmail = @"^[a-zA-Z0-9_-]+@[a-z]+\.com(\.br)?$";
    private readonly Regex _regex;
    private readonly IUnitOfWork _unitOfWork;

    public ICommand RegistroCommand { get; }

    [ObservableProperty]
    private string nome;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string senha;


    public PagRegistroViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RegistroCommand = new Command(async () => await OnRegistroClicked());

        _regex = new Regex(_regexEmail);
    }

    private async Task OnRegistroClicked()
    {
        try
        {
            bool emailValido = ValidarEmail(Email);
            bool senhaValida = ValidarSenha(Senha);

            if (emailValido && senhaValida)
            {
                var usuarioRetornado = await _unitOfWork.UsuarioRepository.GetUserByEmail(Email);

                if (usuarioRetornado == null)
                {
                    var usuario = new Usuario(Nome, Email, Senha, false);
                    await _unitOfWork.UsuarioRepository.Add(usuario);
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Cadastro realizado com êxito!", "OK");
                    await Application.Current.MainPage.Navigation.PushAsync(new PagLogin());
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

    private bool ValidarEmail(string email)
    {
        Match match = _regex.Match(email);

        if (match.Success)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool ValidarSenha(string senha)
    {
        return senha.Length == 8;
    }
}