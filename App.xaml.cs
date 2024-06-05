using ClinicaVet.View;


namespace ClinicaVet
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new PagLogin());
        }
    }
}