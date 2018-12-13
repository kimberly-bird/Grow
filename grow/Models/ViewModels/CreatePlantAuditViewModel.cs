using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grow.Models.ViewModels
{
    public class CreatePlantAuditViewModel
    {
        public Plant Plant { get; set; }
        public Plant Name { get; set; }
        public PlantAudit PlantAudit { get; set; }

    }
}
