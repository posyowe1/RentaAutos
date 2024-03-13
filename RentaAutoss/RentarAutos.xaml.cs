namespace RentaAutoss;

public partial class RentarAutos : ContentPage
{
	public RentarAutos()
	{
		InitializeComponent();
		this.IconImageSource = "icono1.png";
	}
	Operaciones op = new Operaciones();
	private void btnCerrar_Clicked(object sender, EventArgs e)
	{

    }

	private void btnGuardar_Clicked(object sender, EventArgs e)
	{
		if (eCliente.Text == null || eTeledfono.Text == null || ePlacas.Text == null)
		{
			DisplayAlert("Aviso", "Verifica que hayas rellenado todos los campos", "OK");
		}
		else
		{
			var auto = new Autos
			{
				placas = ePlacas.Text,
				rentado = true
			};

			op.ModificarAutoRentado(auto);
			var tabbedpage = Application.Current.MainPage as MainPage;
			ListaAutos agregarA = tabbedpage.Children[0] as ListaAutos;
			agregarA.CargarAutos();

			ePlacas.Text = string.Empty;
			eTeledfono.Text = string.Empty;
			eCliente.Text = string.Empty;
			fFechaEntrega.Date = DateTime.Now;
			fFechaSalida.Date = DateTime.Now;
			ePrecioxDia.Text = "0";
			eTotal.Text = "0";
		}
	}
	public bool PasarAutoRenta(Autos auto)
	{
		ByteArrayToImageConverter converter = new ByteArrayToImageConverter();
		try
		{
			ePlacas.Text = auto.placas;
			ePlacas.IsEnabled = false;
			ePrecioxDia.Text = auto.precioxdia.ToString();
			ePrecioxDia.IsEnabled = false;
			return true;
		}
		catch (Exception ex)
		{
			DisplayAlert("Error", $"{ex.Message}", "OK");
			return false;
		}
	}

	private void fFechaEntrega_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
	{
		if (e.PropertyName == DatePicker.DateProperty.PropertyName)
		{
			DateTime fechaentrega = fFechaEntrega.Date;
			DateTime fechasalida = fFechaSalida.Date;
			int precio = int.Parse(ePrecioxDia.Text);
			eTotal.Text = (precio * (fechaentrega - fechasalida).Days).ToString();
		}
	}
}