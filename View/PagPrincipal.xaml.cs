using ClinicaVet.Model;
using ClinicaVet.ViewModel;


namespace ClinicaVet.View;
public partial class PagPrincipal : TabbedPage
{
	private readonly Usuario _usuario;

	public PagPrincipal(Usuario usuario)
	{
		_usuario = usuario;


		InitializeComponent();

        BindingContext = new PaginaPrincipalViewModel(_usuario);
    }
}