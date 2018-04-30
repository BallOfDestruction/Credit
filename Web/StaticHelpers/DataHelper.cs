using System;
using System.Collections.Generic;
using Web.Models;

namespace Web.StaticHelpers
{
    public static class DataHelper
    {
        public static IEnumerable<IEntity> GetList(Context context, Type type)
        {
            dynamic objdect = Activator.CreateInstance(type);
            return context.GetDbSet(objdect);
        }
    }
}
