using PLib;

var log = Log.Instance;
log.Write("Start!");
Console.WriteLine("Welcome to automatic likes in instagram");

// if use setting file
Console.WriteLine("Do you use your setting file? (y/n");
var setting = Console.ReadLine();
log.Write($"setting: {setting}");

var headless = string.Empty;
var username = string.Empty;
var password = string.Empty;
if (setting == "y") {

} else {
  // if use headless
  Console.WriteLine("Do you wanna launch app as headless? (y/n)");
  headless = Console.ReadLine();
  log.Write($"headless: {headless}");

  // input instagram user name and password
  Console.WriteLine("Type your instagram user name");
  username = $"{Console.ReadLine()}";
  log.Write($"username: {username}");
  Console.WriteLine("Type your instagram password");
  password = $"{Console.ReadLine()}";
}

// launch browser
var instagram = new Instagram();
if (!instagram.LaunchBrowser(Instagram.Browser.Chrome, headless == "y")) {
  log.Write($"Quit");
  return;
}
//Thread.Sleep(1000);

// access website
if (!instagram.AccessInstagram()) {
  instagram.Quit();
  log.Write($"Quit");
  return;
}
Thread.Sleep(1000);

// login
if (!instagram.Login(username, password)) {
  instagram.Quit();
  log.Write($"Quit");
  return;
}
Thread.Sleep(5000);

#if false
// 1. フォローの多いアカウントの投稿にコメントしている人にいいねする
// 2. コメントしている人に入って投稿にいいねする（2〜5個）
// 3. 自動でコメントやDMできるかどうか

// explore tags
if (!instagram.Explore()) {
  instagram.Quit();
  log.Write($"Quit");
  return;
}
Thread.Sleep(3000);

// move to 10th image
if (!instagram.Move()) {
  instagram.Quit();
  log.Write($"Quit");
  return;
}
Thread.Sleep(1000);

// click latest image
if (!instagram.Select()) {
  instagram.Quit();
  log.Write($"Quit");
  return;
}
Thread.Sleep(1000);

// like
if (!instagram.Like()) {
  instagram.Quit();
  log.Write($"Quit");
  return;
}

ConsoleKey key;
do {
  key = Console.ReadKey().Key;
} while (key != ConsoleKey.Escape);
#endif

// close instance
instagram.Quit();
log.Write($"Quit");
