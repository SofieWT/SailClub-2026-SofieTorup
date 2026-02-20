using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Helpers.Filter
{
    public class FilterFuncs
    {

        static void FilterFunc<T>(List<T> filterList, params Predicate<T>[] preds)
        {
            foreach(Predicate<T> p in preds)
            {

            }

        }

    }
}
