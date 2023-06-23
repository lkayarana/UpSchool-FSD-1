using RandomPasswordGenerator;

var passwordGenerator = new PasswordGenerator();

passwordGenerator.Introduction();
passwordGenerator.ReadInputs();

if (passwordGenerator.IncludeNumbers || passwordGenerator.IncludeLowercaseCharacters ||
    passwordGenerator.IncludeUppercaseCharacters || passwordGenerator.IncludeSpecialCharacters)
{
    passwordGenerator.Create();
    passwordGenerator.WriteLatestGeneratedPassword();
}

Console.ReadLine();

return 0;