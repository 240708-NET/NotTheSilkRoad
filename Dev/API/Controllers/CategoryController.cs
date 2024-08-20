using DTO;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private CategoryServices _service;

    public CategoryController(CategoryServices service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<CategoryDTO>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<CategoryDTO> GetById(int id)
    {
        CategoryDTO category = _service.GetById(id);
        return category != null ? Ok(category) : NotFound($"Category id={id} not found!");
    }

    [HttpPost]
    public ActionResult<CategoryDTO> Insert(CategoryDTO category)
    {
        CategoryDTO categoryCreated = _service.Save(category);

        return CreatedAtAction(nameof(GetById), new { id = categoryCreated.Id }, categoryCreated);
    }

    [HttpPut("{id}")]
    public ActionResult<CategoryDTO> Update(int id, CategoryDTO category)
    {
        if(id != category.Id){
            return BadRequest("Category Id mismatch.");
        }

        CategoryDTO categoryFound = _service.GetById(id);

        if (categoryFound == null){
            return NotFound($"Category with Id = {id} not found!");
        }
        
        return _service.Update(id, category);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteById(int id)
    {
        CategoryDTO categoryFound = _service.GetById(id);

        if (categoryFound == null){
            return NotFound($"Category with Id = {id} not found!");
        }
        _service.Delete(categoryFound);
        return Ok($"Category with Id = {id} deleted!");
    }
}