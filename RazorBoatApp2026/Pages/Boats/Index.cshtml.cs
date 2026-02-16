using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Helpers.Sorting;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Boats
{
    public class IndexModel : PageModel
    {
        private IBoatRepository bRepo;
        public List<Boat> Boats { get; set; }
        [BindProperty(SupportsGet =true)]
        public string FilterCriteria { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }
        public IndexModel(IBoatRepository boatRepository)
        {
            bRepo = boatRepository;
        }
        public void OnGet()
        {
            if(!string.IsNullOrEmpty(FilterCriteria))
            {
                Boats = bRepo.FilterBoats(FilterCriteria);
            }
            else
                Boats = bRepo.GetAllBoats();

            switch (SortBy)
            {
                case "Id":
                    {
                        Boats.Sort();
                        break;
                    }
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
                //default
            }
        }
    }
}
