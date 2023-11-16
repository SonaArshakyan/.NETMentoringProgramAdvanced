using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Services;
using Application.Models;


namespace Catalog.Controllers;

[Route("api/v1/categories")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ICategoryService  _categoryService;
    public CategoryController( ICategoryService categoryService, ILogger<CategoryController> logger)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    /// <summary>
    /// Get all Categories // action => List of categories
    /// </summary>
    /// <response code="200">The Category list successfully got</response>
    /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
    /// <response code="500">A server fault occurred</response>

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCategories()
    {
        return Ok(await _categoryService.GetAllCategoriesAsync());
    }

    /// <summary>
    /// Create a new Category //action => Add category
    /// </summary>
    /// <response code="201">The Category was created successfully. </response>
    /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
    /// <response code="500">A server fault occurred</response>
    /// 
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO category)
    {
        var result = await  _categoryService.CreateCategoryAsync(category);

        return CreatedAtAction(
            actionName: nameof(CreateCategory),
            value: result);
    }

    /// <summary>
    /// Delete Category // action => Delete category (with the related items)
    /// </summary>
    /// <response code="204">The Category was deleted successfully.</response>
    /// <response code="404">A Category having specified Category id was not found</response>
    /// <response code="500">A server fault occurred</response>

    [HttpDelete("{categoryId:int}")] 
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCategory( int categoryId)
    {
        await _categoryService.DeleteCategoryAsync(categoryId);
        return NoContent();
    }

    /// <summary>
    /// Update an existing Category // action => Update category
    /// </summary>
    /// <response code="204">The Category was updated successfully</response>
    /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
    /// <response code="404">The Category was not found for specified Category id</response>
    /// <response code="500">A server fault occurred</response>

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCategory(int id,[FromBody] CategoryDTO  category)
    {
        await _categoryService.UpdateCategoryAsync(id, category);
        return NoContent();
    }
}
