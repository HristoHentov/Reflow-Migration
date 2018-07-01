using System.Runtime.InteropServices;

namespace Reflow.Core.Utility
{
    internal static class Utils
    {
        /// <summary>
        /// Returns a 2 element string[] containg file name at index 0 and file type at index 1.
        /// </summary>
        /// <param name="path"> The path of the file. </param>
        /// <returns>string[] where the 0 index contains the filename and index 1 - the file type</returns>
        internal static string[] GetFullFilename(string path)
        {
            string[] attribs = new string[2];

            attribs[0] = path.Substring(path.LastIndexOf(GetOsSeperator()) + 1);
            var extensionStart = attribs[0].LastIndexOf('.');

            if (extensionStart == -1) // Cases where the file does not have an extension
                return attribs;

            attribs[1] = attribs[0].Substring(attribs[0].LastIndexOf('.'));

            attribs[0] = attribs[0].Replace(attribs[1], string.Empty);
            attribs[1] = attribs[1].Substring(1);
            return attribs;
        }

        internal static char GetOsSeperator()
        {
            var IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if (IsWindows)
                return '\\';

            return '/';
        }
    }
}
