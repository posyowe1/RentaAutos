using System.ComponentModel;
using System.Globalization;
using System.IO;

namespace RentaAutoss;

public partial class AgregarAutos : ContentPage
{
	public AgregarAutos()
	{
		InitializeComponent();
		this.IconImageSource = "icono2.png";
	}
	Operaciones op = new Operaciones();
	ByteArrayToImageConverter converter = new ByteArrayToImageConverter();
	byte[] bytes;
	private async void btnFoto_Clicked(object sender, EventArgs e)
	{
		try
		{
			var resultado = await MediaPicker.PickPhotoAsync();

			if (resultado != null)
			{
				// Obtener la imagen como un flujo de datos
				using (var stream = await resultado.OpenReadAsync())
				{
					// Leer los datos de la imagen en un arreglo de bytes
					var ms = new MemoryStream();
					await stream.CopyToAsync(ms);
					bytes = ms.ToArray();

					// Crear un objeto de imagen desde el arreglo de bytes
					var imagenSource = ImageSource.FromStream(() => new MemoryStream(bytes));
					selecImagen.Source = imagenSource;
				}
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"Ocurrió un error al cargar la foto: {ex.Message}", "Aceptar");
		}
	}

	public bool PasarAuto(Autos auto)
	{
		try
		{
			bytes = auto.imgAuto;
			ImageSource imageSource = (ImageSource)converter.Convert(auto.imgAuto, typeof(ImageSource), null, CultureInfo.CurrentCulture);
			selecImagen.Source = imageSource;
			ePlacas.Text = auto.placas;
			eModelo.Text = auto.modelo;
			eMarca.Text = auto.marca;
			eColor.Text = auto.color;
			eAño.Text = auto.año;
			ePrecioDias.Text = auto.precioxdia.ToString();
			return true;
		}
		catch (Exception ex)
		{
			DisplayAlert("Error", $"{ex.Message}", "OK");
			return false;
		}
	}
	private void btnGuardar_Clicked(object sender, EventArgs e)
	{
		if (bytes == null || ePlacas.Text == null || eMarca.Text == null || eModelo.Text == null || eAño.Text == null || eColor.Text == null || ePrecioDias.Text == null)
		{
			DisplayAlert("Aviso", "Verifica que hayas rellenado todos los campos", "OK");
		}
		else
		{
			var auto = new Autos
			{
				imgAuto = bytes,
				placas = ePlacas.Text,
				marca = eMarca.Text,
				modelo = eModelo.Text,
				año = eAño.Text,
				color = eColor.Text,
				precioxdia = int.Parse(ePrecioDias.Text),
				rentado = false
			};
			var tabbedpage = Application.Current.MainPage as MainPage;
			ListaAutos agregarA = tabbedpage.Children[0] as ListaAutos;
			if (op.VerificarPlacas(ePlacas.Text))
			{
				DisplayAlert("Aviso", "Esas placas ya estan registradas", "OK");
			}
			else
			{
				op.insertarAuto(auto);
				agregarA.CargarAutos();
				ePlacas.Text = string.Empty;
				eMarca.Text = string.Empty;
				eModelo.Text = string.Empty;
				eAño.Text = string.Empty;
				eColor.Text = string.Empty;
				ePrecioDias.Text = string.Empty;
				selecImagen.Source = null;
			}
		}
	}

	private void btnModificar_Clicked(object sender, EventArgs e)
	{
		if (bytes == null || ePlacas.Text == null || eMarca.Text == null || eModelo.Text == null || eAño.Text == null || eColor.Text == null || ePrecioDias.Text == null)
		{
			DisplayAlert("Aviso", "Verifica que hayas rellenado todos los campos", "OK");
		}
		else
		{
			var auto = new Autos
			{
				imgAuto = bytes,
				placas = ePlacas.Text,
				marca = eMarca.Text,
				modelo = eModelo.Text,
				año = eAño.Text,
				color = eColor.Text,
				precioxdia = int.Parse(ePrecioDias.Text)
			};

			op.ModificarAuto(auto);
			var tabbedpage = Application.Current.MainPage as MainPage;
			ListaAutos agregarA = tabbedpage.Children[0] as ListaAutos;
			agregarA.CargarAutos();

			ePlacas.Text = string.Empty;
			eMarca.Text = string.Empty;
			eModelo.Text = string.Empty;
			eAño.Text = string.Empty;
			eColor.Text = string.Empty;
			ePrecioDias.Text = string.Empty;
			selecImagen.Source = null;
		}
	}
}