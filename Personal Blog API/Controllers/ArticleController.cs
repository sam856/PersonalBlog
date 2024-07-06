using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personal_Blog_API.Models;

namespace Personal_Blog_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ArticleController : ControllerBase
    {
        private readonly AppBbContext _context;
      
        public ArticleController(AppBbContext context)
        {
            _context = context;
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
        [HttpGet]
        public async Task<ActionResult> GetArticles()
        {
            return Ok (await _context.Articles.ToListAsync());
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult> GetArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return Ok (article);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int id, Article article)
        {
                if (!ArticleExists(id))
                {
                    return NotFound();
                }
                else
                {

                var Oldarticle = await _context.Articles.FindAsync(id);
                     Oldarticle.Title=article.Title;
                       Oldarticle.PublishedDate=article.PublishedDate;
                      Oldarticle.Content = article.Content;
                _context.Update(Oldarticle);

                _context.SaveChanges();
                return Ok("Update Successfully");
                  }

            return BadRequest();

        }

        [HttpPost]
        public async Task<ActionResult>PostArticle(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return Ok( new { id = article.Id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return Ok("Delete Successfully");
        }

    
    }
}


