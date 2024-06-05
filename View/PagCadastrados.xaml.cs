using ClinicaVet.ViewModel;
using ClinicaVet.Repositories;

namespace ClinicaVet.View;


public partial class PagCadastrados : ContentPage
{
	private readonly IUnitOfWork _unitOfWork;
    public PagCadastrados(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;

		InitializeComponent();

		BindingContext = new PaginaCadastradosViewModel(_unitOfWork);
	}
}