using ClinicaVet.Model;
using ClinicaVet.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace ClinicaVet.ViewModel
{
    public partial class PaginaPrincipalViewModel : ObservableObject
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Usuario _usuario;
        public AsyncRelayCommand EditarCommand { get; }



        [ObservableProperty]
        private string nome;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string senha;

        [ObservableProperty]
        private string buttonText;

        [ObservableProperty]
        private bool entryIsVisible;

        [ObservableProperty]
        private bool labelIsVisible;

        [ObservableProperty]
        private string nomeNovo;

        [ObservableProperty]
        private string emailNovo;

        [ObservableProperty]
        private string senhaNovo;

        public PaginaPrincipalViewModel(IUnitOfWork unitOfWork, Usuario usuario)
        {
            EditarCommand = new AsyncRelayCommand(OnEditarClickedAsync);
            LabelIsVisible = true;
            EntryIsVisible = false;
            ButtonText = "Editar";
            Senha = "********";
            _unitOfWork = unitOfWork;
            _usuario = usuario;
            Nome = _usuario.Nome;
            Email = _usuario.Email;
        }


        private async Task OnEditarClickedAsync()
        {
            if (LabelIsVisible == true)
            {
                LabelIsVisible = false;
                EntryIsVisible = true;
                ButtonText = "Salvar";

            }
            else
            {
                LabelIsVisible = true;
                EntryIsVisible = false;
                ButtonText = "Editar";
                if (NomeNovo != _usuario.Nome && NomeNovo != null)
                {
                    _usuario.Nome = NomeNovo;
                    Nome = NomeNovo;
                }
                if (EmailNovo != _usuario.Email && EmailNovo != null)
                {
                    _usuario.Email = EmailNovo;
                    Email = EmailNovo;
                }

                if (SenhaNovo != _usuario.Senha && SenhaNovo != null)
                {
                    _usuario.Senha = SenhaNovo;
                }
                await _unitOfWork.UsuarioRepository.Update(_usuario);
            }

        }

    }
}