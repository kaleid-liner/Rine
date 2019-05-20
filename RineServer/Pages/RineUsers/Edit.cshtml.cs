using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RineServer.Models;

namespace RineServer.Pages.RineUsers
{
    public class EditModel : PageModel
    {
        private readonly RineServer.Models.RineServerContext _context;

        public EditModel(RineServer.Models.RineServerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RineUser RineUser { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RineUser = await _context.RineUser.FirstOrDefaultAsync(m => m.Id == id);

            if (RineUser == null)
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

            _context.Attach(RineUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RineUserExists(RineUser.Id))
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

        private bool RineUserExists(int id)
        {
            return _context.RineUser.Any(e => e.Id == id);
        }
    }
}
