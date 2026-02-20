using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Bookings
{
    public class CreateBookingModel : PageModel
    {
        IBoatRepository _bRepo;
        IMemberRepository _mRepo;
        IBookingRepository _boRepo;
        [BindProperty]
        public Boat BookedBoat { get; set; }
        [BindProperty]
        public string Destination { get; set; }
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty]
        public DateTime StartDate { get; set; }
        [BindProperty]
        public DateTime EndDate { get; set; }

        public CreateBookingModel(IBoatRepository boatRepository, IMemberRepository memberRepository, IBookingRepository bookingRepository)
        {
            _bRepo = boatRepository;
            _mRepo = memberRepository;
            _boRepo = bookingRepository;
        }

        public IActionResult OnGet(string sailNumber)
        {
            BookedBoat = _bRepo.SearchBoat(sailNumber);
            return Page();
        }

        public IActionResult OnPostBook()
        {
            List<Booking> activeBooking = new List<Booking>();

            Member member = _mRepo.SearchMember(PhoneNumber);
            Booking newBooking = new Booking(Id, StartDate, EndDate, Destination, member, BookedBoat);
            _boRepo.AddBooking(newBooking);

            activeBooking.Add(newBooking);
            return RedirectToPage("Index");
        }
        public IActionResult OnPostCancel()
        {
            return RedirectToPage("ChooseBoat");
        }
    }
}
