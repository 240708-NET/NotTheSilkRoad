using DTO;
using Microsoft.AspNetCore.Mvc;
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
    public ActionResult<List<ItemDTO>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<ItemDTO> GetById(int id)
    {
        ItemDTO item = _service.GetById(id);
        return item != null ? Ok(item) : NotFound($"Item id={id} not found!");
    }

    [HttpPost]
    public ActionResult<ItemDTO> Insert(ItemDTO item)
    {
        ItemDTO itemCreated = _service.Save(item);

        return CreatedAtAction(nameof(GetById), new { id = itemCreated.Id }, itemCreated);
    }

    [HttpPut("{id}")]
    public ActionResult<ItemDTO> Update(int id, ItemDTO item)
    {
        if(id != item.Id){
            return BadRequest("Item Id mismatch.");
        }

        ItemDTO itemFound = _service.GetById(id);

        if (itemFound == null){
            return NotFound($"Item with Id = {id} not found!");
        }
        
        return _service.Update(id, item);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteById(int id)
    {
        ItemDTO itemFound = _service.GetById(id);

        if (itemFound == null){
            return NotFound($"Item with Id = {id} not found!");
        }
        _service.Delete(itemFound);
        return Ok($"Item with Id = {id} deleted!");
    }
}