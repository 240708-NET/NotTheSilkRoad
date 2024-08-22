using DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private UserServices _service;

    public AuthController(UserServices service)
    {
        _service = service;
    }


    [HttpPost]
    public IActionResult Login(UserLoginDTO UserLogin)
    {

        User User = _service.GetByEmail(UserLogin.Email);

        if (User == null)
        {
            return NotFound("User not found!");
        }

        if (BCrypt.Net.BCrypt.Verify(UserLogin.Password, User.Password))
        {
            if (User.GetType() == typeof(Customer))
            {
                return Ok(new CustomerDTO((Customer)User, true));
            }
            else
            {
                return Ok(new SellerDTO((Seller)User, true));
            }
        }
        else
        {
            return StatusCode(401);
        }

    }


}