using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MyApiNetCore6.Models.Image
{
    public class UploadSingleFileModel
    {
        [Required(ErrorMessage = "Phải chọn file upload")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "png,jpg,jpeg,gif")]
        [Display(Name = "Chọn file upload")]
        public IFormFile FileUpload { get; set; }
    }
}
