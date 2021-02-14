using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class CarDetailDto : IDto
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string Brand{ get; set; }
        public string Color{ get; set; }
        public decimal DailyPrice { get; set; }

    }
}
