using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInstructions.Models
{
    public class RecipeIngredient
    {
        /// <summary>
        /// The refered ingredients object Id (string representation)
        /// </summary>
        public string IngredientId { get; set; }
        /// <summary>
        /// The amount of this ingredient required
        /// </summary>
        public int IngredientAmount { get; set; }
        /// <summary>
        /// The amount suffix, eg. "kg" or "cup"
        /// </summary>
        public string IngredientAmountSuffix { get; set; }

        [BsonIgnore] //ignore this field when serializing(saving). used for view-purposes
        public string IngredientName { get; set; }
    }
}
