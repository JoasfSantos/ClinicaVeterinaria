using ClinicaVet.View;
using ClinicaVet.Repositories;
using ClinicaVet.Data;
using Nancy.TinyIoc;

namespace ClinicaVet
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Registre suas dependências
            //TinyIoCContainer.Current.Register<IUnitOfWork, UnitOfWork>();
            //TinyIoCContainer.Current.Register<MyDbContext>().AsSingleton();

            // Resolva suas dependências
            //var unitOfWork = TinyIoCContainer.Current.Resolve<IUnitOfWork>();

            // Passe suas dependências para o ViewModel
            MainPage = new NavigationPage(new PagLogin());
        }
    }
}