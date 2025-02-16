using Views = PokePet.Views;

namespace PokePet
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

			Routing.RegisterRoute("PokePet", typeof(Views.PokePet));
		}
    }
}
