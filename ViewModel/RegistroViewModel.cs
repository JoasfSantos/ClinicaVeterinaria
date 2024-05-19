using ClinicaVet.Repositories;
using ClinicaVet.Model;
using ClinicaVet.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ClinicaVet.ViewModel;
public class PagRegistroViewModel : INotifyPropertyChanged
{
    private readonly IUnitOfWork _unitOfWork;

    public ICommand RegistroCommand { get; }

    private string _nome;
    private string _email;
    private string _senha;
    private bool _colaborador;

    public string Nome
    {
        get => _nome;
        set
        {
            _nome = value;
            OnPropertyChanged();
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    public string Senha
    {
        get => _senha;
        set
        {
            _senha = value;
            OnPropertyChanged();
        }
    }

    public bool Colaborador
    {
        get => _colaborador;
        set
        {
            _colaborador = value;
        }
    }

    public PagRegistroViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        RegistroCommand = new Command(async () => await OnRegistroClicked());
    }

    private async Task OnRegistroClicked()
    {
        try
        {
            var usuario = new Usuario(Nome, Email, Senha, Colaborador)
            {
                Nome = this.Nome,
                Email = this.Email,
                Senha = this.Senha,
                Colaborador = false
            };

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

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


}