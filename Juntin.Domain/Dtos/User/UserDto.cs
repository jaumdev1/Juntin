namespace Domain.Dtos.User;

public record UserDto
{
    public UserDto(string username, string email, string password, string confirmPassword)
    {
        Email = email;
        Username = username;
        Password = password;
        ConfirmPassword = confirmPassword;
    }

    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}