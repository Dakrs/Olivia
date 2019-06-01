using Olivia.DataAccess;
using System;
namespace Olivia.Models
{
    public class Ingredient
    {
        public IngredientDAO IngredientDAO = new IngredientDAO();
        public int Id_Ingredient { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }


        public RecipeIngredient FindRecipeUsage(int id_receita)
        {
            RecipeIngredient data;

            data = IngredientDAO.GetRecipeIngredient(id_receita, Id_Ingredient);

            return data;
        }
    }
}
