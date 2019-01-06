using grow.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grow.Models.ViewModels
{
    public class IndexWaterViewModel
    {
        public IEnumerator<KeyValuePair<Water, Plant>> filteredWater { get; set; }
        public List<Water> allWater { get; set; }
        public Plant Plant { get; set; }

        public IndexWaterViewModel() {}

        public IndexWaterViewModel(ApplicationDbContext ctx)
        {
            var allWater = ctx.Water
                .Include(pa => pa.PlantAudits)
                .Include(p => p.Plants)
                .ToList();

            Dictionary<Water, Plant> filteredWater = new Dictionary<Water, Plant>();

            foreach (var w in allWater)
            {
                w.PlantAudits.OrderBy(d => d.DateCreated);
                foreach (var pa in w.PlantAudits)
                {
                    if (!filteredWater.ContainsKey(pa.Water))
                    {
                        filteredWater.Add(pa.Water, pa.Plant);
                    }
                }
            }
        }
        
    }

}
