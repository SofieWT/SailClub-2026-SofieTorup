using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Helpers.Sorting;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Members
{
    public class IndexModel : PageModel
    {
        private IMemberRepository mRepo;
        public List<Member> Members { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }
        public IndexModel(IMemberRepository memberRepository)
        {
            mRepo = memberRepository;
        }
        public void OnGet()
        {
            if(!string.IsNullOrEmpty(FilterCriteria))
            {
                Members = mRepo.FilterNameMembers(FilterCriteria);
            }
            else
                Members = mRepo.GetAllMembers();


            switch (SortBy)
            {
                case "Id":
                    {
                        Members.Sort();
                        break;
                    }
                case "FirstName":
                    {
                        IComparer<Member> memberComparer = new MemberCompareByFirstName();
                        Members.Sort(memberComparer);
                        break;
                    }
                case "SurName":
                    {
                        IComparer<Member> memberSurNameComparer = new MemberCompareBySurName();
                        Members.Sort(memberSurNameComparer);
                        break;
                    }

                case "City":
                    {
                        IComparer<Member> memberCityComparer = new MemberCompareByCity();
                        Members.Sort(memberCityComparer);
                        break;
                    }
                //default " " :
                //    {
                //        break;
                //    }
            }
        }

    }
}
