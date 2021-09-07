using System;
using System.Collections.Generic;
using System.Text;

namespace crosscutting.checkmarx
{
    public static class AppConstants
    {
        public const string ORDER_QUEUE = "OrderQueue";
        public const string DELEIVERY_QUEUE = "DeliveryQueue";
    }


    public static class MessageErrorConstants
    {
        public const string EMPTY_DISHES_MSG = "Dishes mst be added to the order";
        public const string TABLE_NOT_SET_MSG = "Table must be selected";
        public const string CHEF_NOT_SET_MSG = "Chef must be selected";
        public const string WAITER_NOT_SET_MSG = "Waiter must be selected";
        public const string CHEF_IS_BUSY_MSG = "Chef is Already Busy";
        public const string CHEF_EMPTY_ID_MSG = "Chef Cannot have a Empty ID";
        public const string CHEF_EMPTY_NAME_MSG = "Chef Cannot have a Empty Name";
        public const string CHEF_DUPLICATE_ID_MSG = "This Id already Exists, try another one";
    }
}
