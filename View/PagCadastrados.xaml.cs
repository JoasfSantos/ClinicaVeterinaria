using ClinicaVet.ViewModel;
using ClinicaVet.Model;
using Nancy.ModelBinding;
using ClinicaVet.Repositories;

namespace ClinicaVet.View;


public partial class PagCadastrados : ContentPage
{
	private readonly Usuario _usuario;
	private readonly IUnitOfWork _unitOfWork;
    public PagCadastrados(Usuario usuario, IUnitOfWork unitOfWork)
	{
		_usuario = usuario;
		_unitOfWork = unitOfWork;
		InitializeComponent();
		BindingContext = new PaginaCadastradosViewModel(_usuario, _unitOfWork);
	}
}