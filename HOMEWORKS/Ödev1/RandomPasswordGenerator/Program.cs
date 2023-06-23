using RandomPasswordGenerator;

var passwordGenerator = new PasswordGenerator();

passwordGenerator.Introduction();

passwordGenerator.ReadInputs();

passwordGenerator.Create();

passwordGenerator.WriteLatestGeneratedPassword();

Console.ReadLine();

return 0;