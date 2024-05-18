using ClinicaVet.Repositories;
using ClinicaVet.Utilidades;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace ClinicaVet.ViewModel
{
    public class PagLoginViewModel : INotifyPropertyChanged
    {
        private readonly IUnitOfWork _unitOfWork;

        public ICommand LoginCommand { get; }

        private string _email;

        private string _senha;

        private string _pathDbSqlite;

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

        public string PathDbSqlite // Propriedade para exibir o valor do pathDbSqlite
        {
            get => _pathDbSqlite;
            set
            {
                _pathDbSqlite = value;
                OnPropertyChanged();
            }
        }

        public PagLoginViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            LoginCommand = new Command(async () => await OnLoginClicked());
            PathDbSqlite = PathDB.GetPath("teste.db3");
        }

        private async Task OnLoginClicked()
        {
            // Supondo que você tenha as variáveis 'email' e 'senha'
            var user = await _unitOfWork.UsuarioRepository.GetUserByEmailAndPassword(Email, Senha);

            if (user != null)
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}