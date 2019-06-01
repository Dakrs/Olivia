using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Olivia.Models
{
    public class RecipeIngredient
    {
        public int Id_Ingredient { get; set; }
        public string Name { get; set; }
        public float Quantity { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
    }
}
