using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie24h_API.Data;
using Movie24h_API.Models;

namespace Movie24h_API.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : Controller {

        private readonly Movie24hContext _context;

        public MemberController(Movie24hContext context) {
            _context = context;
        }

        // GET: api/Members
        [HttpGet]
        [Authorize]
        [Route("MembersList")]
        public async Task<ActionResult<List<Member>>> Get() {
            try {
                var members = await _context.Members.ToListAsync();
                if(members.Count == 0) {
                    return NotFound("No data found...");
                }
                return Ok(members);
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Members/12345
        [Authorize]
        [HttpGet]
        [Route("MemberDetail")]
        public async Task<ActionResult<Member>> Get(string id) {
            var member = await _context.Members.FindAsync(id);
            if(member == null) {
                return NotFound();
            }
            return Ok(member);
        }

        // GET: api/CreateMember
        [HttpPost]
        [Route("CreateMember")]
        public async Task<ActionResult<Member>> Post(Member member) {
            member.Id = Guid.NewGuid().ToString();
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }
    }
}
