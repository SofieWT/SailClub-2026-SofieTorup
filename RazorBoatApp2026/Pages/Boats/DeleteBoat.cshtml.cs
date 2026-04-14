using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Boats
{
    public class DeleteEventModel : PageModel
    {
        private IBoatRepoAsync bRepo;
        [BindProperty]
        public Boat BoatToBeDeleted { get; set; }
        public DeleteEventModel(IBoatRepoAsync boatRepository)
        {
            bRepo = boatRepository;
        }
        public async Task<IActionResult> OnGetAsync(string sailNumber)
        {
            BoatToBeDeleted = await bRepo.SearchBoat(sailNumber);
            return Page();
        }
        public async Task<IActionResult> OnPostDelete()
        {
            try
            {
                await bRepo.RemoveBoat(BoatToBeDeleted.SailNumber);
                return RedirectToPage("index");
            }
            catch(SqlException sqlex)
            {
                ViewData["ErrorMessage"] = "The boat has bookings, and can therefore not be deleted";
                return Page();
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }

        }
        public IActionResult OnpostCancel()
        {
            return RedirectToPage("Index");
        }

    }
}
