using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Helpers.Sorting;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Boats
{
    public class IndexModel : PageModel
    {
        private IBoatRepoAsync bRepo;
        public List<Boat> Boats { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }
        public IndexModel(IBoatRepoAsync boatRepository)
        {
            bRepo = boatRepository;
        }
        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(FilterCriteria))
            {
                Boats = await bRepo.FilterBoats(FilterCriteria);
            }
            else
                Boats = await bRepo.GetAllBoats();

            switch (SortBy)
            {
                case "SailNumber":
                    {
                        IComparer<Boat> boatComparer = new BoatCompareBySailNumber();
                        Boats.Sort(boatComparer);
                        break;
                    }
                case "YearOfConstruction":
                    {
                        IComparer<Boat> boatComparer = new BoatCompareByYear();
                        Boats.Sort(boatComparer);
                        break;
                    }
                default:
                    {
                        Boats.Sort();
                        break;
                    }
            }
        }
    }
}
