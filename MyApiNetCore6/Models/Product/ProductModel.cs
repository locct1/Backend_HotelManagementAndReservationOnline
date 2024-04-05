using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models.CategoryService;

namespace MyApiNetCore6.Models.Service
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public CategoryModel Category { get; set; }
    }
}
