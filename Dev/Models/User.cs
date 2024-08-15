using System.ComponentModel.DataAnnotations;

namespace Models;

public class User
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

}
