#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsApi.Models;

namespace NewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsItemsController : ControllerBase
    {
        private readonly NewsContext _context;

        public NewsItemsController(NewsContext context)
        {
            _context = context;
        }

        // GET: api/NewsItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsItemDTO>>> GetNewsItems()
        {
            return await _context.NewsItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/NewsItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsItemDTO>> GetNewsItem(long id)
        {
            var newsItem = await _context.NewsItems.FindAsync(id);

            if (newsItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(newsItem);
        }
        // PUT: api/NewsItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNewsItem(long id, NewsItemDTO newsItemDTO)
        {
            if (id != newsItemDTO.Id)
            {
                return BadRequest();
            }

            var newsItem = await _context.NewsItems.FindAsync(id);
            if (newsItem == null)
            {
                return NotFound();
            }

            newsItem.ReportName = newsItemDTO.Name;
            newsItem.IsComplete = newsItemDTO.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!NewsItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        // POST: api/NewsItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NewsItemDTO>> CreateNewsItem(NewsItemDTO newsItemDTO)
        {
            var newsItem = new NewsItem
            {
                IsComplete = newsItemDTO.IsComplete,
                ReportName = newsItemDTO.Name
            };

            _context.NewsItems.Add(newsItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetNewsItem),
                new { id = newsItem.Id },
                ItemToDTO(newsItem));
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsItem(long id)
        {
            var newsItem = await _context.NewsItems.FindAsync(id);

            if (newsItem == null)
            {
                return NotFound();
            }

            _context.NewsItems.Remove(newsItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NewsItemExists(long id)
        {
            return _context.NewsItems.Any(e => e.Id == id);
        }

        private static NewsItemDTO ItemToDTO(NewsItem newsItem) =>
            new NewsItemDTO
            {
                Id = newsItem.Id,
                Name = newsItem.ReportName,
                IsComplete = newsItem.IsComplete
            };
    }
}