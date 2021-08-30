using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInstructions.Models
{
    public class RecipeEditViewModel
    {
        public Recipe Recipe { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
