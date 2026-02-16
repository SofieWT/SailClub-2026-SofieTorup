using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Helpers.Sorting
{
    public class MemberCompareByCity : IComparer<Member>
    {
        public int Compare(Member? x, Member? y)
        {
            return x.City.CompareTo(y.City);
        }
    }
}
