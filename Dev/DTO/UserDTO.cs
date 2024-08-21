using Models;

namespace DTO;

public class UserDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public UserDTO(User entity)
    {
        this.Id = entity.Id;
        this.Name = entity.Name;
        this.Email = entity.Email;
        this.Password = entity.Password;
    }

}

