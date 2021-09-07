using System;
using System.Collections.Generic;
using System.Text;

namespace application.checkmarx.DisplayModels
{
    public class BillDisplay : IResult
    {
        public string Waiter { get; set; }
        public string Chef { get; set; }
        public string Date { get; set; }
        public decimal Total { get; set; }
    }
}
