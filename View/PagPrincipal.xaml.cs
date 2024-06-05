#nullable disable
using ClinicaVet.Model;
using ClinicaVet.Repositories;
using ClinicaVet.ViewModel;

namespace ClinicaVet.View
{
    public partial class PagPrincipal: TabbedPage
    {
        private readonly Usuario _usuario;
        private readonly ContentPage _pagAgendamentos;
        private readonly ContentPage _pagRegistro;
        private readonly ContentPage _pagRegistroAgendamento;
        private readonly ContentPage _pagCadastrados;
        private readonly IUnitOfWork _unitOfWork;

        public PagPrincipal(Usuario usuario, IUnitOfWork unitOfWork)
        {
            _usuario = usuario;

            _unitOfWork = unitOfWork;

            InitializeComponent();

            _pagAgendamentos = new PagAgendamentos(_usuario, _unitOfWork);
            Children.Insert(0, _pagAgendamentos);

            if (_usuario.Colaborador) { // Se usu�rio for um colaborador, pode acessar p�ginas de cadastro para registrar outros colaboradores, al�m de cosultar os usu�rios cadastrados.
                _pagRegistro = new PagRegistro(_unitOfWork, true, _usuario);
                _pagCadastrados = new PagCadastrados( _unitOfWork);
                Children.Insert(1, _pagRegistro);
                Children.Insert(2, _pagCadastrados);
            }else{
                _pagRegistroAgendamento = new PagRegistroAgendamento(_unitOfWork, _usuario, _usuario.Colaborador); //Se o usu�rio for um tutor, ele poder� registrar agendamentos.
                Children.Insert(1, _pagRegistroAgendamento);
            }

            BindingContext = new PaginaPrincipalViewModel(_unitOfWork, _usuario);
        }
    }
}