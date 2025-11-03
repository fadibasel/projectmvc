using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace KAShope.Models
{ 
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="product name is required.")]
        [MinLength(3,ErrorMessage = "product name must be at least 3 char long .")]
        [MaxLength(100,ErrorMessage ="product name can`t axceed 100 char")]
        public string Name { get; set; }

        [Required(ErrorMessage = "product description is required.")]
        [MinLength(20, ErrorMessage = "product description must be at least 3 char long .")]
        public string Description { get; set; }
        [Required(ErrorMessage = "product price is required.")]
        [Range(0.01 ,100000 , ErrorMessage=" price must be between 0.01 and 1000")]
        public decimal Price { get; set; }
        [Range(1,5)]
        public int? Rate { get; set; }
        [Required(ErrorMessage = "product quantity is required.")]
        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }
        [ValidateNever]
        //[EmailAddress]
        //[DataType(DataType.Password)]
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
    }

}