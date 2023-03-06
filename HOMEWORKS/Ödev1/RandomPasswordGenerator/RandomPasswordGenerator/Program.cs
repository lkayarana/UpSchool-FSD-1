using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RandomPasswordGenerator;

namespace RandomPasswordGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var passwordGenerator = new PasswordGenerator();

            passwordGenerator.Introduction();

            passwordGenerator.ReadInputs();

            passwordGenerator.Create();

            passwordGenerator.WriteLatestGeneratedPassword();
            
            Console.ReadLine();

            //return 0;

        }
    }
}
