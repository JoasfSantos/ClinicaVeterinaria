using ClinicaVet.Repositories;
using ClinicaVet.ViewModel;


namespace ClinicaVet.View
{
    public partial class PagLogin : ContentPage
    {
        private readonly IUnitOfWork _unitOfWork;

        public PagLogin()
        {
            InitializeComponent();

            _unitOfWork = new UnitOfWork();

            BindingContext = new PagLoginViewModel(_unitOfWork);
        }
    }
}