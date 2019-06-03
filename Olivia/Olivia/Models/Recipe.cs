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
        public RecipeDAO RecipeDAO = new RecipeDAO();
        public IngredientDAO IngredientDAO = new IngredientDAO();
        public InstructionDAO InstructionDAO = new InstructionDAO();
        public int Id_Recipe { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Creator { get; set; }
        public int Type { get; set; }
        public int Duration { get; set; }
        public float Calories { get; set; }
        public float Fat { get; set; }
        public float Carbs { get; set; }
        public float Protein { get; set; }
        public float Fiber { get; set; }
        public float Sodium { get; set; }
        public List<RecipeIngredient> Ingredients { get; set; } = Repeated(new RecipeIngredient(), 10);//new List<IngredientRecipe>();
        public List<Instruction> Instructions { get; set; } = Repeated(new Instruction(), 10);//new List<Instruction>();
        public List<string> Warnings { get; set; }
        public static List<T> Repeated<T>(T value, int count)
        {
            List<T> ret = new List<T>(count);
            ret.AddRange(Enumerable.Repeat(value, count));
            return ret;
        }


        public Instruction GetInstructionByPosition(int pos)
        {
            return InstructionDAO.GetInstruction(Id_Recipe, pos);
        }

        public void DeleteIngredients()
        {
            IngredientDAO.DeleteRecipeIngredients(Id_Recipe);
            return;
        }

        public void DeleteInstruction(int position)
        {
            InstructionDAO.DeleteInstruction(Id_Recipe, position);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Recipe objAsPart = obj as Recipe;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        public bool Equals(Recipe other)
        {
            if (other == null) return false;
            return (this.Id_Recipe.Equals(other.Id_Recipe));
        }

        public byte[] GetImage()
        {
            return RecipeDAO.GetRecipeImage(Id_Recipe);
        }

        public void Approve()
        {
            RecipeDAO.ApproveRecipe(Id_Recipe);
        }
    }
}
