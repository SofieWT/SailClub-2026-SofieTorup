using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Boats
{
    public class EditBoat2Model : PageModel
    {
        private IBoatRepository _bRepo;
        [BindProperty]
        public Boat BoatToUpdate2 { get; set; }
        public EditBoat2Model(IBoatRepository boatRepository)
        {
            _bRepo = boatRepository;
        }
        public void OnGet(string sailNumber)
        {
            BoatToUpdate2 = _bRepo.SearchBoat(sailNumber);
        }

        public IActionResult OnPost()
        {
            _bRepo.UpdateBoat(BoatToUpdate2);
            return RedirectToPage("Index");
        }
        public IActionResult OnPostDelete()
        {
            _bRepo.RemoveBoat(BoatToUpdate2.SailNumber);
            return RedirectToPage("Index");
        }
    }
}
