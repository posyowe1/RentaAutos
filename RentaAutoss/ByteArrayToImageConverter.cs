using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentaAutoss
{
	public class ByteArrayToImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is byte[] byteArray)
			{
				try
				{
					// Convertir el arreglo de bytes a una imagen
					ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(byteArray));
					return imageSource;
				}
				catch (Exception ex)
				{
					// Manejar cualquier error durante la conversión
					Console.WriteLine($"Error al convertir el byte array a imagen: {ex.Message}");
					return null;
				}
			}

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
