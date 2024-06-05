#nullable disable
using ClinicaVet.Model;
using ClinicaVet.Repositories;
using ClinicaVet.ViewModel;


namespace ClinicaVet.View;
public partial class PagRegistro : ContentPage
{
    private readonly bool _isColaborador;
    private readonly Usuario _usuarioCadastrando;
    public PagRegistro(IUnitOfWork unitOfWork, bool isColaborador, Usuario usuarioCadastrando)
    {
        InitializeComponent();

        _isColaborador = isColaborador;

        _usuarioCadastrando = usuarioCadastrando;
        
        BindingContext = new PagRegistroViewModel(unitOfWork, _isColaborador, _usuarioCadastrando);
  
    }
    public PagRegistro(IUnitOfWork unitOfWork, bool isColaborador)
    {
        InitializeComponent();

        _isColaborador = isColaborador;

        BindingContext = new PagRegistroViewModel(unitOfWork, _isColaborador);

    }
}