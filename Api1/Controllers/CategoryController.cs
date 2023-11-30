﻿using Api1.Extensions;
using Api1.ViewModels;
using Api1.ViewModels.Categories;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using System.Drawing.Text;

namespace Api1.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync( [FromServices] IMemoryCache cache,
            [FromServices] BlogDataContext context)
        {
            try
            {
                var categories = cache.GetOrCreate("CategoriesCacche", entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                    return GetCategories(context);
                });
                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch 
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("05X04 - Falha interna no servidor"));
            }

        }

        private List<Category> GetCategories (BlogDataContext context)
        {
            return context.Categories.ToList();
        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id,[FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context
                .Categories.
                FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound(new ResultViewModel<Category>("Conteúdo nao encontrado"));

                return Ok(new ResultViewModel<Category>(category));
            }
            catch 
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("05X05 - Falha interna no servidor"));
            }

        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync([FromBody] EditorCategoryViewModel model,[FromServices] BlogDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));

            try
            {
                var category = new Category
                {
                    Id = 0,                   
                    Name = model.Name,
                    Slug = model.Slug.ToLower(),
                };
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
            }
            catch(DbUpdateException e) 
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE9 - Não foi possível incluir a categoria"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>("05X10 - Falha interna no servidor"));
            }


        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id,[FromBody] EditorCategoryViewModel model, [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context
                .Categories.
                FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado."));

                category.Name = model.Name;
                category.Slug = model.Slug.ToLower();

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE8 - Não foi possível alterar a categoria."));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<Category>("05X11 - Falha interna no servidor."));
            }

        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id,[FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context
                .Categories.
                FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado."));


                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE7 - Não foi possível deletar a categoria"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<Category>("05X12 - Falha interna no servidor"));
            }
        }
    }
}