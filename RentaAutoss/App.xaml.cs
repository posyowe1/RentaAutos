namespace RentaAutoss
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			MainPage = new MainPage(); // Asignar la MainPage que contiene el TabbedPage
		}
	}
}
