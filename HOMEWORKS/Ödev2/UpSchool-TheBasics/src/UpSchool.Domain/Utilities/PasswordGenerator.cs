using System.Text;
using UpSchool.Domain.Dtos;

namespace UpSchool.Domain.Utilities
{
    public class PasswordGenerator
    {
        private const string Numbers = "0123456789";
        private const string LowercaseCharacters = "abcdefghijklmnopqrstuvwxyz";
        private const string UppercaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string SpecialCharacters = "!@#$%^&*()";

        private readonly Random _random;
        private readonly StringBuilder _passwordBuilder;
        private readonly StringBuilder _charSetBuilder;
        private readonly Stack<PasswordMemento> _passwordHistory;
        private readonly Stack<PasswordMemento> _redoStack;

        public PasswordGenerator()
        {
            _random = new Random();
            _passwordBuilder = new StringBuilder();
            _charSetBuilder = new StringBuilder();
            _passwordHistory = new Stack<PasswordMemento>();
            _redoStack = new Stack<PasswordMemento>();
        }

        public string Generate(GeneratePasswordDto generatePasswordDto)
        {
            _charSetBuilder.Clear();
            _passwordBuilder.Clear();

            if (generatePasswordDto.IncludeNumbers) _charSetBuilder.Append(Numbers);
            if (generatePasswordDto.IncludeLowercaseCharacters) _charSetBuilder.Append(LowercaseCharacters);
            if (generatePasswordDto.IncludeUppercaseCharacters) _charSetBuilder.Append(UppercaseCharacters);
            if (generatePasswordDto.IncludeSpecialCharacters) _charSetBuilder.Append(SpecialCharacters);

            if (!generatePasswordDto.IncludeNumbers && !generatePasswordDto.IncludeLowercaseCharacters &&
                !generatePasswordDto.IncludeUppercaseCharacters && !generatePasswordDto.IncludeSpecialCharacters)
            {
                return string.Empty;
            }

            var charSet = _charSetBuilder.ToString();

            for (int i = 0; i < generatePasswordDto.Length; i++)
            {
                var randomIndex = _random.Next(charSet.Length);
                _passwordBuilder.Append(charSet[randomIndex]);
            }

            var password = _passwordBuilder.ToString();
            _passwordHistory.Push(new PasswordMemento(password, generatePasswordDto));
            _redoStack.Clear();

            return password;
        }

        public bool CanUndoPassword()
        {
            return _passwordHistory.Count > 1;
        }

        public bool CanRedoPassword()
        {
            return _redoStack.Count > 0;
        }

        public string UndoPassword()
        {
            if (CanUndoPassword())
            {
                var previousMemento = _passwordHistory.Pop();
                _redoStack.Push(previousMemento);

                var previousPassword = _passwordHistory.Peek().Password;
                _passwordBuilder.Clear();
                _passwordBuilder.Append(previousPassword);
                return previousPassword;
            }

            return string.Empty;
        }

        public string RedoPassword()
        {
            if (CanRedoPassword())
            {
                var redoMemento = _redoStack.Pop();
                _passwordHistory.Push(redoMemento);

                var redoPassword = redoMemento.Password;
                _passwordBuilder.Clear();
                _passwordBuilder.Append(redoPassword);
                return redoPassword;
            }

            return string.Empty;
        }
    }
}
