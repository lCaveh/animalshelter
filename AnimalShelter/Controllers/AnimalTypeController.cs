
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;

namespace AnimalShelter.Controllers
{
    public class AnimalTypeController : Controller
    {

        [HttpGet("/animaltypes")]
        public ActionResult Index()
        {
            List<AnimalType> allAnimalTypes = AnimalType.GetAll();
            return View(allAnimalTypes);
        }

        [HttpGet("/animaltypes/new")]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost("/animaltypes")]
        public ActionResult Create(string animaltype)
        {
            AnimalType animalType = new AnimalType(animaltype);
            List<AnimalType> allAnimalTypes = AnimalType.GetAll();
            return View("Index", allAnimalTypes);
        }

        [HttpGet("/animaltypes/{id}")]
        public ActionResult Show(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            AnimalType selectedAnimalType = AnimalType.Find(id);
            List<Animal> animalTypes = selectedAnimalType.GetAnimals();
            model.Add("animaltypes", selectedAnimalType);
            model.Add("animals", animalTypes);
            return View(model);
        }

        // This one creates new Items within a given Category, not new Categories:
        [HttpPost("/animaltypes/{animalTypeId}/animals")]
        public ActionResult Create(string name, string sex, DateTime date, string breed, int animalTypeId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            AnimalType foundAnimalType = AnimalType.Find(animalTypeId);
            Animal newAnimal = new Animal(name, sex, date, breed, animalTypeId);
            foundAnimalType.AddItem(newAnimal);
            newAnimal.Save();
            List<Animal> animalTypeItems = foundAnimalType.GetAnimals();
            model.Add("animals", animalTypeItems);
            model.Add("animaltypes", foundAnimalType);
            return View("Show", model);
        }

    }
}