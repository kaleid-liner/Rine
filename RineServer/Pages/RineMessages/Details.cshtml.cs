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
    public class DetailsModel : PageModel
    {
        private readonly RineServer.Models.RineServerContext _context;

        public DetailsModel(RineServer.Models.RineServerContext context)
        {
            _context = context;
        }

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
    }
}
