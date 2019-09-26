using System;
using System.Linq;
using System.Reflection;

using Consol = System.Console;

namespace AlphabetCipher.Console
{
    class Program
    {
        static int Main(string[] args)
        {
            return new Program(args).Start();
        }

        private readonly string[] args;
        private readonly MethodInfo[] methods;
        private readonly Cipher cipher;

        private Program(string[] args)
        {
            this.args = args;
            this.methods = typeof(Cipher).GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            this.cipher = new Cipher();
        }

        private int Start()
        {
            if (args == null || args.Length < 1)
            {
                return DisplayHelp("Insufficient parameters passed: " + args.Length);
            }

            var method = FindMethod(args[0]);
            if (method == null)
            {
                return DisplayHelp("Unknown method: " + args[0]);
            }

            var parameters = method.GetParameters();
            if (parameters.Length + 1 != args.Length)
            {
                return DisplayHelp("Insufficient parameters passed for the " + method.Name + " method: " + args.Length);
            }

            try
            {
                Consol.WriteLine(method.Invoke(this.cipher, args.Skip(1).ToArray()));
            }
            catch (Exception e)
            {
                Consol.Error.WriteLine(e);
                Consol.Error.WriteLine();
                return DisplayHelp(null);
            }
            
            return 0;
        }

        private MethodInfo FindMethod(string name)
        {
            return methods.FirstOrDefault(method => method.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        int DisplayHelp(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Consol.WriteLine(message);
                Consol.WriteLine();
            }

            Consol.WriteLine("Usage: dotnet run {methodName} {input1} {input2}");
            foreach (var method in methods)
            {
                Consol.WriteLine("  " + method.Name + ": ");
                foreach (var parameter in method.GetParameters())
                {
                    Consol.WriteLine("    " + parameter.Name);
                }
                Consol.WriteLine();
            }

            return 1;
        }
    }
}
