using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Members
{
    public class EditMemberModel : PageModel
    {
        private IMemberRepository mRepo;
        [BindProperty]
        public Member MemberToBeUpdated { get; set; }
        public EditMemberModel(IMemberRepository memberRepository)
        {
            mRepo = memberRepository;
        }
        public void OnGet(string phoneNumber)
        {
            MemberToBeUpdated = mRepo.SearchMember(phoneNumber);
        }
        public IActionResult OnPost()
        {
            mRepo.UpdateMember(MemberToBeUpdated);
            return RedirectToPage("Index");
        }
        public IActionResult OnPostDelete()
        {
            mRepo.RemoveMember(MemberToBeUpdated);
            return RedirectToPage("Index");
        }
        public IActionResult OnPostCancel()
        {
            return RedirectToPage("Index");
        }
    }
}
