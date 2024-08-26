using DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class SellerController : ControllerBase
{
    private SellerServices _service;

    private UserServices _serviceUser;

    public SellerController(SellerServices service,
                            UserServices serviceUser)
    {
        _service = service;
        _serviceUser = serviceUser;
    }

    [HttpGet]
    public ActionResult<List<SellerDTO>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<SellerDTO> GetById(int id)
    {
        SellerDTO seller = _service.GetById(id);
        return seller != null ? Ok(seller) : NotFound($"Seller id={id} not found!");
    }

    [HttpPost]
    public ActionResult<SellerDTO> Insert(SellerDTO seller)
    {
        User userFound = _serviceUser.GetByEmail(seller.Email);

        if(userFound != null){
            return BadRequest("Email is already taken!");
        }

        seller.Email = seller.Email.ToLower();

        SellerDTO sellerCreated = _service.Save(seller);

        return CreatedAtAction(nameof(GetById), new { id = sellerCreated.Id }, sellerCreated);
    }

    [HttpPut("{id}")]
    public ActionResult<SellerDTO> Update(int id, SellerDTO seller)
    {

        User userFound = _serviceUser.GetByEmail(seller.Email);

        if(userFound != null && userFound.Id != id){
            return BadRequest("Email is already taken!");
        } 

        if(id != seller.Id){
            return BadRequest("Seller Id mismatch.");
        }

        SellerDTO sellerFound = _service.GetById(id);

        if (sellerFound == null){
            return NotFound($"Seller with Id = {id} not found!");
        }
        
        return _service.Update(id, seller);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteById(int id)
    {
        SellerDTO sellerFound = _service.GetById(id);

        if (sellerFound == null){
            return NotFound($"Seller with Id = {id} not found!");
        }
        _service.Delete(sellerFound);
        return Ok($"Seller with Id = {id} deleted!");
    }
}