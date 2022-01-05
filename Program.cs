using PLib;

Log.Write("Start!");
Console.WriteLine("Welcome to automatic likes in instagram");

// query as headless
Console.WriteLine("Do you wanna launch app as headless? (y/n)");
var headless = Console.ReadLine();
Log.Write($"headless: {headless}");

// input instagram user name and password
Console.WriteLine("Type your instagram user name");
var username = $"{Console.ReadLine()}";
Log.Write($"username: {username}");
Console.WriteLine("Type your instagram password");
var password = $"{Console.ReadLine()}";

// launch browser
var instagram = new Instagram();
if (!instagram.LaunchBrowser(Instagram.Browser.Chrome, headless == "y")) {
  Log.Write($"Quit");
  return;
}
Thread.Sleep(1000);

// access website
if (!instagram.AccessInstagram()) {
  instagram.Quit();
  Log.Write($"Quit");
  return;
}
Thread.Sleep(1000);

// login
if (!instagram.Login(username, password)) {
  instagram.Quit();
  Log.Write($"Quit");
  return;
}
Thread.Sleep(5000);

// 1. フォローの多いアカウントの投稿にコメントしている人にいいねする
// 2. コメントしている人に入って投稿にいいねする（2〜5個）
// 3. 自動でコメントできるかどうか

#if false
//var key = Console.ReadKey().Key;
//if (key == ConsoleKey.A) {
  // search tags
  driver.Navigate().GoToUrl("https://www.instagram.com/explore/tags/camera");
  Thread.Sleep(3000);
//}

// move to 10th image
var target = driver.FindElements(By.ClassName("_9AhH0"))[10];
var actions = new Actions(driver);
actions.MoveToElement(target);
actions.Perform();
Thread.Sleep(1000);

// click latest image
driver.FindElements(By.ClassName("_9AhH0"))[9].Click();
Thread.Sleep(1000);
driver.FindElement(By.ClassName("fr66n")).Click();
#endif

ConsoleKey key;
do {
  key = Console.ReadKey().Key;
} while (key != ConsoleKey.Escape);

// close instance
instagram.Quit();
Log.Write($"Quit");
