using DTO;
using Microsoft.AspNetCore.Mvc;
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
    public ActionResult<List<ProductDTO>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<ProductDTO> GetById(int id)
    {
        ProductDTO product = _service.GetById(id);
        return product != null ? Ok(product) : NotFound($"Product id={id} not found!");
    }

        [HttpGet("seller/{id}")]
    public ActionResult<List<ProductDTO>> GetBySellerId(int id)
    {
        List<ProductDTO> products = _service.GetBySellerId(id);
        return Ok(products);
    }

    [HttpPost]
    public ActionResult<ProductDTO> Insert(ProductDTO product)
    {
        ProductDTO productCreated = _service.Save(product);

        return CreatedAtAction(nameof(GetById), new { id = productCreated.Id }, productCreated);
    }

    [HttpPut("{id}")]
    public ActionResult<ProductDTO> Update(int id, ProductDTO product)
    {
        if(id != product.Id){
            return BadRequest("Order Id mismatch.");
        }

        ProductDTO productFound = _service.GetById(id);

        if (productFound == null){
            return NotFound($"Product with Id = {id} not found!");
        }
        
        return _service.Update(id, product);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteById(int id)
    {
        ProductDTO productFound = _service.GetById(id);

        if (productFound == null){
            return NotFound($"Product with Id = {id} not found!");
        }
        _service.Delete(productFound);
        return Ok($"Product with Id = {id} deleted!");
    }
}