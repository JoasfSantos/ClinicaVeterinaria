using ClinicaVet.Model;
using ClinicaVet.ViewModel;
using ClinicaVet.Repositories;

namespace ClinicaVet.View
{
    public partial class PagAgendamentos : ContentPage
    {
        private readonly Usuario _usuario;
        private readonly IUnitOfWork _unitOfWork;

        public PagAgendamentos(Usuario usuario, IUnitOfWork unitOfWork)
        {
            _usuario = usuario;

            _unitOfWork = unitOfWork;

            InitializeComponent();

            BindingContext = new PagAgendamentosViewModel(_usuario, _unitOfWork, _usuario.Colaborador);
        }
    }
}