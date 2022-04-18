using BookWordCount.Interfaces;
using BookWordCount.Models.Dtos;
using BookWordCount.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookWordCount.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class WordCountController : ControllerBase
    {
        private readonly IWordCountService _wordCountService;

        public WordCountController(IWordCountService wordCountService)
        {
            _wordCountService = wordCountService;
        }

        [HttpGet("~/[action]")]
        public IActionResult GetCurrentUserWordCounts(int userId)
        {
            // TODO: use user ID from token sub
            var userWordCounts = _wordCountService.GetUserWordCounts(userId);
            
            if (userWordCounts.Any())
            {
                return Ok(userWordCounts);
            } else
            {
                return NotFound();
            }
        }
        
        [HttpGet]
        public IActionResult GetByUser(int userId, int bookId)
        {
            var wordCount = _wordCountService.GetUserWordCount(userId, bookId);

            if (wordCount != null)
            {
                return Ok(wordCount);
            } else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Add(ChangeWordCountDto changeCountDto, int userId)
        {
            if (_wordCountService.AddWordCount(changeCountDto, userId))
            {
                return Ok();
            } else
            {
                return StatusCode(500);
            }
        }
        
        [HttpPatch]
        public IActionResult Update(ChangeWordCountDto changeCountDto, int userId)
        {
            if (_wordCountService.UpdateWordCount(changeCountDto, userId))
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int wordCountId)
        {
            if (_wordCountService.DeleteWordCount(wordCountId))
            {
                return Ok();
            } else
            {
                return StatusCode(500);
            }
        }
    }
}
