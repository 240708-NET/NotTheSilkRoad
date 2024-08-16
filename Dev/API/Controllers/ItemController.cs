namespace API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private ItemServices _ModelService;

    public ItemController(ItemServices ModelService)
    {
        _ModelService = ModelService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var models = _ModelService.GetAll();
        return Ok(models);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var models = _ModelService.GetById(id);
        return Ok(models);
    }

    [HttpPost]
    public IActionResult Insert(Item model)
    {
        if (_ModelService.Save(model) != null)
            return Ok(new { message = "Created Model" });
        else 
            return Ok(new { message = "Client-side Error" });
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Item model)
    {
        if (_ModelService.Update(id, model) != null)
            return Ok(new { message = "Updated" });
        else 
            return Ok(new { message = "Client-side Error" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _ModelService.DeleteById(id);
        return Ok(new { message = "Deleted" });
    }
}