using System;
using System.Collections.Generic;
using System.IO;
//using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logan.Mall.BaseLib.Utilities
{
    public class PathUtil
    {
        public static string GetFilePath(string relativePath)
        {
            string filePath = relativePath;
            if (!string.IsNullOrEmpty(filePath))
            {
                if (!relativePath.ToUpper().StartsWith(AppDomain.CurrentDomain.BaseDirectory))
                {
                    filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath.Replace('/', '\\').TrimStart('\\'));
                }
            }
            return filePath;
        }

    }
}
