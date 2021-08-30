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
    public class RecipesController : Controller
    {
        public IActionResult Index()
        {
            var mongoClient = new MongoClient();
            var database = mongoClient.GetDatabase("food_instructions");
            var collection = database.GetCollection<Recipe>("recipes");

            List<Recipe> recipesInDb = collection.Find(r => true).ToList();

            return View(recipesInDb);
        }

        public IActionResult Create()
        {
            var mongoClient = new MongoClient();
            var database = mongoClient.GetDatabase("food_instructions");
            var collection = database.GetCollection<Ingredient>("ingredients");

            List<Ingredient> ingredientsInDb = collection.Find(r => true).ToList();
            return View(ingredientsInDb);
        }

        [HttpPost]
        public IActionResult Create(Recipe recipe)
        {
            var mongoClient = new MongoClient();
            var database = mongoClient.GetDatabase("food_instructions");
            var collection = database.GetCollection<Recipe>("recipes");
            collection.InsertOne(recipe);

            return Redirect("/Recipes");
        }

        public IActionResult Show(string Id)
        {
            ObjectId objId = new ObjectId(Id);
            var mongoClient = new MongoClient();
            var database = mongoClient.GetDatabase("food_instructions");
            var collection = database.GetCollection<Recipe>("recipes");
            var ingredientsCollection = database.GetCollection<Ingredient>("ingredients");

            Recipe recipe = collection.Find(r => r.Id == objId).FirstOrDefault();
            foreach(RecipeIngredient ri in recipe.RecipeIngredients)
            {
                ObjectId ingredientId = new ObjectId(ri.IngredientId);
                Ingredient ingredient = ingredientsCollection.Find(i => i.Id == ingredientId).FirstOrDefault();
                ri.IngredientName = ingredient.Name;
            }

            return View(recipe);
        }

        public IActionResult Edit(string Id)
        {
            ObjectId objId = new ObjectId(Id);
            var mongoClient = new MongoClient();
            var database = mongoClient.GetDatabase("food_instructions");
            var collection = database.GetCollection<Recipe>("recipes");
            var ingredientsCollection = database.GetCollection<Ingredient>("ingredients");
            List<Ingredient> ingredientsInDb = ingredientsCollection.Find(r => true).ToList();

            Recipe recipe = collection.Find(r => r.Id == objId).FirstOrDefault();
            foreach (RecipeIngredient ri in recipe.RecipeIngredients)
            {
                ObjectId ingredientId = new ObjectId(ri.IngredientId);
                Ingredient ingredient = ingredientsCollection.Find(i => i.Id == ingredientId).FirstOrDefault();
                ri.IngredientName = ingredient.Name;
            }

            RecipeEditViewModel view = new RecipeEditViewModel();
            view.Ingredients = ingredientsInDb;
            view.Recipe = recipe;

            return View(view);
        }

        
        [HttpPost]
        public IActionResult Edit(string Id, Recipe recipe)
        {
            ObjectId objId = new ObjectId(Id);
            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("food_instructions");
            var collection = database.GetCollection<Recipe>("recipes");

            recipe.Id = objId;
            collection.ReplaceOne(b => b.Id == objId, recipe);

            return Redirect("/Recipes/Show/" + Id);
        }


        [HttpPost]
        public IActionResult Delete(string Id)
        {
            ObjectId objId = new ObjectId(Id);
            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("food_instructions");
            var collection = database.GetCollection<Recipe>("recipes");
            collection.DeleteOne(i => i.Id == objId);

            return Redirect("/Recipes");
        }
    }
}
