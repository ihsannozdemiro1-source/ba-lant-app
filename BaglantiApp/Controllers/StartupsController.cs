using BaglantiApp.Data;
using BaglantiApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaglantiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartupsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StartupsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Startups (Tüm girişimleri listeler)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Startup>>> GetStartups()
        {
            return await _context.Startups.ToListAsync();
        }

        // POST: api/Startups (Yeni girişim ekler)
        [HttpPost]
        public async Task<ActionResult<Startup>> PostStartup(Startup startup)
        {
            _context.Startups.Add(startup);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStartups), new { id = startup.Id }, startup);
        }
    }
}
