using grow.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grow.Models.ViewModels
{
    public class CreatePlantViewModel
    {
        public Plant Plant { get; set; }
        public List<SelectListItem> Water { get; set; }
        
        public CreatePlantViewModel() { }

        public CreatePlantViewModel(ApplicationDbContext ctx)
        {

            Water = ctx.Water.Select(li => new SelectListItem()
            {
                Text = li.Regularity,
                Value = li.WaterId.ToString()
            }).ToList();
        }
    }
}
