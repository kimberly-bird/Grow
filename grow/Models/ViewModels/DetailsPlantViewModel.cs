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
        public Light Light { get; set; }
        public Water Water { get; set; }
        public List<PlantAudit> PlantAudit { get; set; }

        public DetailsPlantViewModel() { }

        public DetailsPlantViewModel(ApplicationDbContext ctx)
        { 
            PlantAudit = ctx.PlantAudit.ToList();

            foreach (PlantAudit pa in PlantAudit)
            {
                pa.Light = ctx.Light.Where(l => l.LightId == pa.LightId).FirstOrDefault();
                pa.Water = ctx.Water.Where(w => w.WaterId == pa.WaterId).FirstOrDefault();
            
            }
        }
    }
}
