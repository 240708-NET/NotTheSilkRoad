using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private ProductServices _service;

    public ProductController(ProductServices service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<Product>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        Product product = _service.GetById(id);
        return product != null ? Ok(product) : NotFound($"Product id={id} not found!");
    }

    [HttpPost]
    public ActionResult<Product> Insert(Product product)
    {
        Product productCreated = _service.Save(product);

        return CreatedAtAction(nameof(GetById), new { id = productCreated.Id }, productCreated);
    }

    [HttpPut("{id}")]
    public ActionResult<Product> Update(int id, Product product)
    {
        if(id != product.Id){
            return BadRequest("Order Id mismatch.");
        }

        Product productFound = _service.GetById(id);

        if (productFound == null){
            return NotFound($"Product with Id = {id} not found!");
        }
        
        return _service.Update(id, product);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteById(int id)
    {
        Product productFound = _service.GetById(id);

        if (productFound == null){
            return NotFound($"Product with Id = {id} not found!");
        }
        _service.Delete(productFound);
        return Ok($"Product with Id = {id} deleted!");
    }
}