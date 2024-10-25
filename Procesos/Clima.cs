using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;

    public class AllDay
    {
        public string weather { get; set; }
        public int icon { get; set; }
        public double temperature { get; set; }
        public double temperature_min { get; set; }
        public double temperature_max { get; set; }
        public Wind wind { get; set; }
        public CloudCover cloud_cover { get; set; }
        public Precipitation precipitation { get; set; }
    }

    public class CloudCover
    {
        public int total { get; set; }
    }

    public class Current
    {
        public string icon { get; set; }
        public int icon_num { get; set; }
        public string summary { get; set; }
        public double temperature { get; set; }
        public Wind wind { get; set; }
        public Precipitation precipitation { get; set; }
        public int cloud_cover { get; set; }
    }

    public class Daily
    {
        public List<Datum> data { get; set; }
    }

    public class Datum
    {
        public DateTime date { get; set; }
        public string weather { get; set; }
        public int icon { get; set; }
        public string summary { get; set; }
        public double temperature { get; set; }
        public Wind wind { get; set; }
        public CloudCover cloud_cover { get; set; }
        public Precipitation precipitation { get; set; }
        public string day { get; set; }
        public AllDay all_day { get; set; }
        public object morning { get; set; }
        public object afternoon { get; set; }
        public object evening { get; set; }
    }

    public class Hourly
    {
        public List<Datum> data { get; set; }
    }

    public class Precipitation
    {
        public double total { get; set; }
        public string type { get; set; }
    }

    public class Climas
    {
        public string lat { get; set; }
        public string lon { get; set; }
        public int elevation { get; set; }
        public string timezone { get; set; }
        public string units { get; set; }
        public Current current { get; set; }
        public Hourly hourly { get; set; }
        public Daily daily { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int angle { get; set; }
        public string dir { get; set; }
    }

class Clima{

    private static readonly string Language = "en"; 
    private static readonly string ApiKey = "webd4te0poyoa4jocbsgo7gjejh6l1n4rm83vuop"; 

    public static async Task<ServerResult> ObtenerClimaAsync(string placeId){

        // URL base de la API
        //santo-domingo
        

        try{
            var url = $"https://www.meteosource.com/api/v1/free/point?place_id={placeId}&sections=all&timezone=UTC&language={Language}&units=metric&key={ApiKey}";
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            var climas = JsonSerializer.Deserialize<Climas>(json);

            var finai = new List<Dictionary<string, object>>();

            // Crea un diccionario con los valores del objeto climas
            var dic = new Dictionary<string, object>
            {
                { "Latitud", climas.lat },
                { "Longitud", climas.lon },
                { "Elevacion", climas.elevation },
                { "Zona horaria", climas.timezone },
                { "Unidades", climas.units },
                { "Temperatura actual", climas.current.temperature },
                { "Velocidad del viento", climas.current.wind.speed },
                { "Resumen del clima", climas.current.summary }
            };

            finai.Add(dic);

            return new ServerResult(true, "Clima obtenido", finai);
         }
        catch (Exception ex){
             return new ServerResult(false, "Error al obtener el clima: " + ex.Message);
        }

    }

}
