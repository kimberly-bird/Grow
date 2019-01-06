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
        public Water Water { get; set; }
        public List<Plant> Plants { get; set; }
    }
}
