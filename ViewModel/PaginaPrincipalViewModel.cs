using ClinicaVet.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClinicaVet.ViewModel
{
    public partial class PaginaPrincipalViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isColaborador;

        public PaginaPrincipalViewModel(Usuario usuario)
        {
            IsColaborador = usuario.Colaborador;
        }
    }
}