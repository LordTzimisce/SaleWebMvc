using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models.Enums
{
    public enum SaleStatus : int
    {
        Pending = 1,
        Billed = 2,
        Canceled = 3
    }
}
