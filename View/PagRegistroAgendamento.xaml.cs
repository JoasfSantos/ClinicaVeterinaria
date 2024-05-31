using ClinicaVet.Model;
using ClinicaVet.Repositories;
using ClinicaVet.ViewModel;


namespace ClinicaVet.View
{
    public partial class PagRegistroAgendamento : ContentPage
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Usuario _usuario;

        public PagRegistroAgendamento(IUnitOfWork unitOfWork, Usuario usuario)
        {
            InitializeComponent();

            _unitOfWork = unitOfWork;

            _usuario = usuario;

            BindingContext = new RegistroAgendamentoViewModel(_unitOfWork, _usuario, true);
        }
    }
}