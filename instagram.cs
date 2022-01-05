using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace PLib {
  public class Instagram {
    public enum Browser {
      Chrome
    }

    private const string URL = "https://www.instagram.com/";

    private ChromeDriver _chromeDriver;

    public bool LaunchBrowser(Browser browser, bool headless) {
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
        Log.Write($"Exception: launch chrome: {ex.Message}");
        return false;
      }
      return true;
    }

    public bool AccessInstagram() {
      // access website
      try {
        _chromeDriver.Navigate().GoToUrl(URL);
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        Log.Write($"Exception: browse instagram: {ex.Message}");
        return false;
      }
      return true;
    }
    
    public bool Login(string username, string password) {
      // input username
      try {
        _chromeDriver.FindElement(By.Name("username")).SendKeys(username);  
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        Log.Write($"Exception: input username: {ex.Message}");
        return false;
      }

      // input password
      try {
        _chromeDriver.FindElement(By.Name("password")).SendKeys(password);
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        Log.Write($"Exception: input password: {ex.Message}");
        return false;
      }

      // login
      try {
        _chromeDriver.FindElement(By.ClassName("L3NKy")).Click();
      } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        Log.Write($"Exception: login: {ex.Message}");
        return false;
      }
      return true;
    }

    public bool Explore() {

    }

    public bool Like() {
      
    }

    public void Quit() {
      _chromeDriver.Quit();
    }
  }
}
