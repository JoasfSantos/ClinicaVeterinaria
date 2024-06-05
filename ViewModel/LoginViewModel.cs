#nullable disable
using ClinicaVet.Repositories;
using ClinicaVet.View;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;


namespace ClinicaVet.ViewModel
{
    public partial class PagLoginViewModel : ObservableObject
    {
        private readonly IUnitOfWork _unitOfWork;

        public ICommand LoginCommand { get; }
        public ICommand RedirecionarPagRegistroCommand { get; }

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string senha;

        public PagLoginViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            LoginCommand = new Command(async () => await OnLoginClicked());

            RedirecionarPagRegistroCommand = new Command(async () => await RedirecionarPagRegistro());
        }

        public async Task RedirecionarPagRegistro() // Redireciona para a página de registro, nesse caso, o valor é false pois é um tutor sendo cadastrado.
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PagRegistro(_unitOfWork, false));
        }

        public async Task OnLoginClicked()
        {
            bool isValid = ValidarLogin(); // Verifica se e-mail ou senha estão vazios.
            if (!isValid)
            {
                return;
            }

            var usuario = await _unitOfWork.UsuarioRepository.GetUserByEmailAndPassword(Email, Senha); 

            if (usuario != null) // Se achar o usuário no banco de dados, redireciona para a página principal.
            {
                await Application.Current.MainPage.Navigation.PushAsync(new PagPrincipal(usuario, _unitOfWork)); //Linha deve ser comentada.
            }
            else
            {
                // Caso não ache o usuário informa alerta de erro.
                await Application.Current.MainPage.DisplayAlert("Erro", "E-mail ou senha incorretos.", "OK"); //Linha deve ser comentada.
            }
        }

        public bool ValidarLogin()
        {
            if (Email == null || Senha == null)
            {
                Application.Current.MainPage.DisplayAlert("Erro", "E-mail ou senha não podem estar vazios.", "OK"); //Linha deve ser comentada
                return false;
            }
            return true;
        }
    }
}