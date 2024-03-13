using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.Maui.Controls.PlatformConfiguration;
//using Windows.Storage;

namespace RentaAutoss
{
    class Operaciones
    {
        private MongoClient mongo;
        private IMongoCollection<Autos> RegistroAutos;
		public Operaciones()
        {
			mongo = new MongoClient("mongodb://192.168.43.86:27017");
			var basedatos = mongo.GetDatabase("RentaAutos");
			RegistroAutos = basedatos.GetCollection<Autos>("Autos");
		}
        public void insertarAuto(Autos autos)
        {
            RegistroAutos.InsertOne(autos);
        }
		public void ModificarAuto(Autos mautos)
		{
			var filter = Builders<Autos>.Filter.Eq(x => x.placas, mautos.placas);
			var update = Builders<Autos>.Update
					.Set(x => x.marca, mautos.marca)
					.Set(x => x.modelo, mautos.modelo)
					.Set(x => x.año, mautos.año)
					.Set(x => x.color, mautos.color)
					.Set(x => x.precioxdia, mautos.precioxdia);
			var options = new UpdateOptions { IsUpsert = false }; 

			RegistroAutos.UpdateOne(filter, update, options);
		}
		public void ModificarAutoRentado(Autos mautos)
		{
			var filter = Builders<Autos>.Filter.Eq(x => x.placas, mautos.placas);
			var update = Builders<Autos>.Update.Set(x => x.rentado, mautos.rentado);

			RegistroAutos.UpdateOne(filter, update);
		}
		public async Task<List<Autos>> ObtenerTodosAutos()
		{
			var filter = Builders<Autos>.Filter.Eq("rentado", false);
			var autos = await RegistroAutos.Find(filter).ToListAsync();
			return autos;
		}
		public bool VerificarPlacas(string autoPlaca)
		{
			var filter = Builders<Autos>.Filter.Eq(x => x.placas, autoPlaca);
			var count = RegistroAutos.CountDocuments(filter);

			if(count > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

    public class Autos
    {
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		public byte[] imgAuto { get; set; }
		public string placas {  get; set; }
        public string marca { get; set; }
        public string modelo {  get; set; }
        public string año { get; set; }
        public string color {  get; set; }
        public int precioxdia { get; set; }
		public bool rentado { get; set; }
    }
}
