using System.IO;

namespace tests.Helpers;

public static class FileComparator
{
    public static bool FileEquals(string path1, string path2)
    {
        var file1 = File.ReadAllBytes(path1);
        var file2 = File.ReadAllBytes(path2);
        if (file1.Length != file2.Length) return false;
        
        for (var i = 0; i < file1.Length; i++)
        {
            if (file1[i] != file2[i])
            {
                return false;
            }
        }
        return true;
    }
}