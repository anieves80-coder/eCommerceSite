using System.ComponentModel.DataAnnotations;

namespace EStore.web.Models.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }
        
        [Required]
        public string Email { get; set; }
    }
}
