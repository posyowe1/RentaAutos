//using Android.Views;
using Microsoft.Maui.Controls.Internals;
using System.Collections.ObjectModel;
using System.Globalization;

namespace RentaAutoss;

public partial class ListaAutos : ContentPage
{
	public ListaAutos()
	{
		InitializeComponent();
		this.IconImageSource = "icono3.png";
		CargarAutos();
	}
	Operaciones op = new Operaciones();
	AgregarAutos agrega = new AgregarAutos();
	Autos selectedAuto;
	public ObservableCollection<Autos> AutosList { get; set; }
	public async void CargarAutos()
	{
		try
		{
			var autos = await op.ObtenerTodosAutos();
			AutosList = new ObservableCollection<Autos>();
			autosListView.ItemsSource = AutosList;
			foreach (var auto in autos)
			{
				AutosList.Add(auto);
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"Ocurrió un error al cargar los datos: {ex.Message}", "Aceptar");
		}
	}

	private void btnActualizar_Clicked(object sender, EventArgs e)
	{
		if (agrega != null && selectedAuto != null)
		{
			var tabbedpage = Application.Current.MainPage as MainPage;
			AgregarAutos agregarA = tabbedpage.Children[1] as AgregarAutos;
			if (agregarA.PasarAuto(selectedAuto))
			{
				DisplayAlert("Alerta", "Si", "OK");
			}
			else
			{
				DisplayAlert("Alerta", "No", "OK");
			}
		}
	}
	private void autosListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		if (e.SelectedItem != null)
		{
			selectedAuto = e.SelectedItem as Autos;
		}
	}
	private void btnRentar_Clicked(object sender, EventArgs e)
	{
		if (agrega != null && selectedAuto != null)

		{
			var tabbedpage = Application.Current.MainPage as MainPage;
			RentarAutos agregarA = tabbedpage.Children[2] as RentarAutos;
			if (agregarA.PasarAutoRenta(selectedAuto))
			{
				DisplayAlert("Alerta", "Si", "OK");
			}
			else
			{
				DisplayAlert("Alerta", "No", "OK");
			}
		}
	}
}