using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Interfaces
{
    public interface IFilterFuncs
    {

        List<T> FilterFunction<T>(List<T> objects, params Predicate<T>[] preds);
    }
}
