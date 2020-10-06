using System.Collections.Specialized;
using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
      List<Animal> model = new List<Animal>{};
      List<Animal> unsortedList = _db.Animals.ToList();
      switch (place)
      {
        case "Breed":
          model = unsortedList.OrderBy(o=>o.Breed).ThenBy(o => o.Name).ToList();
          break;
        case "Name":
          model = unsortedList.OrderBy(o=>o.Name).ThenBy(o => o.Breed).ToList();
          break;
        case "DateAdmitted":
          model = unsortedList.OrderBy(o=>o.DateAdmitted).ToList();
          break;
        default:
          model = unsortedList;
          break;
      }
      return View(model);
    }
    // model.Sort((x, y) => string.Compare(x.Name, y.Name));
//     public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> enumerable, string sortColumn)
// {
//     var param = Expression.Parameter(typeof(T), "x");
//     var mySortExpression = Expression.Lambda<Func<T, object>>(Expression.Property(param, sortColumn), param);
//     return enumerable.OrderBy(mySortExpression);;
//  }

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
  }
}