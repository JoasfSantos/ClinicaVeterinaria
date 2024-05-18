
namespace ClinicaVet.View
{
    public class LogoLoc : StackLayout
    {
        public LogoLoc()
        {
            var stackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };

            var image = new Image
            {
                Source = "logo_solo.png",
                WidthRequest = 200,
                HeightRequest = 150
            };
            this.Children.Add(image);
        }
    }
}
