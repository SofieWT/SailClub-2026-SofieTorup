using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Boats
{
    public class DeleteBoat2Model : PageModel
    {
        private IBoatRepoAsync _bRepo;
        [BindProperty]
        public Boat DeleteBoat2 { get; set; }

        public DeleteBoat2Model(IBoatRepoAsync boatRepository)
        {
            _bRepo = boatRepository;
        }
        public async Task<IActionResult> OnGet(string sailnumber)
        {
            DeleteBoat2 = await _bRepo.SearchBoat(sailnumber);

            return Page();
        }
        public async Task<IActionResult> OnPostDelete(string sailNumber)
        {
            try
            {
                await _bRepo.RemoveBoat(sailNumber);
                return RedirectToPage("Index");
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
            finally
            {

            }

        }
        public IActionResult OnPost()
        {
            return RedirectToPage("Index");
        }
    }
}
