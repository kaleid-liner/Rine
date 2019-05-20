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
    public class IndexModel : PageModel
    {
        private readonly RineServer.Models.RineServerContext _context;

        public IndexModel(RineServer.Models.RineServerContext context)
        {
            _context = context;
        }

        public IList<RineMessage> RineMessage { get;set; }

        public async Task OnGetAsync()
        {
            RineMessage = await _context.RineMessage.ToListAsync();
        }
    }
}
