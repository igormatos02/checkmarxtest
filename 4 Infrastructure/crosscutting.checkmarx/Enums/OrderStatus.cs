using System;
using System.Collections.Generic;
using System.Text;

namespace crosscutting.checkmarx.Enums
{
    public enum OrderStatus
    {
        None = 0,
        SentToKitchen = 1,
        Preparing = 2,
        ReadyToDeliver = 3,
        Delivered = 4,
        Closed = 5
    }
}
