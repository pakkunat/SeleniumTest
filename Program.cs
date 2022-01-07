using PLib;

var log = Log.Instance;
log.Write("Start!");
Console.WriteLine("Welcome to automatic likes in instagram");

// load setting file
var setting = new Setting();
if (!setting.Read("./setting/setting.toml")) {
  log.Write($"Quit");
  return;
}
log.Write($"IsNoWindow: {setting.IsNoWindow}");
log.Write($"UserName: {setting.UserName}");
log.Write($"Password: {setting.Password}");
log.Write($"AfterLaunchBrowser: {setting.AfterLaunchBrowser} ms");
log.Write($"AfterAccessApp: {setting.AfterAccessApp} ms");
log.Write($"AfterLogin: {setting.AfterLogin} ms");
log.Write($"AfterExplore: {setting.AfterExplore} ms");
log.Write($"AfterMove: {setting.AfterMove} ms");
log.Write($"AfterSelect: {setting.AfterSelect} ms");
var tags = string.Empty;
foreach (var t in setting.Tags) {
  tags += $"{t},";
}
log.Write($"Tags: {tags}");

// launch browser
var instagram = new Instagram();
if (!instagram.LaunchBrowser(Instagram.Browser.Chrome, setting.IsNoWindow)) {
  log.Write($"Quit");
  return;
}
Thread.Sleep(setting.AfterLaunchBrowser);

// access website
if (!instagram.AccessApp()) {
  instagram.Quit();
  log.Write($"Quit");
  return;
}
Thread.Sleep(setting.AfterAccessApp);

// login
if (!instagram.Login(setting.UserName, setting.Password)) {
  instagram.Quit();
  log.Write($"Quit");
  return;
}
Thread.Sleep(setting.AfterLogin);

// explore tags
if (!instagram.Explore(setting.Tags[0])) {
  instagram.Quit();
  log.Write($"Quit");
  return;
}
Thread.Sleep(setting.AfterExplore);

// move to latest image
if (!instagram.Move(9)) {
  instagram.Quit();
  log.Write($"Quit");
  return;
}
Thread.Sleep(setting.AfterMove);

// click image
if (!instagram.Select(9)) {
  instagram.Quit();
  log.Write($"Quit");
  return;
}
Thread.Sleep(setting.AfterSelect);

// do comment like
if (!instagram.CommentLike()) {
  instagram.Quit();
  log.Write($"Quit");
  return;
}

#if false
// 1. フォローの多いアカウントの投稿にコメントしている人にいいねする
// 2. コメントしている人に入って投稿にいいねする（2〜5個）
// 3. 自動でコメントやDMできるかどうか

// like
if (!instagram.Like()) {
  instagram.Quit();
  log.Write($"Quit");
  return;
}
#endif

var key = ConsoleKey.NoName;
do {
  key = Console.ReadKey().Key;
} while (key != ConsoleKey.Escape);

// close instance
instagram.Quit();
log.Write($"Quit");
