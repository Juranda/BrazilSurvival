using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public record RegisterRequest(
    [Required]
    [MinLength(4)]
    string name,

    [Required]
    [EmailAddress(ErrorMessage = "Email inválido")]
    string email,

    [Required]
    [PasswordPropertyText]
    string password
);