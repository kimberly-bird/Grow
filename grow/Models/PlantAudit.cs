using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace grow.Models
{
    public class PlantAudit
    {
        [Key]
        public int PlantAuditId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated { get; set; }

        [Required]
        public int PlantId { get; set; }
        public Plant Plant { get; set; }

        [Required]
        public int WaterId { get; set; }
        public Water Water { get; set; }

        [Required]
        public int LightId { get; set; }
        public Light Light { get; set; }

        [Required]
        public bool RequirementsChanged { get; set; }

        [Required]
        public bool InfestationIssue { get; set; }

        public string Notes { get; set; }

        [Display(Name = "Image")]
        public string UpdatedImage { get; set; }

    }
}
