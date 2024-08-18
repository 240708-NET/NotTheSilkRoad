using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private CustomerServices _service;

    public CustomerController(CustomerServices service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<Customer>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Customer> GetById(int id)
    {
        Customer customer = _service.GetById(id);
        return customer != null ? Ok(customer) : NotFound($"Customer id={id} not found!");
    }

    [HttpPost]
    public ActionResult<Customer> Insert(Customer customer)
    {
        Customer customerCreated = _service.Save(customer);

        return CreatedAtAction(nameof(GetById), new { id = customerCreated.Id }, customerCreated);
    }

    [HttpPut("{id}")]
    public ActionResult<Customer> Update(int id, Customer customer)
    {
        if(id != customer.Id){
            return BadRequest("Customer Id mismatch.");
        }

        Customer customerFound = _service.GetById(id);

        if (customerFound == null){
            return NotFound($"Customer with Id = {id} not found!");
        }
        
        return _service.Update(id, customer);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteById(int id)
    {
        Customer customerFound = _service.GetById(id);

        if (customerFound == null){
            return NotFound($"Customer with Id = {id} not found!");
        }
        _service.Delete(customerFound);
        return Ok($"Customer with Id = {id} deleted!");
    }
}