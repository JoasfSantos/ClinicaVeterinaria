using ClinicaVet.Model;
using ClinicaVet.Repositories;

namespace ClinicaVet.View
{
    public partial class PagPrincipal: TabbedPage
    {
        private readonly Usuario _usuario;
        private readonly ContentPage _pagAgendamentos;
        private readonly ContentPage _pagRegistro;
        private readonly ContentPage _pagRegistroAgendamento;
        private readonly IUnitOfWork _unitOfWork;

        public PagPrincipal(Usuario usuario, IUnitOfWork unitOfWork)
        {
            _usuario = usuario;

            _unitOfWork = unitOfWork;

            InitializeComponent();

            _pagAgendamentos = new PagAgendamentos(_usuario, _unitOfWork);
            Children.Insert(0, _pagAgendamentos);

            if (_usuario.Colaborador) {
                _pagRegistro = new PagRegistro(_unitOfWork);
                //_pagRegistro.BindingContext = new PagAgendamentosViewModel(_usuario, _unitOfWork, _usuario.Colaborador);
                Children.Insert(1, _pagRegistro);
            }else{
                _pagRegistroAgendamento = new PagRegistroAgendamento(_unitOfWork, _usuario, _usuario.Colaborador);
                Children.Insert(1, _pagRegistroAgendamento);
            }

            CurrentPage = _pagAgendamentos;
        }
    }
}