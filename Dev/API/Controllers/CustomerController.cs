using DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private CustomerServices _service;

    private UserServices _serviceUser;

    public CustomerController(CustomerServices service,
                                UserServices serviceUser)
    {
        _service = service;
        _serviceUser = serviceUser;
    }

    [HttpGet]
    public ActionResult<List<CustomerDTO>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<CustomerDTO> GetById(int id)
    {
        CustomerDTO customer = _service.GetById(id);
        return customer != null ? Ok(customer) : NotFound($"Customer id={id} not found!");
    }

    [HttpPost]
    public ActionResult<CustomerDTO> Insert(CustomerDTO customer)
    {
        User userFound = _serviceUser.GetByEmail(customer.Email);

        if(userFound != null){
            return BadRequest("Email is already taken!");
        }
        
        customer.Email = customer.Email.ToLower();

        CustomerDTO customerCreated = _service.Save(customer);

        return CreatedAtAction(nameof(GetById), new { id = customerCreated.Id }, customerCreated);
    }

    [HttpPut("{id}")]
    public ActionResult<CustomerDTO> Update(int id, CustomerDTO customer)
    {

        User userFound = _serviceUser.GetByEmail(customer.Email);

        if(userFound != null && userFound.Id != id){
            return BadRequest("Email is already taken!");
        }

        if(id != customer.Id){
            return BadRequest("Customer Id mismatch.");
        }

        CustomerDTO customerFound = _service.GetById(id);

        if (customerFound == null){
            return NotFound($"Customer with Id = {id} not found!");
        }
        
        return _service.Update(id, customer);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteById(int id)
    {
        CustomerDTO customerFound = _service.GetById(id);

        if (customerFound == null){
            return NotFound($"Customer with Id = {id} not found!");
        }
        _service.Delete(customerFound);
        return Ok($"Customer with Id = {id} deleted!");
    }
}