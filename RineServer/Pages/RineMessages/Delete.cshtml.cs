using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RineServer.Models;

namespace RineServer.Pages.RineMessages
{
    public class DeleteModel : PageModel
    {
        private readonly RineServer.Models.RineServerContext _context;

        public DeleteModel(RineServer.Models.RineServerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RineMessage RineMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RineMessage = await _context.RineMessage.FirstOrDefaultAsync(m => m.Id == id);

            if (RineMessage == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RineMessage = await _context.RineMessage.FindAsync(id);

            if (RineMessage != null)
            {
                _context.RineMessage.Remove(RineMessage);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
