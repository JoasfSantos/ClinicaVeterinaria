#nullable disable
using ClinicaVet.Model;
using ClinicaVet.Repositories;
using ClinicaVet.Utilidades;
using ClinicaVet.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace ClinicaVet.ViewModel
{
    public partial class PaginaPrincipalViewModel : ObservableObject
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Usuario _usuario;
        private readonly ValidadorEmail _validadorEmail;

        public AsyncRelayCommand EditarCommand { get; }
        public Command SairCommand { get; }

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
            _validadorEmail = new ValidadorEmail();

            EditarCommand = new AsyncRelayCommand(OnEditarClickedAsync);

            SairCommand = new Command(async () => await PaginaPrincipalViewModel.Sair());

            LabelIsVisible = true;

            EntryIsVisible = false;

            ButtonText = "Editar";

            _unitOfWork = unitOfWork;

            _usuario = usuario;

            Nome = _usuario.Nome;

            Email = _usuario.Email;

            Senha = _usuario.Senha;
        }

        public async Task OnEditarClickedAsync() // Editar dados do usuário.
        {
            // Verificar se houve alguma alteração
            bool houveAlteracao = false;

            // Ocultar rótulos e exibir campos de entrada para edição
            if (LabelIsVisible)
            {
                LabelIsVisible = false;
                EntryIsVisible = true;
                ButtonText = "Salvar";
                return;
            }

            // Validar os campos antes de exibir a mensagem de confirmação
            if (NomeNovo != _usuario.Nome && NomeNovo != null)
            {
                _usuario.Nome = NomeNovo;
                Nome = NomeNovo;
                houveAlteracao = true;
            }

            if (EmailNovo != _usuario.Email && EmailNovo != null)
            {
                if (!_validadorEmail.ValidarEmail(EmailNovo))
                {
                    await Application.Current.MainPage.DisplayAlert("Erro!", "E-mail inválido!", "OK");
                    return;
                }

                _usuario.Email = EmailNovo;
                Email = EmailNovo;
                houveAlteracao = true;
            }

            if (SenhaNovo != _usuario.Senha && SenhaNovo != null)
            {
                if (SenhaNovo.Length < 8)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro!", "A senha deve conter oito caracteres.", "OK");
                    return;
                }

                _usuario.Senha = SenhaNovo;
                Senha = SenhaNovo;
                houveAlteracao = true;
            }

            // Exibir mensagem de confirmação apenas se houver alterações
            if (houveAlteracao)
            {
                var confirmacao = await Application.Current.MainPage.DisplayAlert("Confirmação!", "Tem certeza que deseja alterar seus dados?", "Sim", "Cancelar"); //Linha deve ser comentada.
                if (!confirmacao) //Linha deve ser comentada.
                { //Linha deve ser comentada.
                    return; //Linha deve ser comentada.
                } //Linha deve ser comentada.
                await AtualizarUsuario();
                await Application.Current.MainPage.DisplayAlert("Informação!", "Dados alterados com sucesso!", "OK"); //Linha deve ser comentada.
            }
            // Restaurar a exibição padrão
            LabelIsVisible = true;
            EntryIsVisible = false;
            ButtonText = "Editar";
        }

        // Atualizar usuário no repositório
        public async Task AtualizarUsuario()
        {
            _unitOfWork.UsuarioRepository.Update(_usuario);
            IEnumerable<Agendamento> agendamentos = await _unitOfWork.AgendamentoRepository.GetAgendamentosByIdTutor(_usuario.Id);
            foreach (var agendamento in agendamentos) // Quando nome do usuário for alteado, se ele é um tutor, o mesmo também deve ser alterado para cada agendamento que ele fez.
            {
                agendamento.NomeTutor = _usuario.Nome;
                _unitOfWork.AgendamentoRepository.Update(agendamento);
            }
            await Application.Current.MainPage.Navigation.PushAsync(new PagPrincipal(_usuario, _unitOfWork)); //Linha deve ser comentada.
        }

        private static async Task Sair() // Sair kkk
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PagLogin());
        }
    }
}