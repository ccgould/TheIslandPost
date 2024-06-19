using System.IO;
using System.Text.RegularExpressions;

namespace TheIslandPostManagerWPF.Helpers;
internal static class HelperMethods
{
    internal static bool IsPhoneNumber(string number)
    {
        return Regex.Match(number, @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$").Success;
    }

    internal static string ReplaceInvalidChars(string filename)
    {
        return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
    }
}
