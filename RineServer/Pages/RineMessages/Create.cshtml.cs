using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RineServer.Models;

namespace RineServer.Pages.RineMessages
{
    public class CreateModel : PageModel
    {
        private readonly RineServer.Models.RineServerContext _context;

        public CreateModel(RineServer.Models.RineServerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RineMessage RineMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RineMessage.Add(RineMessage);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}