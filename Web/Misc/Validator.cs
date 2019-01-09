using System.IO;
using System.Linq;

namespace Web.Misc
{
    public static class Validator
    {
        public static bool IsImage(string fileName) => Constants.ImageExtensions.Contains(Path.GetExtension(fileName));
    }
}
