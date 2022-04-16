using BookWordCount.Models.Dtos;
using BookWordCount.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookWordCount.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class WordCountController : ControllerBase
    {
        private readonly WordCountService _wordCountService;

        public WordCountController(WordCountService wordCountService)
        {
            _wordCountService = wordCountService;
        }

        [HttpGet("~/[action]")]
        public IActionResult GetCurrentUserWordCounts()
        {
            // TODO: use user ID from token sub
            var userWordCounts = _wordCountService.GetUserWordCounts(1);
            
            if (userWordCounts.Any())
            {
                return Ok(userWordCounts);
            } else
            {
                return NotFound();
            }
        }
        
        [HttpGet]
        public IActionResult GetByUser()
        {
            var wordCount = _wordCountService.GetUserWordCount(1, 2);

            if (wordCount != null)
            {
                return Ok(wordCount);
            } else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Add(ChangeWordCountDto changeCountDto)
        {
            if (_wordCountService.AddWordCount(changeCountDto, 1))
            {
                return Ok();
            } else
            {
                return StatusCode(500);
            }
        }
        
        [HttpPatch]
        public IActionResult Update(ChangeWordCountDto changeCountDto)
        {
            if (_wordCountService.UpdateWordCount(changeCountDto, 1))
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
