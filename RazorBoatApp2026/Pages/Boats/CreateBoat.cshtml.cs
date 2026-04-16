using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using SailClubLibrary.Exceptions;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Boats
{
    public class CreateBoatModel : PageModel
    {
         private IBoatRepoAsync _repo;
        [BindProperty]
        public Boat NewBoat { get; set; }

        public CreateBoatModel(IBoatRepoAsync boatRepository)
        {
            _repo = boatRepository;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await _repo.AddBoat(NewBoat);
            }
            catch(BoatSailnumberExistsException bex)
            {
                ViewData["ErrorMessage"] = bex.Message;
                return Page();
            }
            catch(SqlException sqlex)
            {
                ViewData["ErrorMessage"] = sqlex.Message;
                return Page();
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}
