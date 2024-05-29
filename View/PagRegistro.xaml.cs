using ClinicaVet.Repositories;
using ClinicaVet.ViewModel;


namespace ClinicaVet.View;
public partial class PagRegistro : ContentPage
{
    public PagRegistro(IUnitOfWork unitOfWork)
    {
        InitializeComponent();

        BindingContext = new PagRegistroViewModel(unitOfWork);
    }
}