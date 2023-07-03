using UpSchool.Domain.Dtos;

public class PasswordMemento
{
    public string Password { get; }
    public GeneratePasswordDto GeneratePasswordDto { get; }

    public PasswordMemento(string password, GeneratePasswordDto generatePasswordDto)
    {
        Password = password;
        GeneratePasswordDto = generatePasswordDto;
    }
}