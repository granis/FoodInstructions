using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInstructions.Models
{
    public class Ingredient
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// The ingredients taste, is it bitter, salt, umami, sweet, sour?
        /// </summary>
        public string Taste { get; set; }
    }
}
