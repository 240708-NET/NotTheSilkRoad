using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private OrderServices _service;

    public OrderController(OrderServices service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<Order>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Order> GetById(int id)
    {
        Order order = _service.GetById(id);
        return order != null ? Ok(order) : NotFound($"Order id={id} not found!");
    }

    [HttpPost]
    public ActionResult<Order> Insert(Order order)
    {
        if(order.Customer == null || order.Customer.Id == 0){
            return BadRequest("Customer Id is missing.");
        }

        Order orderCreated = _service.Save(order);

        return CreatedAtAction(nameof(GetById), new { id = orderCreated.Id }, orderCreated);
    }

    [HttpPut("{id}")]
    public ActionResult<Order> Update(int id, Order order)
    {
        if(id != order.Id){
            return BadRequest("Order Id mismatch.");
        }

        Order orderFound = _service.GetById(id);

        if (orderFound == null){
            return NotFound($"Order with Id = {id} not found!");
        }
        
        return _service.Update(id, order);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteById(int id)
    {
        Order orderFound = _service.GetById(id);

        if (orderFound == null){
            return NotFound($"Order with Id = {id} not found!");
        }
        _service.Delete(orderFound);
        return Ok($"Order with Id = {id} deleted!");
    }
}