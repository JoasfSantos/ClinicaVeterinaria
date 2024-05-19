using ClinicaVet.Repositories;
using ClinicaVet.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace ClinicaVet.ViewModel
{
    public class PagLoginViewModel : INotifyPropertyChanged
    {
        private readonly IUnitOfWork _unitOfWork;

        private IEnumerable<Usuario> _usuarios;
        private int _idUsuario;

        public ICommand LoginCommand { get; }
        public ICommand ViewUsersCommand { get; private set; }

        private string _email;
        private string _senha;

        public IEnumerable<Usuario> Usuarios


        {
            get => _usuarios;
            set
            {
                _usuarios = value;
                OnPropertyChanged();
            }
        }

        public int IdUsuario
        {
            get => _idUsuario;
            set
            {
                _idUsuario = value;
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


        public PagLoginViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            LoginCommand = new Command(async () => await OnLoginClicked());
            ViewUsersCommand = new Command(async () => await OnViewUsersClicked());
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
                // O usuário com o email e senha fornecidos foi encontrado
                // Agora você pode proceder com a lógica de login
            }
            else
            {
                // Nenhum usuário com o email e senha fornecidos foi encontrado
                // Você pode mostrar uma mensagem de erro ou algo similar
            }
        }

        private async Task<bool> ValidarLogin()
        {
            if (Email == null || Senha == null)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Email ou senha não podem estar vazios.", "OK");
                return false;
            }

            return true;
        }

        private async Task OnViewUsersClicked()
        {
            _usuarios = await _unitOfWork.UsuarioRepository.GetAll();
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}