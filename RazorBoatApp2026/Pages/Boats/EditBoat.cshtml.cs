using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Boats
{
    public class EditBoatModel : PageModel
    {
        private IBoatRepoAsync bRepo;
        [BindProperty]
        public Boat BoatToUpdate { get; set; }
        public EditBoatModel(IBoatRepoAsync boatRepository)
        {
            bRepo = boatRepository;
        }
        public async Task OnGetAsync(string sailNumber)
        {
            BoatToUpdate = await bRepo.SearchBoat(sailNumber);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await bRepo.UpdateBoat(BoatToUpdate);
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            await bRepo.RemoveBoat(BoatToUpdate.SailNumber);
            return RedirectToPage("Index");
        }
        public IActionResult OnpostCancel()
        {
            return RedirectToPage("Index");
        }
    }
}
