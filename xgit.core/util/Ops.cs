using XGit.Core.Domain;

namespace XGit.Core.Util
{
  public static class Ops
  {
    public static List<Repo> Repos = new List<Repo>
    {
      new Repo(
        localNamespace: "gitstuff",
        localName: "github.skills.intro",
        originFullName: "skills.introduction-to-github",
        originUrl: "https://github.com/skills/introduction-to-github.git"),
      new Repo(
        localNamespace: "gitstuff",
        localName: "github.gitignore",
        originFullName:"github.gitignore",
        originUrl:
        "https://github.com/github/gitignore.git"),
      new Repo(localNamespace:"gitstuff",
        localName:"gitextensions",
        originFullName:"gitextensions.gitextensions",
        originUrl:"https://github.com/gitextensions/gitextensions.git"),
      new Repo(
        localName:"octokit.net",
        localNamespace:"gitstuff",
        originFullName:"octokit.octokit.net",
        originUrl:"https://github.com/octokit/octokit.net.git"),
      new Repo(
        localNamespace: "gitstuff",
        localName: "xgit",
        originFullName:"cteague3.xgit",
        originUrl:
        "https://github.com/cteague3/xgit.git"),

    };

  }
}
