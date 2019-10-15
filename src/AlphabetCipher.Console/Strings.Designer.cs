namespace AlphabetCipher.Console
{
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [DebuggerNonUserCode]
    [CompilerGenerated]
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    internal sealed class Strings
    {
        private static ResourceManager resourceMan;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager =>
            resourceMan ?? (resourceMan = new ResourceManager(typeof(Strings).FullName, typeof(Strings).Assembly));

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture { get; set; }

        internal static string Arg_InsufficientParameters => ResourceManager.GetString(nameof(Arg_InsufficientParameters), Culture);

        internal static string Arg_InsufficientMethodParameters => ResourceManager.GetString(nameof(Arg_InsufficientMethodParameters), Culture);

        internal static string Arg_UnknownMethod => ResourceManager.GetString(nameof(Arg_UnknownMethod), Culture);

        internal static string Usage_Header => ResourceManager.GetString(nameof(Usage_Header), Culture);

        internal static string Usage_Method => ResourceManager.GetString(nameof(Usage_Method), Culture);
    }
}
