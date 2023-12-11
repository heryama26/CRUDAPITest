using CRUDAPITest.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPITest.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UserController(MyDbContext context)
        {
            _context = context;
        }
        [Route("api/getDataUser/{userid}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> getDataUser(string userid)
        {
            List<UserModel> userItem = new List<UserModel>();
            if (userid.ToLower() == "all")
            {
                userItem = await _context.tbl_user.ToListAsync();

                if (userItem == null || userItem.Count == 0)
                {
                    return NotFound();
                }
                return userItem;
            }
            else
            {
                var getDataById = await _context.tbl_user.FindAsync(int.Parse(userid));
                if (getDataById == null)
                {
                    return NotFound();
                }
                userItem.Add(getDataById);
                return userItem;
            }
        }
        [Route("api/setDataUser")]

        [HttpPost]
        public async Task<ActionResult<UserModel>> setDataUser(UserModel user)
        {
            _context.tbl_user.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(getDataUser), new { userid = user.userid }, user);
        }
        [Route("api/delDataUser/{userid}")]
        [HttpDelete]
        public async Task<IActionResult> delDataUser(int userid)
        {
            var user = await _context.tbl_user.FindAsync(userid);
            if (user == null)
            {
                return NotFound();
            }

            _context.tbl_user.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
