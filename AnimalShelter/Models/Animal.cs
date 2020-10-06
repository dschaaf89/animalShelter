using System;
namespace AnimalShelter.Models
{
    public class Animal
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateAdmitted { get; set; }
        public string Breed { get; set; }
        public int AnimalId { get; set; }

        public string Type {get; set;}

    }
}