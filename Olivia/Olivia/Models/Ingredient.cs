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
        public int Id_Recipe { get; set; }      //only used when referring to specific usage in a recipe
        public float Quantity { get; set; }     //only used when referring to specific usage in a recipe
        public string Unit { get; set; }        //only used when referring to specific usage in a recipe


        public Ingredient FindRecipeUsage(int id_receita)
        {
            Ingredient data;

            data = IngredientDAO.GetRecipeIngredient(id_receita, Id_Ingredient);

            return data;
        }
    }
}
