using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ProfitOrder
{
    class DiscontinuedItem
    {
        [PrimaryKey]
        public int ItemNo { get; set; }
    }
}
