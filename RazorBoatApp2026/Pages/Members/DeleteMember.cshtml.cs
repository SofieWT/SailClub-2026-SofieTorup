using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Members
{
    public class DeleteMemberModel : PageModel
    {
        IMemberRepository mRepo;
        [BindProperty]
        public Member MemberToBeDeleted { get; set; }
        public DeleteMemberModel(IMemberRepository memberRepository)
        {
            mRepo = memberRepository;
        }
        public IActionResult OnGet(string phoneNumber)
        {
            MemberToBeDeleted = mRepo.SearchMember(phoneNumber);
            return Page();
        }

        public IActionResult OnPostDelete(Member member)
        {
            mRepo.RemoveMember(MemberToBeDeleted);
            return RedirectToPage("Index");
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("Index");
        }
    }
}
