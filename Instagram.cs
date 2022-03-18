using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace PLib {
  public class Instagram {
    public enum Browser {
      Chrome
    }

    private const string NOWINDOW = "--headless";
    private const string URL = "https://www.instagram.com/";
    private const string USERNAME = "username";
    private const string PASSWORD = "password";
    private const string LOGIN = "L3NKy";
    private const string EXPLORE = "https://www.instagram.com/explore/tags";
    private const string INDEX = "_9AhH0";
    private const string LIKE = "fr66n";
    private const string COMMENT_LIKE = "jdtwu";
    private const string GUEST = "_6lAjh";

    private Log log = Log.Instance;
    private ChromeDriver? _chromeDriver = null;

    public bool LaunchBrowser(Browser browser, bool nowindow) {
      // null check
      if (_chromeDriver != null) {
        log.Write("Already launched");
        return true;
      }

      // create browser options
      var options = new ChromeOptions();
      if (nowindow) {
        options.AddArgument(NOWINDOW);  
      }

      // get latest chrome driver
      var driverPath = string.Empty;
      try {
        var path = new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
        var split = path.Split("/");
        driverPath = $"./{split[split.Length - 4]}/{split[split.Length - 3]}/{split[split.Length - 2]}";
        log.Write($"driver path: {driverPath}");
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        log.Write($"Exception: get chrome driver: {ex.Message}");
        return false;
      }

      // launch browser
      try {
        _chromeDriver = new ChromeDriver(driverPath, options);
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        log.Write($"Exception: launch chrome: {ex.Message}");
        return false;
      }
      return true;
    }

    public bool AccessApp() {
      // null check
      if (_chromeDriver == null) {
        log.Write("Not launch yet");
        return false;
      }

      // access website
      try {
        _chromeDriver?.Navigate().GoToUrl(URL);
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        log.Write($"Exception: browse app: {ex.Message}");
        return false;
      }
      return true;
    }
    
    public bool Login(string username, string password) {
      // null check
      if (_chromeDriver == null) {
        log.Write("Not launch yet");
        return false;
      }

      // input username
      try {
        _chromeDriver?.FindElement(By.Name(USERNAME)).SendKeys(username);  
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        log.Write($"Exception: input username: {ex.Message}");
        return false;
      }

      // input password
      try {
        _chromeDriver?.FindElement(By.Name(PASSWORD)).SendKeys(password);
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        log.Write($"Exception: input password: {ex.Message}");
        return false;
      }

      // login
      try {
        _chromeDriver?.FindElement(By.ClassName(LOGIN)).Click();
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        log.Write($"Exception: login: {ex.Message}");
        return false;
      }
      return true;
    }

    public bool Explore(string tag) {
      // null check
      if (_chromeDriver == null) {
        log.Write("Not launch yet");
        return false;
      }

      try {
        _chromeDriver?.Navigate().GoToUrl($"{EXPLORE}/{tag}");
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        log.Write($"Exception: explore: {ex.Message}");
        return false;
      }
      return true;
    }

    public bool Move(int index) {
      // null check
      if (_chromeDriver == null) {
        log.Write("Not launch yet");
        return false;
      }

      try {
        var target = _chromeDriver?.FindElements(By.ClassName(INDEX))[index];
        var actions = new Actions(_chromeDriver);
        actions.MoveToElement(target);
        actions.Perform();
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        log.Write($"Exception: move: {ex.Message}");
        return false;
      }
      return true;
    }

    public bool Select(int index) {
      // null check
      if (_chromeDriver == null) {
        log.Write("Not launch yet");
        return false;
      }

      try {
        _chromeDriver?.FindElements(By.ClassName(INDEX))[index].Click();
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        log.Write($"Exception: select: {ex.Message}");
        return false;
      }
      return true;
    }

    public bool CommentLike() {
      // null check
      if (_chromeDriver == null) {
        log.Write("Not launch yet");
        return false;
      }

      try {
        _chromeDriver?.FindElement(By.ClassName(COMMENT_LIKE)).Click();
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        log.Write($"Exception: comment like: {ex.Message}");
        return false;
      }
      return true;
    }

    public bool Like() {
      // null check
      if (_chromeDriver == null) {
        log.Write("Not launch yet");
        return false;
      }

      try {
        _chromeDriver?.FindElement(By.ClassName(LIKE)).Click();
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        log.Write($"Exception: like: {ex.Message}");
        return false;
      }
      return true;
    }

    public bool MoveToGuest(int index) {
      // null check
      if (_chromeDriver == null) {
        log.Write("Not launch yet");
        return false;
      }

      try {
        _chromeDriver?.FindElements(By.ClassName(GUEST))[index].Click();
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        log.Write($"Exception: move to guest: {ex.Message}");
        return false;
      }
      return true;
    }

    public void Quit() {
      _chromeDriver?.Quit();
      _chromeDriver = null;
    }
  }
}
