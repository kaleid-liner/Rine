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
    public class IndexModel : PageModel
    {
        private readonly RineServer.Models.RineServerContext _context;

        public IndexModel(RineServer.Models.RineServerContext context)
        {
            _context = context;
        }

        public IList<RineUser> RineUser { get;set; }

        public async Task OnGetAsync()
        {
            RineUser = await _context.RineUser.ToListAsync();
        }
    }
}
