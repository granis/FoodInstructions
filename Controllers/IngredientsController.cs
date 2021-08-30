using FoodInstructions.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInstructions.Controllers
{
    public class IngredientsController : Controller
    {
        public IActionResult Index()
        {
            var mongoClient = new MongoClient();
            var database = mongoClient.GetDatabase("food_instructions");
            var collection = database.GetCollection<Ingredient>("ingredients");

            List<Ingredient> ingredientsInDb = collection.Find(r => true).ToList();

            return View(ingredientsInDb);
        }

        /// <summary>
        /// Create page (form) for a new ingredient
        /// </summary>
        /// <returns>Create form view</returns>
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Ingredient ingredient)
        {
            var mongoClient = new MongoClient();
            var database = mongoClient.GetDatabase("food_instructions");
            var collection = database.GetCollection<Ingredient>("ingredients");

            collection.InsertOne(ingredient);

            return Redirect("/Ingredients");
        }

        public IActionResult Show(string Id)
        {
            ObjectId objId = new ObjectId(Id);
            var mongoClient = new MongoClient();
            var database = mongoClient.GetDatabase("food_instructions");
            var collection = database.GetCollection<Ingredient>("ingredients");

            Ingredient ingredient = collection.Find(i => i.Id == objId).FirstOrDefault();

            return View(ingredient);
        }

        public IActionResult Edit(string Id)
        {
            ObjectId objId = new ObjectId(Id);
            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("food_instructions");
            var collection = database.GetCollection<Ingredient>("ingredients");
            Ingredient ingredient = collection.Find(i => i.Id == objId).FirstOrDefault();

            return View(ingredient);
        }

        [HttpPost]
        public IActionResult Edit(string Id, Ingredient ingredient)
        {
            ObjectId objId = new ObjectId(Id);
            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("food_instructions");
            var collection = database.GetCollection<Ingredient>("ingredients");

            ingredient.Id = objId;
            collection.ReplaceOne(b => b.Id == objId, ingredient);

            return Redirect("/Ingredients/Show/" + Id);
        }

        [HttpPost]
        public IActionResult Delete(string Id)
        {
            ObjectId objId = new ObjectId(Id);
            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("food_instructions");
            var collection = database.GetCollection<Ingredient>("ingredients");
            collection.DeleteOne(i => i.Id == objId);

            return Redirect("/Ingredients");
        }
    }
}
