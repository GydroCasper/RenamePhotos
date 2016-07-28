using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RenamePhotos.Code
{
    internal class File
    {
        internal static List<string> GetFiles()
        {
            return Directory.GetFiles(Directory.GetCurrentDirectory(), "*.jpg").ToList();
        }
    }
}
