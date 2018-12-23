using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace grow.Models
{
    public class Light
    {
        [Key]
        public int LightId { get; set; }

        [Required]
        [Display(Name = "Light Requirements")]
        public string Requirements { get; set; }

        public virtual ICollection<PlantAudit> PlantAudits { get; set; }
    }
}
