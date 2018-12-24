using grow.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grow.Models.ViewModels
{
    public class DetailsWateringViewModel
    {
        public Water Water { get; set; }
        public List<Plant> Plants { get; set; }

        public DetailsWateringViewModel() { }

        public DetailsWateringViewModel(ApplicationDbContext ctx)
        {
            Water.PlantAudits = ctx.PlantAudit.ToList();

            foreach (PlantAudit pa in Water.PlantAudits)
            {
                Plants = ctx.Plant.Where(p => p.PlantId == pa.PlantId).ToList();

            }
        }
    }
}
