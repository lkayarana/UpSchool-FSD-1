using System;
using System.Collections.Generic;
using System.Text;

namespace RandomPasswordGenerator
{
    public class PasswordGenerator
    {
        public const string Numbers = "1234567890";
        public const string LowercaseCharacters = "abcdefghijklmnopqrstuvwxyz";
        public const string UppercaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string SpecialCharacters = "*$-+?_&=!%{}/\"";
        private int _length = 0;
        private bool _isUserInputValid = true;


        private string _lastGneratedPassword;
        private List<string> _generatedPasswords;


        private readonly Random _random;
        private readonly StringBuilder _passwordBuilder;
        private readonly StringBuilder _charSetBuilder;


        public bool IncludeNumbers { get; set; }
        public bool IncludeLowercaseCharacters { get; set; }
        public bool IncludeUppercaseCharacters { get; set; }
        public bool IncludeSpecialCharacters { get; set; }
        

        public PasswordGenerator()
        {
            _random = new Random();

            _passwordBuilder = new StringBuilder();

            _charSetBuilder = new StringBuilder();

            _lastGneratedPassword = string.Empty;

            _generatedPasswords = new List<string>();
        }

        public void Introduction()
        {
            Console.WriteLine("*****************************************");
            Console.WriteLine("Welcome to the Random Password Generator!");
            Console.WriteLine("*****************************************");
        }
        
        public void ReadInputs()
        {
            Console.WriteLine(Inputs.Questions.IncludeNumbers);
            IncludeNumbers = Console.ReadLine()?.ToLower() == "y";

            Console.WriteLine(Inputs.Questions.IncludeLowercaseCharacters);
            IncludeLowercaseCharacters = Console.ReadLine()?.ToLower()== "y";

            Console.WriteLine(Inputs.Questions.IncludeUppercaseCharacters);
            IncludeUppercaseCharacters = Console.ReadLine()?.ToLower()== "y";

            Console.WriteLine(Inputs.Questions.IncludeSpecialCharacters);
            IncludeSpecialCharacters = Console.ReadLine()?.ToLower()== "y";

            Console.WriteLine(Inputs.Questions.PasswordLength);
           
            var passwordLength = Console.ReadLine();

            if (!int.TryParse(passwordLength, out var _length)
                ||
                !IncludeNumbers && !IncludeLowercaseCharacters && !IncludeUppercaseCharacters &&
                !IncludeSpecialCharacters)
            {
                _isUserInputValid = false;
                return;
            }

            _isUserInputValid = true;

        }


        private void DisplayErroeMessage(string message = null)
        {
            if (string.IsNullOrEmpty(message))
                Console.WriteLine("Geçersiz işlem!");
            return;
        }


        public void Create()
        {
            if (IncludeNumbers) _charSetBuilder.Append(Numbers);

            if (IncludeLowercaseCharacters) _charSetBuilder.Append(LowercaseCharacters);

            if (IncludeUppercaseCharacters) _charSetBuilder.Append(UppercaseCharacters);

            if (IncludeSpecialCharacters) _charSetBuilder.Append(SpecialCharacters);

            var charSet = _charSetBuilder.ToString();

            for ( int i = 0; i < _length; i++ )
            {
                var randomIndex = _random.Next(charSet.Length);

                _passwordBuilder.Append(charSet[randomIndex]);
            }

            _lastGneratedPassword = _passwordBuilder.ToString();

            _generatedPasswords.Add( _lastGneratedPassword );

            _charSetBuilder.Clear();
            _passwordBuilder.Clear();
        }


        public string GetLatestGeneratedPassword() => _lastGneratedPassword;

        public List<string> GetGeneratedPasswords() => _generatedPasswords;

        public void WriteLatestGeneratedPassword() => WriteFormattedPassword(_lastGneratedPassword);

        public void WriteGeneratedPasswords() => _generatedPasswords.ForEach(WriteFormattedPassword);

        public void WriteFormattedPassword(string password)
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine(password);
            Console.WriteLine("------------------------------------------------");
        }

        
        


    }
}
