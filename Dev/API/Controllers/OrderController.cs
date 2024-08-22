using DTO;
using Microsoft.AspNetCore.Mvc;
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
    public ActionResult<List<OrderDTO>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<OrderDTO> GetById(int id)
    {
        OrderDTO order = _service.GetById(id);
        return order != null ? Ok(order) : NotFound($"Order id={id} not found!");
    }

    [HttpGet("customer/{id}")]
    public ActionResult<List<OrderDTO>> GetByCustomerId(int id)
    {
        List<OrderDTO> orders = _service.GetByCustomerId(id);
        return Ok(orders);
    }

    [HttpPost]
    public ActionResult<OrderDTO> Insert(OrderDTO order)
    {
        if(order.Customer == null || order.Customer.Id == 0){
            return BadRequest("Customer Id is missing.");
        }

        OrderDTO orderCreated = _service.Save(order);

        return CreatedAtAction(nameof(GetById), new { id = orderCreated.Id }, orderCreated);
    }

    [HttpPut("{id}")]
    public ActionResult<OrderDTO> Update(int id, OrderDTO order)
    {
        if(id != order.Id){
            return BadRequest("Order Id mismatch.");
        }

        OrderDTO orderFound = _service.GetById(id);

        if (orderFound == null){
            return NotFound($"Order with Id = {id} not found!");
        }
        
        return _service.Update(id, order);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteById(int id)
    {
        OrderDTO orderFound = _service.GetById(id);

        if (orderFound == null){
            return NotFound($"Order with Id = {id} not found!");
        }
        _service.Delete(orderFound);
        return Ok($"Order with Id = {id} deleted!");
    }
}