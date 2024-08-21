using DTO;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private UserServices _service;

    public UserController(UserServices service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<UserDTO>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<UserDTO> GetById(int id)
    {
        UserDTO user = _service.GetById(id);
        return user != null ? Ok(user) : NotFound($"User id={id} not found!");
    }

    [HttpPost]
    public ActionResult<UserDTO> Insert(UserDTO user)
    {
        UserDTO userCreated = _service.Save(user);

        return CreatedAtAction(nameof(GetById), new { id = userCreated.Id }, userCreated);
    }

    [HttpPut("{id}")]
    public ActionResult<UserDTO> Update(int id, UserDTO user)
    {
        if(id != user.Id){
            return BadRequest("User Id mismatch.");
        }

        UserDTO userFound = _service.GetById(id);

        if (userFound == null){
            return NotFound($"User with Id = {id} not found!");
        }
        
        return _service.Update(id, user);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteById(int id)
    {
        UserDTO userFound = _service.GetById(id);

        if (userFound == null){
            return NotFound($"User with Id = {id} not found!");
        }
        _service.Delete(userFound);
        return Ok($"User with Id = {id} deleted!");
    }
}