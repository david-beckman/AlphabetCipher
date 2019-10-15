//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AlphabetCipher.Console
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;

    using Console = System.Console;

    internal class Program
    {
        private readonly MethodInfo[] methods;

        private readonly Cipher cipher;

        private Program()
        {
            this.methods = typeof(Cipher).GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            this.cipher = new Cipher();
        }

        private static IFormatProvider CurrentFormatProvider => Thread.CurrentThread.CurrentCulture;

        private static int Main(string[] args)
        {
            return new Program().Start(args);
        }

        private int Start(string[] args)
        {
            if (args == null || args.Length < 1)
            {
                return this.DisplayHelp(string.Format(Program.CurrentFormatProvider, Strings.Arg_InsufficientParameters, args.Length));
            }

            var method = this.FindMethod(args[0]);
            if (method == null)
            {
                return this.DisplayHelp(string.Format(Program.CurrentFormatProvider, Strings.Arg_UnknownMethod, args[0]));
            }

            var parameters = method.GetParameters();
            if (parameters.Length + 1 != args.Length)
            {
                return this.DisplayHelp(string.Format(
                    Program.CurrentFormatProvider,
                    Strings.Arg_InsufficientMethodParameters,
                    method.Name,
                    args.Length));
            }

            try
            {
                Console.WriteLine(method.Invoke(this.cipher, args.Skip(1).ToArray()));
            }
            catch (TargetInvocationException e)
            {
                Console.Error.WriteLine(e.InnerException);
                Console.Error.WriteLine();
                return this.DisplayHelp(null);
            }

            return 0;
        }

        private MethodInfo FindMethod(string name)
        {
            return this.methods.FirstOrDefault(method => method.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        private int DisplayHelp(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine(message);
                Console.WriteLine();
            }

            Console.WriteLine(Strings.Usage_Header);
            foreach (var method in this.methods)
            {
                Console.WriteLine(string.Format(
                    Program.CurrentFormatProvider,
                    Strings.Usage_Method,
                    method.Name,
                    string.Join(" ", method.GetParameters().Select(parameter => parameter.Name))));
            }

            return 1;
        }
    }
}
