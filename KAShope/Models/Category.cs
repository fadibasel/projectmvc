using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
namespace KAShope.Models
{
    public class Category
    {
        public int Id { get; set; }


        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string Name { get; set; }
        [ValidateNever]
        public List<Product> Products { get; set; } = new List<Product>();

    }
}
