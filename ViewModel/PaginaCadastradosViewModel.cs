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
        public async Task LoadUsuariosAsync()
        {
            var usuariosEnumerable = await _unitOfWork.UsuarioRepository.GetNonColaboradores();
            Usuarios = usuariosEnumerable;
        }
        public async Task OnExcluirClickedAsync(Usuario usuarioSelecionado)
        {
            var agendamentos = await _unitOfWork.AgendamentoRepository.GetAgendamentosByIdTutor(usuarioSelecionado.Id);

            //if (agendamentos.Any())
            //{
            //    await Application.Current.MainPage.DisplayAlert("INFORMAÇÃO!", "Este usuario possui agendamentos pendentes", "OK");
            //    return;
            //}
            //var confirmacao = await Application.Current.MainPage.DisplayAlert("Confirmação!", "Tem certeza que deseja excluir o usuario?", "Sim", "Cancelar");
            //if (!confirmacao)
            //{
            //    return;
            //}
            if (!agendamentos.Any())
            {
                _unitOfWork.UsuarioRepository.Remove(usuarioSelecionado);
                await LoadUsuariosAsync();
            }
        }
    }
}
