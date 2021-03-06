﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Accounts;
using WebApi.Models.Projects;
using WebApi.Models.Rates;

namespace WebApi.Models.Customers
{
    public class CustomerResponse : CommonResponses
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public List<ProjectResponse> Projects { get; set; }
        public List<RateResponse> Rates { get; set; }
        public AccountResponse Manager { get; set; }
    }
}
