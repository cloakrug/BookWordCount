using BookWordCount.Interfaces;
using BookWordCount.Models.Dtos;
using BookWordCount.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookWordCount.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserBookStatController : ControllerBase
    {
        private readonly IUserBookStatService _userBookStatService;

        public UserBookStatController(IUserBookStatService userBookStatService)
        {
            _userBookStatService = userBookStatService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllBookStatsForCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var userBookStat = _userBookStatService.GetAllUserBookStats(userId);

            if (userBookStat == null) return NotFound();

            return Ok(userBookStat);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetBookStatsForCurrentUser(string bookId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }
            
            var userBookStat = _userBookStatService.GetUserBookStats(userId, bookId);

            if (userBookStat == null) return NotFound();

            return Ok(userBookStat);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddUserBookStatsDto stats)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            stats.UserId = userId;

            var res = _userBookStatService.AddUserBookStats(stats);


            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return StatusCode(500);
            }
        }

        // TODO: 
        //[HttpPatch]
        //public IActionResult Update(ChangeWordCountDto changeCountDto, string userId)
        //{
        //    if (_wordCountService.UpdateWordCount(changeCountDto, userId))
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return StatusCode(500);
        //    }
        //}

        [Authorize]
        [HttpDelete]
        public IActionResult DeleteStatsForBook(string bookId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            if (_userBookStatService.DeleteStatsForBook(bookId, userId))
            {
                return Ok(true);
            } else
            {
                return StatusCode(500);
            }

        }
    }
}
