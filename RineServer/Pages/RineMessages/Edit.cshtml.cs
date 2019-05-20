using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RineServer.Models;

namespace RineServer.Pages.RineMessages
{
    public class EditModel : PageModel
    {
        private readonly RineServer.Models.RineServerContext _context;

        public EditModel(RineServer.Models.RineServerContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(RineMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RineMessageExists(RineMessage.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RineMessageExists(int id)
        {
            return _context.RineMessage.Any(e => e.Id == id);
        }
    }
}
