using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Boats
{
    public class DeleteBoat2Model : PageModel
    {
        private IBoatRepository _bRepo;
        public Boat DeleteBoat2 { get; set; }

        public DeleteBoat2Model(IBoatRepository boatRepository)
        {
            _bRepo = boatRepository;
        }
        public IActionResult OnGet(string sailnumber)
        {
            DeleteBoat2 = _bRepo.SearchBoat(sailnumber);
            return Page();
        }
        public IActionResult OnPostDelete(string sailNumber)
        {
            _bRepo.RemoveBoat(sailNumber);
            return RedirectToPage("Index");
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("Index");
        }
    }
}
