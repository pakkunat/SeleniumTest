using Nett;

namespace PLib {
  public class Setting {
    public bool IsHeadless { get; private set; }
    
    public string? UserName { get; private set; }

    public string? Password { get; private set; }

    private Log log = Log.Instance;

    public bool Read(string filePath) {
      try {
        var toml = Toml.ReadFile(filePath);
        var info = toml.Get<TomlTable>("Info");
        IsHeadless = info.Get<bool>("Headless");
        UserName = info.Get<string>("UserName");
        Password = info.Get<string>("Password");
      } catch (Exception ex) {
        log.Write($"Exception: toml read: {ex.Message}");
        return false;
      }
      return true;
    }

    public bool Write() {
      return true;
    }
  }
}
