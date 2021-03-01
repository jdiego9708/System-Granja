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
    public class CorralRepository
    {
        //Lista para guardar los errores
        List<string> errores = new List<string>();
        //Clase de manejo
        DCorrales DCorrales = new DCorrales();

        public async Task<string> GetCorrales(string tipo_busqueda, string texto_busqueda)
        {
            try
            {
                var (rpta, dt) = await DCorrales.BuscarCorrales(tipo_busqueda, texto_busqueda);
                if (dt != null)
                {
                    List<Corrales> list = dt.AsEnumerable().Select(row => new Corrales(row)).ToList();

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

        public async Task<(string rpta, string modelJson)> InsertCorral(Corrales corral)
        {
            try
            {
                var (rpta, id) = await DCorrales.InsertarCorral(corral);
                if (rpta.Equals("OK"))
                {
                    corral.Id_corral = id;
                    var json = JsonConvert.SerializeObject(corral);
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