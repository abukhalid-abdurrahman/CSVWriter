using System.Linq;

namespace tests.Helpers;

public static class FileComparator
{
    public static bool FileEquals(string path1, string path2)
    {
        var file1 = File.ReadAllBytes(path1);
        var file2 = File.ReadAllBytes(path2);

        if (file1.Length != file2.Length) return false;

        return !file1.Where((t, i) => t != file2[i]).Any();
    }
}
