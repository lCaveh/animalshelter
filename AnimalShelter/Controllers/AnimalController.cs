using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;
using System.Collections.Generic;

namespace AnimalShelter.Controllers
{
    public class AnimalsController : Controller
    {

        [HttpGet("/animaltypes/{animaltypeId}/animals/new")]
        public ActionResult New(int animaltypeId)
        {
            AnimalType animalType = AnimalType.Find(animaltypeId);
            return View(animalType);
        }

        [HttpGet("/animaltypes/{animaltypeId}/animals/{animalId}")]
        public ActionResult Show(int animaltypeId, int animalId)
        {
            Animal animal = Animal.Find(animalId);
            Dictionary<string, object> model = new Dictionary<string, object>();
            AnimalType animalType = AnimalType.Find(animaltypeId);
            model.Add("item", animal);
            model.Add("category", animalType);
            return View(model);
        }

        [HttpPost("/animals/delete")]
        public ActionResult DeleteAll()
        {
            Animal.ClearAll();
            return View();
        }

    }
}