using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalShelter.Controllers
{
  public class AnimalsController : Controller
  {
    private readonly AnimalShelterContext _db;
    public AnimalsController(AnimalShelterContext db)
    {
      _db = db;
    }

    public ActionResult Index(string place)
    {
      List<Animal> model = _db.Animals.ToList();

      if (place == null)
      {
        return View(model);
      }
      else
      {
        model = _db.Animals.OrderBy(a => a.GetType().GetProperty(place).GetValue(a)).ToList();
        return View(model);
      }
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Animal animal)
    {
      _db.Animals.Add(animal);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Animal thisAnimal = _db.Animals.FirstOrDefault(animal => animal.AnimalId == id);
      return View(thisAnimal);

    }

    [HttpPost]
    public ActionResult Search(string name)
    {

      List<Animal> allAnimals = _db.Animals.ToList();
      List<Animal> model = new List<Animal> { };
      foreach (Animal animal in allAnimals)
      {
        string temp = animal.Name.ToUpper();
        string temp2 = name.ToUpper();

        if (temp.Contains(temp2))
        {
          model.Add(animal);
        }
      }
      return View(model);
    }

    public ActionResult SearchRequest()
    {
      return View();
    }

    public ActionResult Edit(int id)
    {
      Animal thisAnimal = _db.Animals.FirstOrDefault(animals => animals.AnimalId == id);
      return View(thisAnimal);
    }

    [HttpPost]
    public ActionResult Edit(Animal animal)
    {
      _db.Entry(animal).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Animal thisAnimal = _db.Animals.FirstOrDefault(animals => animals.AnimalId == id);
      return View(thisAnimal);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Animal thisAnimal = _db.Animals.FirstOrDefault(animals => animals.AnimalId == id);
      _db.Animals.Remove(thisAnimal);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}