using Microsoft.AspNetCore.Mvc;
using WebsiteLicenta.Data;
using WebsiteLicenta.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebsiteLicenta.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public GameController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/Game")]
        public async Task<IActionResult> Post([FromBody] LeaderboardItem model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    model.username = user?.UserName ?? "Anonymous";
                    bool alreadyExists = _context.BoardItem.Any(item => item.username == model.username && item.score == model.score);

                    if (!alreadyExists)
                    {
                        _context.BoardItem.Add(model);
                        await _context.SaveChangesAsync();
                        Console.WriteLine("It worked!!!");
                        return Ok(new { success = true });
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Duplicate score for user not allowed" });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("It failed!");
                    Console.WriteLine(ex.Message);
                    return StatusCode(500, new { success = false, message = "An error occurred while processing your request" });
                }
            }
            else
            {
                return BadRequest(new { success = false, message = "Invalid model state" });
            }
        }
    }
}
