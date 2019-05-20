using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RineServer.Models;

namespace RineServer.Pages.RineUsers
{
    public class DetailsModel : PageModel
    {
        private readonly RineServer.Models.RineServerContext _context;

        public DetailsModel(RineServer.Models.RineServerContext context)
        {
            _context = context;
        }

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
    }
}
