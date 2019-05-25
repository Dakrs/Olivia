using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Olivia.DataAccess;

namespace Olivia.Models
{
    public class Recipe
    {

        private Dictionary<Ingredient, Quantity> ingredients;
        private List<Instruction> instructions;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatorId { get; set; }
        public int Type { get; set; }
        public float Calories { get; set; }
        public float Fat { get; set; }
        public float Carbs { get; set; }
        public float Protein { get; set; }
        public float Fiber { get; set; }
        public float Sodium { get; set; }


        public List<Instruction> GetInstructions()
        {
            return this.instructions;
        }

        public Dictionary<Ingredient,Quantity> GetIngredients()
        {
            return this.ingredients;
        }

        public void refreshInstructions()
        {
            InstructionDAO aux = new InstructionDAO();
            this.instructions = aux.InstructionsForRecipe(this.Id);

            IngredientDAO aux2 = new IngredientDAO();
            this.ingredients = aux2.IngredientsForRecipe(this.Id);
        }
    }

    public class Quantity
    {
        public float Amount { get; set; }
        public string Unity { get; set; }
    }
}
