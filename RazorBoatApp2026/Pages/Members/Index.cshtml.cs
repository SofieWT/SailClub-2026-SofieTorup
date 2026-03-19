using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Helpers.Filter;
using SailClubLibrary.Helpers.Sorting;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Members
{
    public class IndexModel : PageModel
    {
        private IMemberRepository mRepo;
        private IFilterFuncs _filterFunc;
        public List<Member> Members { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterBy { get; set; }
        [BindProperty(SupportsGet = true)]
        public MemberType? SelectedMemberType { get; set; }

        public IndexModel(IMemberRepository memberRepository, IFilterFuncs filterFunc)
        {
            mRepo = memberRepository;
            _filterFunc = filterFunc;
        }
        public void OnGet()
        {
            //VI skal bruge GetAll til medlemmer, som listen, som sendes videre til filter-funktion
            if (!string.IsNullOrEmpty(FilterCriteria))
            {
                var predicates = FilterByPredicate();
                List<Member> tempListOfMembers = _filterFunc.FilterFunction(mRepo.GetAllMembers(), predicates.ToArray());
                if (SelectedMemberType.HasValue)
                {
                    List<Member> membersWithType = new List<Member>();
                    foreach (Member m in tempListOfMembers)
                    {
                        if (m.TheMemberType == SelectedMemberType)
                        {
                            membersWithType.Add(m);
                        }
                    }
                    Members = membersWithType;
                }
                else
                {
                    Members = tempListOfMembers;
                }
            }
            else
            {
                Members = mRepo.GetAllMembers();
            }

            switch (SortBy)
            {
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
                default:
                    {
                        Members.Sort();
                        break;
                    }
            }
        }

        public List<Predicate<Member>> FilterByPredicate()
        {
            List<Predicate<Member>> predicatesList = new List<Predicate<Member>>();
            Predicate<Member> firstNames = m => m.FirstName.ToLower().Contains(FilterCriteria.ToLower());
            Predicate<Member> surNames = m => m.SurName.ToLower().Contains(FilterCriteria.ToLower());
            Predicate<Member> phoneNumbers = m => m.PhoneNumber.Contains(FilterCriteria);
            Predicate<Member> mails = m => m.Mail.ToLower().Contains(FilterCriteria.ToLower());
            Predicate<Member> cities = m => m.City.ToLower().Contains(FilterCriteria.ToLower());
            //if (SelectedMemberType.HasValue)
            //{
                //Predicate<Member> memberType = mt => SelectedMemberType.HasValue && mt.TheMemberType.Equals(SelectedMemberType);
            //}
                
            //predicatesList.Add(memberType);
            switch (FilterBy)
            {
                case "FirstName":
                    {
                        predicatesList.Add(firstNames);
                        break;
                    }
                case "SurName":
                    {
                        predicatesList.Add(surNames);
                        break;
                    }
                case "PhoneNumber":
                    {
                        predicatesList.Add(phoneNumbers);
                        break;
                    }
                case "Mail":
                    {
                        predicatesList.Add(mails);
                        break;
                    }
                case "City":
                    {
                        predicatesList.Add(cities);
                        break;
                    }
                default:
                    {
                        predicatesList.Add(firstNames);
                        predicatesList.Add(surNames);
                        predicatesList.Add(phoneNumbers);
                        predicatesList.Add(mails);
                        predicatesList.Add(cities);
                        break;
                    }
            }
            return predicatesList;
        }
    }
}
