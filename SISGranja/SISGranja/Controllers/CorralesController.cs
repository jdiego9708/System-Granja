using SISGranja.Common;
using SISGranja.Common.Models;
using SISGranja.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SISGranja.Controllers
{
    public class CorralesController : Controller
    {
        ApiService apiService = new ApiService();

        // GET: Corrales
        public async Task<ActionResult> ObservarCorrales()
        {
            Response response =
                 await apiService.GetList<Corrales>("https://localhost:44381/", "Corrales/", "ObservarCorrales");

            if (response.IsSuccess)
            {
                return View(response.Result);
            }
            else
            {
                return View();
            }
        }

        // GET: Corrales/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Corrales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Corrales/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                Corrales corral = new Corrales
                {
                    Nombre_corral = collection.Get("Nombre_corral"),
                    Descripcion_corral = collection.Get("Descripcion_corral"),
                    Estado_corral = "ACTIVO",
                };

                Response response =
                 await apiService.Post<Corrales>("https://localhost:44381/", "Corrales/", "CrearCorral", corral);

                if (response.IsSuccess)
                {
                    return View(response.Result);
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Corrales/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Corrales/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Corrales/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Corrales/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
