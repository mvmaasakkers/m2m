using System;
using System.Collections.Generic;
using System.Linq;
using Hobbies.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hobbies.Controllers
{
    [Route("categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private DataContext _dataContext = null;

        public CategoriesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("fill")]
        public IActionResult Fill()
        {
            var p1 = new Post {Title = "Thing"};

            _dataContext.Posts.Add(p1);
            _dataContext.Posts.Add(new Post() {Title = "DingFlofBips"});

            var t1 = new Tag {TagId = "euro"};

            _dataContext.Tags.Add(t1);
            _dataContext.Tags.Add(new Tag() {TagId = "other"});

            p1.PostTags = new List<PostTag>();
            p1.PostTags.Add(new PostTag {Post = p1, Tag = t1});

            _dataContext.SaveChanges();
            return Ok();
        }

        //
        [HttpGet("select")]
        public IActionResult GetCategoriesSelect()
        {
            var objectList = _dataContext.Posts.Select(o => new
            {
                o.PostId,
                o.Title,
                o.Content,
                Tags = o.PostTags.Select(ot => ot.Tag).ToList()
            }).ToList();

            return Ok(objectList);
            // return _dataContext.Posts.Include(c => c.PostTags).ThenInclude(ci => ci.Tag).ToList();
        }

        [HttpGet]
        public List<Post> GetCategoriesD()
        {
            return _dataContext.Posts.Include(x => x.PostTags).ThenInclude(x => x.Tag).ToList();
        }
    }
}