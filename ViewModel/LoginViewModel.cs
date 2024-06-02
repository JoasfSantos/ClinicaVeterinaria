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

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string senha;

        public PagLoginViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            LoginCommand = new Command(async () => await OnLoginClicked());
        }

        private async Task OnLoginClicked()
        {
            bool isValid = await ValidarLogin();
            if (!isValid)
            {
                return;
            }

            var usuario = await _unitOfWork.UsuarioRepository.GetUserByEmailAndPassword(Email, Senha);

            if (usuario != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new PagPrincipal(usuario, _unitOfWork));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "E-mail ou senha incorretos.", "OK");
            }
        }

        private async Task<bool> ValidarLogin()
        {
            if (Email == null || Senha == null)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "E-mail ou senha não podem estar vazios.", "OK");
                return false;
            }
            return true;
        }
    }
}