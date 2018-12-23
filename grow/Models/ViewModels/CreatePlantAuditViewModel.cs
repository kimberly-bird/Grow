using grow.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grow.Models.ViewModels
{
    public class CreatePlantAuditViewModel
    {
        public Plant Plant { get; set; }
        public PlantAudit PlantAudit { get; set; }

        public List<SelectListItem> LightList { get; set; }
        public List<SelectListItem> WaterList { get; set; }


        public CreatePlantAuditViewModel() { }

        public CreatePlantAuditViewModel(ApplicationDbContext ctx)
        {
            LightList = ctx.Light.Select(li => new SelectListItem()
            {
                Text = li.Requirements,
                Value = li.LightId.ToString()
            }).ToList();

            WaterList = ctx.Water.Select(li => new SelectListItem()
            {
                Text = li.Regularity,
                Value = li.WaterId.ToString()
            }).ToList();
        }


    }
}
