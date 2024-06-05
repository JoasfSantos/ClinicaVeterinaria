#nullable disable
using ClinicaVet.Model;
using ClinicaVet.Repositories;
using ClinicaVet.ViewModel;


namespace ClinicaVet.View
{
    public partial class PagRegistroAgendamento : ContentPage // Sobrecarga de construtores para redirecionar o usuário para registrar um agendamento ou edita-lo.
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Usuario _usuario;
        private readonly Agendamento _agendamento;
        private readonly bool _fluxoEdicao;

        public PagRegistroAgendamento(IUnitOfWork unitOfWork, Usuario usuario, bool fluxoEdicao)
        {
            InitializeComponent();

            _unitOfWork = unitOfWork;

            _fluxoEdicao = fluxoEdicao;

            _usuario = usuario;

            BindingContext = new RegistroAgendamentoViewModel(_unitOfWork, _usuario, fluxoEdicao);
        }

        public PagRegistroAgendamento(IUnitOfWork unitOfWork, Agendamento agendamento, bool fluxoEdicao)
        {
            InitializeComponent();

            _unitOfWork = unitOfWork;

            _fluxoEdicao = fluxoEdicao;

            _agendamento = agendamento;

            BindingContext = new RegistroAgendamentoViewModel(_unitOfWork, _agendamento, _fluxoEdicao);
        }
    }
}