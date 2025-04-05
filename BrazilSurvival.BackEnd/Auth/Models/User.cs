using System.ComponentModel.DataAnnotations;

namespace BrazilSurvival.BackEnd.Auth.Models;

public record User(
    long Id,
    [StringLength(255)] string Name,
    [EmailAddress][StringLength(255)] string Email,
    [StringLength(255)] string? ProfilePicture,
    [StringLength(32)] string Password,
    string Role,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
