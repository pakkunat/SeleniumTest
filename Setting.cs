using Nett;

namespace PLib {
  public class Setting {
    public bool IsNoWindow { get; private set; } = true;
    
    public string UserName { get; private set; } = string.Empty;

    public string Password { get; private set; } = string.Empty;

    public int AfterLaunchBrowser { get; private set; } = 0;

    public int AfterAccessApp { get; private set; } = 0;

    public int AfterLogin { get; private set; } = 0;

    public int AfterExplore { get; private set; } = 0;

    public int AfterMove { get; private set; } = 0;

    public int AfterSelect { get; private set; } = 0;

    public int AfterMoveToGuest { get; private set; } = 0;

    public List<string> Tags { get; private set; } = new List<string>();

    private Log log = Log.Instance;

    public bool Read(string filePath) {
      try {
        var toml = Toml.ReadFile(filePath);
        var general = toml.Get<TomlTable>("General");
        IsNoWindow = general.Get<bool>("NoWindow");
        var account = toml.Get<TomlTable>("Account");
        UserName = account.Get<string>("UserName");
        Password = account.Get<string>("Password");
        var delay = toml.Get<TomlTable>("Delay");
        AfterLaunchBrowser = delay.Get<int>("AfterLaunchBrowser");
        AfterAccessApp = delay.Get<int>("AfterAccessApp");
        AfterLogin = delay.Get<int>("AfterLogin");
        AfterExplore = delay.Get<int>("AfterExplore");
        AfterMove = delay.Get<int>("AfterMove");
        AfterSelect = delay.Get<int>("AfterSelect");
        AfterMoveToGuest = delay.Get<int>("AfterMoveToGuest");
        var explore = toml.Get<TomlTable>("Explore");
        Tags = explore.Get<List<string>>("Tags");
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
