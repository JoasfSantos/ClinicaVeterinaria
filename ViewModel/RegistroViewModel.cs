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
    private readonly Usuario _usuarioCadastrando;

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

    public PagRegistroViewModel(IUnitOfWork unitOfWork, bool colaborador, Usuario usuarioCadastrando)
    {
        _usuarioCadastrando = usuarioCadastrando;

        _unitOfWork = unitOfWork;

        _isColaborador = colaborador;

        RegistroCommand = new Command(async () => await OnRegistroClicked());

        _validadorEmail = new ValidadorEmail();
    }

    public async Task OnRegistroClicked()
    {
        try
        {
            bool emailValido = _validadorEmail.ValidarEmail(Email);
            bool senhaValida = ValidarSenha(Senha);

            if (emailValido && senhaValida)
            {
                var usuarioRetornado = await _unitOfWork.UsuarioRepository.GetUserByEmail(Email); // Se achar um usuário, disparará mensagem sobre usuário já existente.

                if (usuarioRetornado == null)
                {
                    var usuario = new Usuario(Nome, Email, Senha, _isColaborador);
                    await _unitOfWork.UsuarioRepository.Add(usuario);
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Cadastro realizado com êxito!", "OK"); //(Linha deve ser comentada)
                    if (!_isColaborador) // (Bloco completo "if else" deve ser comentado)
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new PagLogin());
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new PagPrincipal(_usuarioCadastrando, _unitOfWork));
                    }
                }
                else // (Bloco completo "else" deve ser comentado)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", $"O e-mail: {Email}, já existente na base de dados", "OK");
                }
            }
            else // (Bloco completo "else" deve ser comentado)
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
            await Application.Current.MainPage.DisplayAlert("Erro", $"Ocorreu um erro ao cadastrar: {ex.Message}", "OK"); // (Deve ser comentado)
        }
    }

    private static bool ValidarSenha(string senha)
    {
        return senha.Length == 8;
    }
}