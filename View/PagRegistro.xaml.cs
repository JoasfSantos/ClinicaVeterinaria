using ClinicaVet.Repositories;
using ClinicaVet.ViewModel;


namespace ClinicaVet.View;
public partial class PagRegistro : ContentPage
{
    private readonly bool _isColaborador;
    public PagRegistro(IUnitOfWork unitOfWork, bool isColaborador)
    {
        InitializeComponent();

        _isColaborador = isColaborador;

        BindingContext = new PagRegistroViewModel(unitOfWork, _isColaborador);
    }
}