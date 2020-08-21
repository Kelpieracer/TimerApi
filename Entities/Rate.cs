﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Rate
    {
        public int RateId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime Started { get; set; }
        public DateTime Ended { get; set; }
    }
}
