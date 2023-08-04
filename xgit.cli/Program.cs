//using System.Reflection;
using XGit.Core.Domain;
using XGit.Core.settings;
using XGit.Core.Util;
using Formatting = Newtonsoft.Json.Formatting;

var noFailure = true;
var cmdLineArgs = Environment.GetCommandLineArgs();
Console.WriteLine($@"CommandLineArgs={cmdLineArgs.JsonSerialize(Formatting.Indented)}");
Console.WriteLine($@"Args={args.JsonSerialize(Formatting.Indented)}");

//var resDirectoryInfo = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "Resources");
//if (!resDirectoryInfo.Exists)
//{
//  Console.WriteLine($@"Error Encountered! Resources folder could not be located! Path={resDirectoryInfo.FullName}");
//  return 1;
//}
//Console.WriteLine($@"Resources folder located! Path={resDirectoryInfo.FullName}");


var reposRootDirectoryInfo = new DirectoryInfo(Config.Default.ReposRoot);
if (!reposRootDirectoryInfo.Exists) Directory.CreateDirectory(reposRootDirectoryInfo.FullName);
var reposFileInfo = new FileInfo(reposRootDirectoryInfo.FullName + Path.DirectorySeparatorChar + Config.Default.ReposFile);

if (args.Any() && string.Equals(args[0], "init", StringComparison.OrdinalIgnoreCase))
{
  if (reposFileInfo.Exists) { reposFileInfo.Delete(); reposFileInfo.Refresh(); }
  File.WriteAllText(reposFileInfo.FullName, Ops.Repos.JsonSerialize(Formatting.Indented));
  reposFileInfo.Refresh();
  if (reposFileInfo.Exists)
  {
    Console.WriteLine($@"ReposFile={reposFileInfo.FullName} successfully initialized!");
    return 0;
  }
  Console.WriteLine($@"ReposFile={reposFileInfo.FullName} failed initialization!");
  return 1;
}


if (!reposFileInfo.Exists)
{
  Console.WriteLine($@"Error Encountered! Repos file could not be located! Path={reposFileInfo.FullName}");
  return 1;
}
Console.WriteLine($@"Repos file located! Path={reposFileInfo.FullName}");
Ops.Repos = File.ReadAllText(reposFileInfo.FullName).JsonDeserialize<List<Repo>>();

noFailure = noFailure && reposRootDirectoryInfo.CreateReposFiles();

var reposFiles = reposRootDirectoryInfo.LoadReposFiles(out var succ);
if (!succ)
{
  Console.WriteLine($@"Error Encountered while loading repos files from repos root folder! Path={reposRootDirectoryInfo.FullName}");
  return 1;
}

if (!reposFiles.Any())
{
  Console.WriteLine($@"No repos files were found in repos root folder! Path={reposRootDirectoryInfo.FullName}");
  return 1;
}


return noFailure ? 0 : 1;