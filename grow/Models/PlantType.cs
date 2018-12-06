using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace grow.Models
{
    public class PlantType
    {
        [Key]
        public int PlantTypeId { get; set; }

        [Required]
        [Display(Name = "Type of Plant")]
        public string Name { get; set; }

        public virtual ICollection<Plant> Plants { get; set; }
    }
}