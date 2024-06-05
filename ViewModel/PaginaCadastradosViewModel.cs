#nullable disable
using ClinicaVet.Model;
using ClinicaVet.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClinicaVet.ViewModel
{
    public partial class PaginaCadastradosViewModel : ObservableObject
    {
        private readonly IUnitOfWork _unitOfWork;

        [ObservableProperty]
        private string nome;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private IEnumerable<Usuario> usuarios;

        public AsyncRelayCommand<Usuario> ExcluirCommand { get; }

        public PaginaCadastradosViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _ = LoadUsuariosAsync();
            ExcluirCommand = new AsyncRelayCommand<Usuario>(OnExcluirClickedAsync);
        }
        public async Task LoadUsuariosAsync() // Lista os usuários não colaboradores na página de cadastrados(Apenas Colaboradores tem acesso a essa página.)
        {
            var usuariosEnumerable = await _unitOfWork.UsuarioRepository.GetNonColaboradores();
            Usuarios = usuariosEnumerable;
        }
        public async Task OnExcluirClickedAsync(Usuario usuarioSelecionado) // Exclui um usuário da lista, mas apenas se o mesmo não tiver agendamentos realizados.
        {
            var agendamentos = await _unitOfWork.AgendamentoRepository.GetAgendamentosByIdTutor(usuarioSelecionado.Id);

            if (agendamentos.Any()) //Linha deve ser comentada.
            { //Linha deve ser comentada.
                await Application.Current.MainPage.DisplayAlert("INFORMAÇÃO!", "Este usuario possui agendamentos pendentes", "OK"); //Linha deve ser comentada.
                return; //Linha deve ser comentada.
            } //Linha deve ser comentada.
            var confirmacao = await Application.Current.MainPage.DisplayAlert("Confirmação!", "Tem certeza que deseja excluir o usuario?", "Sim", "Cancelar"); //Linha deve ser comentada.
            if (!confirmacao) //Linha deve ser comentada.
            { //Linha deve ser comentada.
                return; //Linha deve ser comentada.
            } //Linha deve ser comentada.
            if (!agendamentos.Any())
            {
                _unitOfWork.UsuarioRepository.Remove(usuarioSelecionado);
                await LoadUsuariosAsync();
            }
        }
    }
}
