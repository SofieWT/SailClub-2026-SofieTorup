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
            //foreach (T item in listOfObjs.FindAll(pred))
            //{
            //    filterList.Add(item);
            //}
            foreach (T obj in listOfObjs)
            {
                bool matchesAllPreds = true;
                foreach (Predicate<T> predicate in predicates)
                {
                    if (!predicate(obj))
                    {
                        matchesAllPreds = false;
                        break;
                    }
                    else if(matchesAllPreds)
                    { 
                        filterList.Add(obj);

                    } 
                }

            }
            return filterList;
        }
    }
}
