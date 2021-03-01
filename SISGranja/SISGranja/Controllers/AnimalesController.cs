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
    public class AnimalesController : Controller
    {
        ApiService apiService = new ApiService();

        // GET: Animales
        public async Task<ActionResult> ObservarAnimales()
        {
            Response response = 
                await apiService.GetList<Animales>("https://localhost:44381/", "Animales/", "ObservarAnimales");

            if (response.IsSuccess)
            {
                return View(response.Result);
            }
            else
            {
                return View();
            }
        }

        // GET: Animales/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Animales/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Animales/Details/5
        public ActionResult AsignarCorral(Animales animal)
        {
            Detalle_animal_corral de = new Detalle_animal_corral
            {
                Animal = animal,
                Id_animal = animal.Id_animal,
                Id_corral = 0,
            };

            return View(de);
        }

        // POST: Animales/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            try
            {
                Animales animal = new Animales
                {
                    Nombre_animal = collection.Get("Nombre_animal"),
                    Descripcion_animal = collection.Get("Descripcion_animal"),
                    Estado_animal = "ACTIVO",
                };

                Response response =
                 await apiService.Post<Animales>("https://localhost:44381/", "Animales/", "Create", animal);

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

        // GET: Animales/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Animales/Edit/5
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

        // GET: Animales/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Animales/Delete/5
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
