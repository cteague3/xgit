using Newtonsoft.Json;
using XGit.Core.Domain;
using XGit.Core.settings;

namespace XGit.Core.Util
{
  public static class Extensions
  {
    public static string JsonSerialize<T>(this T source, Formatting format = Formatting.None)
    {
      return JsonConvert.SerializeObject(source, format);
    }

    public static List<T> JsonDeserializeList<T>(this string source)
    {
      return JsonConvert.DeserializeObject<List<T>>(source);
    }

    public static List<T> JsonDeserializeList<T>(this string source, T typeTo)
    {
      return JsonConvert.DeserializeObject<List<T>>(source);
    }

    public static T JsonDeserialize<T>(this string source)
    {
      return JsonConvert.DeserializeObject<T>(source);
    }

    public static T JsonCopy<T>(this object source)
    {
      return source.JsonSerialize().JsonDeserialize<T>();
    }

    public static List<FileInfo> LoadReposFiles(this DirectoryInfo reposRootDirectoryInfo, out bool succ)
    {
      var noFailure = true;
      var reposFiles = new List<FileInfo>();
      Ops.Repos.GroupBy(rg => rg.LocalNamespace).ToList().ForEach(repoGroup =>
      {
        var reposNamespace = repoGroup.Key;
        var reposFileInfo =
          new FileInfo(reposRootDirectoryInfo.FullName + Path.DirectorySeparatorChar + reposNamespace + Path.DirectorySeparatorChar + reposNamespace + ".json");
        if (reposFileInfo.Exists)
        {
          Console.WriteLine($@"Repos File Found={reposFileInfo.FullName}");
          reposFiles.Add(reposFileInfo);
        }
        else
        {
          Console.WriteLine($@"Repos File Not Found!={reposFileInfo.FullName}");
        }
        noFailure = noFailure && reposFileInfo.Exists;
      });
      succ = noFailure;
      return reposFiles;
    }

    public static bool CreateReposFiles(this DirectoryInfo reposRootDirectoryInfo)
    {
      var noFailure = true;
      Ops.Repos.GroupBy(rg => rg.LocalNamespace).ToList().ForEach(repoGroup =>
      {
        var reposNamespace = repoGroup.Key;
        if (!Directory.Exists(reposRootDirectoryInfo.FullName + Path.DirectorySeparatorChar + reposNamespace))
        {
          Directory.CreateDirectory(reposRootDirectoryInfo.FullName + Path.DirectorySeparatorChar + reposNamespace);
        }
        var reposFileInfo =
          new FileInfo(reposRootDirectoryInfo.FullName + Path.DirectorySeparatorChar + reposNamespace + Path.DirectorySeparatorChar + reposNamespace + ".json");

        if (reposFileInfo.Exists) reposFileInfo.Delete();
        reposFileInfo.Refresh();

        noFailure = noFailure && reposFileInfo.CreateReposFile(reposNamespace);
      });
      return noFailure;
    }

    public static bool CreateReposFile(this FileInfo reposFileInfo, string reposNamespace)
    {
      var repos = Ops.Repos
        .Where(r => r.LocalNamespace.Equals(reposNamespace, StringComparison.OrdinalIgnoreCase))
        .ToList();
      var jRepos = repos.JsonSerialize<List<Repo>>(Formatting.Indented);
      File.WriteAllText(reposFileInfo.FullName, jRepos);
      reposFileInfo.Refresh();
      return reposFileInfo.Exists;
    }

  }
}
