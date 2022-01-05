using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace PLib {
  public class Instagram {
    public enum Browser {
      Chrome
    }

    private const string URL = "https://www.instagram.com/";

    private Log log = Log.Instance;
    private ChromeDriver? _chromeDriver = null;

    public bool LaunchBrowser(Browser browser, bool headless) {
      // null check
      if (_chromeDriver != null) {
        log.Write($"Already launched");
        return true;
      }

      // create browser options
      var options = new ChromeOptions();
      if (headless) {
        options.AddArgument("--headless");  
      }

      // launch browser
      try {
        _chromeDriver = new ChromeDriver(options);
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        log.Write($"Exception: launch chrome: {ex.Message}");
        return false;
      }
      return true;
    }

    public bool AccessInstagram() {
      // null check
      if (_chromeDriver == null) {
        log.Write($"Not launch yet");
        return false;
      }

      // access website
      try {
        _chromeDriver?.Navigate().GoToUrl(URL);
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        log.Write($"Exception: browse instagram: {ex.Message}");
        return false;
      }
      return true;
    }
    
    public bool Login(string username, string password) {
      // null check
      if (_chromeDriver == null) {
        log.Write($"Not launch yet");
        return false;
      }

      // input username
      try {
        _chromeDriver?.FindElement(By.Name("username")).SendKeys(username);  
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        log.Write($"Exception: input username: {ex.Message}");
        return false;
      }

      // input password
      try {
        _chromeDriver?.FindElement(By.Name("password")).SendKeys(password);
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        log.Write($"Exception: input password: {ex.Message}");
        return false;
      }

      // login
      try {
        _chromeDriver?.FindElement(By.ClassName("L3NKy")).Click();
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        log.Write($"Exception: login: {ex.Message}");
        return false;
      }
      return true;
    }

    public bool Explore() {
      // null check
      if (_chromeDriver == null) {
        log.Write($"Not launch yet");
        return false;
      }

      _chromeDriver?.Navigate().GoToUrl("https://www.instagram.com/explore/tags/camera");
  
      return true;
    }

    public bool Move() {
      // null check
      if (_chromeDriver == null) {
        log.Write($"Not launch yet");
        return false;
      }

      var target = _chromeDriver?.FindElements(By.ClassName("_9AhH0"))[10];
      var actions = new Actions(_chromeDriver);
      actions.MoveToElement(target);
      actions.Perform();

      return true;
    }

    public bool Select() {
      // null check
      if (_chromeDriver == null) {
        log.Write($"Not launch yet");
        return false;
      }

      _chromeDriver?.FindElements(By.ClassName("_9AhH0"))[9].Click();

      return true;
    }

    public bool Like() {
      // null check
      if (_chromeDriver == null) {
        log.Write($"Not launch yet");
        return false;
      }

      _chromeDriver?.FindElement(By.ClassName("fr66n")).Click();

      return true;
    }

    public void Quit() {
      _chromeDriver?.Quit();
      _chromeDriver = null;
    }
  }
}
