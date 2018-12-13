using grow.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grow.Models.ViewModels
{
    public class DetailsPlantViewModel
    {
        public Plant Plant { get; set; }
        public List<PlantAudit> PlantAudit { get; set; }

        public DetailsPlantViewModel() { }

        public DetailsPlantViewModel(ApplicationDbContext ctx)
        {
            PlantAudit = ctx.PlantAudit.ToList();
            Plant = Plant;
        }
    }
}
