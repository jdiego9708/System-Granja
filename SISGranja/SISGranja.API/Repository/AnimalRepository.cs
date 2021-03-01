using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SISGranja.API.Core;
using SISGranja.API.Helpers;
using SISGranja.Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SISGranja.API.Repository
{
    public class AnimalRepository
    {
        //Lista para guardar los errores
        List<string> errores = new List<string>();
        //Clase de manejo
        DAnimales DAnimales = new DAnimales();

        public async Task<string> GetAnimals(string tipo_busqueda, string texto_busqueda)
        {
            try
            {
                var (rpta, dt) = await DAnimales.BuscarAnimal(tipo_busqueda, texto_busqueda);
                if (dt != null)
                {
                    List<Animales> list = dt.AsEnumerable().Select(row => new Animales(row)).ToList();

                    return JsonConvert.SerializeObject(list);
                }
                else
                {
                    if (!rpta.Equals("OK"))
                        throw new Exception(rpta);

                    return null;
                }
            }
            catch (Exception ex)
            {
                errores.Add(ex.Message);
                return null;
            }
        }

        public async Task<(string rpta, string modelJson)> InsertAnimal(Animales animal)
        {
            try
            {
                var (rpta, id) = await DAnimales.InsertarAnimal(animal);
                if (rpta.Equals("OK"))
                {
                    animal.Id_animal = id;
                    var json = JsonConvert.SerializeObject(animal);
                    return (rpta, json);
                }
                else
                {
                    if (!rpta.Equals("OK"))
                        throw new Exception(rpta);

                    return (rpta, null);
                }
            }
            catch (Exception ex)
            {
                errores.Add(ex.Message);
                return (ex.Message, null);
            }
        }
    }
}