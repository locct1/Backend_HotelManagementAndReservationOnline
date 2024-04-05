using System.ComponentModel.DataAnnotations;

namespace MyApiNetCore6.Models.CategoryService
{
    public class AddCategoryModel
    {
        public string Name { get; set; }
        public int HotelId { get; set; }
    }
}
