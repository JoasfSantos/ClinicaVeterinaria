using ClinicaVet.Model;
using ClinicaVet.ViewModel;
using ClinicaVet.Repositories;

namespace ClinicaVet.View
{
    public partial class PagPrincipal : TabbedPage
    {
        private readonly Usuario _usuario;
        private readonly ContentPage _pagRegistro;
        private readonly ContentPage _pagRegistroAgendamento;
        private readonly IUnitOfWork _unitOfWork;

        public PagPrincipal(Usuario usuario, IUnitOfWork unitOfWork)
        {
            _usuario = usuario;

            _unitOfWork = unitOfWork;

            InitializeComponent();

            if (_usuario.Colaborador) {
                _pagRegistro = new PagRegistro(_unitOfWork);
                Children.Insert(2, _pagRegistro);
            } else
            {
                _pagRegistroAgendamento = new PagRegistroAgendamento(_unitOfWork, _usuario);
                Children.Insert(1, _pagRegistroAgendamento);
            }


            BindingContext = new PaginaPrincipalViewModel(_usuario);
        }
    }
}