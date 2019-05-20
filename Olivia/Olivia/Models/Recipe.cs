using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Olivia.Controllers
{
    public class Recipe
    {
        
        public int Id { get; set; }

        [Display(Name="Recipe Name")]
        [Required(ErrorMessage = "Name is necessary")]
        public string Name { get; set; }

        [Display(Name = "Recipe Description")]
        [Required(ErrorMessage = "Description is necessary")]
        public string Description { get; set; }

        public int CreatorId { get; set; }

        [Display(Name = "Recipe Type Code Number")]
        [Required(ErrorMessage = "Type is necessary")]
        public int Type { get; set; }

        [Display(Name = "Recipe Calories")]
        [Required(ErrorMessage = "Calories information is necessary")]
        public float Calories { get; set; }

        [Display(Name = "Recipe Fat Amount")]
        [Required(ErrorMessage = "Fat information is necessary")]
        public float Fat { get; set; }

        [Display(Name = "Recipe Carbs Amount")]
        [Required(ErrorMessage = "Carbs information is necessary")]
        public float Carbs { get; set; }

        [Display(Name = "Recipe Protein Amount")]
        [Required(ErrorMessage = "Protein information is necessary")]
        public float Protein { get; set; }

        [Display(Name = "Recipe Fiber Amount")]
        [Required(ErrorMessage = "Fiber information is necessary")]
        public float Fiber { get; set; }

        [Display(Name = "Recipe Sodium Amount")]
        [Required(ErrorMessage = "Sodium information is necessary")]
        public float Sodium { get; set; }




    }
}