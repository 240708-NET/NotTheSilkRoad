using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class SellerController : ControllerBase
{
    private SellerServices _service;

    public SellerController(SellerServices service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<Seller>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Seller> GetById(int id)
    {
        Seller seller = _service.GetById(id);
        return seller != null ? Ok(seller) : NotFound($"Seller id={id} not found!");
    }

    [HttpPost]
    public ActionResult<Seller> Insert(Seller seller)
    {
        Seller sellerCreated = _service.Save(seller);

        return CreatedAtAction(nameof(GetById), new { id = sellerCreated.Id }, sellerCreated);
    }

    [HttpPut("{id}")]
    public ActionResult<Seller> Update(int id, Seller seller)
    {
        if(id != seller.Id){
            return BadRequest("Seller Id mismatch.");
        }

        Seller sellerFound = _service.GetById(id);

        if (sellerFound == null){
            return NotFound($"Seller with Id = {id} not found!");
        }
        
        return _service.Update(id, seller);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteById(int id)
    {
        Seller sellerFound = _service.GetById(id);

        if (sellerFound == null){
            return NotFound($"Seller with Id = {id} not found!");
        }
        _service.Delete(sellerFound);
        return Ok($"Seller with Id = {id} deleted!");
    }
}