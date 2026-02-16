using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Boats
{
    public class DeleteEventModel : PageModel
    {
        private IBoatRepository bRepo;
        [BindProperty]
        public Boat BoatToBeDeleted { get; set; }
        public DeleteEventModel(IBoatRepository boatRepository)
        {
            bRepo = boatRepository;
        }
        public IActionResult OnGet(string sailNumber)
        {
            BoatToBeDeleted = bRepo.SearchBoat(sailNumber);
            return Page();
        }
        public IActionResult OnPostDelete()
        {
            bRepo.RemoveBoat(BoatToBeDeleted.SailNumber);
            return RedirectToPage("index");
        }
        public IActionResult OnpostCancel()
        {
            return RedirectToPage("Index");
        }

    }
}
