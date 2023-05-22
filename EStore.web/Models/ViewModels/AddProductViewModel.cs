using System.ComponentModel.DataAnnotations;

namespace EStore.web.Models.ViewModels
{
    public class AddProductViewModel
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string PicURL { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public Guid Owner { get; set; }
    }
}
