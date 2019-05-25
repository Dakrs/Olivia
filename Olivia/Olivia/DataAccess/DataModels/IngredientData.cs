using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Olivia.DataAccess;

namespace Olivia.Models
{
    public class IngredientData
    {

        public int Id_Ingredient { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }


        public IngredientData(Ingredient ingredient)
        {
            Id_Ingredient = ingredient.Id_Ingredient;
            Name = ingredient.Name;
            Category = ingredient.Category;
        }
    }
}
