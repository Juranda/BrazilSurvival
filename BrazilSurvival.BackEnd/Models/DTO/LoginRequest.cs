using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public record LoginRequest(
    [Required]
    [EmailAddress(ErrorMessage = "Email inválido")]
    string email,

    [Required]
    [PasswordPropertyText]
    string password
);