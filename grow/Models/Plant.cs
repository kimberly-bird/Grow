using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace grow.Models
{
    public class Plant
    {
        [Key]
        public int PlantId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Date Added")]
        public DateTime DateCreated { get; set; }

        [Required]
        [Display(Name = "Plant Name")]
        public string Name { get; set; }

        [Display(Name = "Notes about this plant")]
        public string Notes { get; set; }

        [NotMappedAttribute]
        [FileExtensions(Extensions = "jpg,jpeg")]
        [Display(Name = "Image")]
        public IFormFile InitialImage { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        [Display(Name = "Type of Plant")]
        public int PlantTypeId { get; set; }

        [Display(Name = "Type of Plant")]
        public PlantType PlantType { get; set; }
    }
}
