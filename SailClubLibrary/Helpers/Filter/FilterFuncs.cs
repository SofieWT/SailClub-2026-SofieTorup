using SailClubLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Helpers.Filter
{
    public class FilterFuncs : IFilterFuncs
    {
        public List<T> FilterFunction<T>(List<T> listOfObjs, params Predicate<T>[] predicates)
        {
            List<T> filterList = new List<T>();
            foreach (T obj in listOfObjs)
            {

                foreach (Predicate<T> predicate in predicates)
                {
                    bool matchesAllPreds = true;
                    if (!predicate(obj))
                    {
                        matchesAllPreds = false;

                    }
                    else if (matchesAllPreds && (!filterList.Contains(obj)))
                    {
                        filterList.Add(obj);

                    }
                }
            }
            return filterList;
        }
    }
}
