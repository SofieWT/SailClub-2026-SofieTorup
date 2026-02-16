using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Boats
{
    public class EditBoatModel : PageModel
    {
        private IBoatRepository bRepo;
        [BindProperty]
        public Boat BoatToUpdate { get; set; }
        public EditBoatModel(IBoatRepository boatRepository)
        {
            bRepo = boatRepository;
        }
        public void OnGet(string sailNumber)
        {
            BoatToUpdate = bRepo.SearchBoat(sailNumber);
        }
        public IActionResult OnPost()
        {
            bRepo.UpdateBoat(BoatToUpdate);
            return RedirectToPage("Index");
        }

        public IActionResult OnPostDelete()
        {
            bRepo.RemoveBoat(BoatToUpdate.SailNumber);
            return RedirectToPage("Index");
        }
        public IActionResult OnpostCancel()
        {
            return RedirectToPage("Index");
        }
    }
}
