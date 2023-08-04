namespace XGit.Core.Domain
{
  public class Repo
  {
    public Repo(string localNamespace, string localName, string originFullName, string originUrl)
    {
      LocalNamespace = localNamespace;
      LocalName = localName;
      OriginFullName = originFullName;
      OriginUrl = originUrl;

    }

    public string LocalNamespace { get; set; }
    public string LocalName { get; set; }
    public string OriginFullName { get; set; }
    public string OriginUrl { get; set; }
  }
}
