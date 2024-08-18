using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private ItemServices _service;

    public ItemController(ItemServices service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<Item>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Item> GetById(int id)
    {
        Item item = _service.GetById(id);
        return item != null ? Ok(item) : NotFound($"Item id={id} not found!");
    }

    [HttpPost]
    public ActionResult<Item> Insert(Item item)
    {
        Item itemCreated = _service.Save(item);

        return CreatedAtAction(nameof(GetById), new { id = itemCreated.Id }, itemCreated);
    }

    [HttpPut("{id}")]
    public ActionResult<Item> Update(int id, Item item)
    {
        if(id != item.Id){
            return BadRequest("Item Id mismatch.");
        }

        Item itemFound = _service.GetById(id);

        if (itemFound == null){
            return NotFound($"Item with Id = {id} not found!");
        }
        
        return _service.Update(id, item);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteById(int id)
    {
        Item itemFound = _service.GetById(id);

        if (itemFound == null){
            return NotFound($"Item with Id = {id} not found!");
        }
        _service.Delete(itemFound);
        return Ok($"Item with Id = {id} deleted!");
    }
}