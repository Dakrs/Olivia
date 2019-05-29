using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Olivia.DataAccess;

namespace Olivia.Models
{
    public class RecipeIngredientData
    {

        public int Id_Recipe { get; set; }
        public int Id_Ingredient { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }

    }


}
