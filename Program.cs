using PLib;

const string SETTING_FILE = "./setting/setting.txt";
const int WAIT_MIN = 60;
const int WAIT_MAX = 300;
const int LIKE_MIN = 2;
const int LIKE_MAX = 5;

var log = Log.Instance;
log.Write("Start --------------------");
Console.WriteLine("Welcome to insta program");

// write exit flag (false)
var setting = new Setting();
if (!setting.WriteExit(SETTING_FILE, false)) {
  log.Write("Quit");
  return;
}

// load setting file
if (!setting.Read(SETTING_FILE)) {
  log.Write("Quit");
  return;
}
log.Write($"Browser: {setting.Browser}");
log.Write($"IsNoWindow: {setting.IsNoWindow}");
log.Write($"IsExit: {setting.IsExit}");
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
if (!instagram.LaunchBrowser(instagram.GetBrowser(setting.Browser), setting.IsNoWindow)) {
  log.Write("Quit");
  return;
}
Thread.Sleep(setting.AfterLaunchBrowser);

// access website
if (!instagram.AccessApp()) {
  instagram.Quit();
  log.Write("Quit");
  return;
}
Thread.Sleep(setting.AfterAccessApp);

// login
if (!instagram.Login(setting.UserName, setting.Password)) {
  instagram.Quit();
  log.Write("Quit");
  return;
}
Thread.Sleep(setting.AfterLogin);

// search loop
foreach (var t in setting.Tags.Keys.ToList<string>()) {
  // explore tag
  if (!instagram.Explore(t)) {
    var r = new Random().Next(WAIT_MIN, WAIT_MAX - 1);
    log.Write($"Random Wait: {r} ms");
    Thread.Sleep(r * 1000);
    continue;
  }
  Thread.Sleep(setting.AfterExplore);

  // select loop
  var selectedIndex = 0;
  do {
    // click image
    if (!instagram.Select(selectedIndex)) {
      break;
    }
    Thread.Sleep(setting.AfterSelect);

    // get guest count
    var guestCount = (int?)0;
    if (!instagram.GetGuestCount(ref guestCount)) {
      break;
    }
    log.Write($"Guest Count: {guestCount}");

    for (var i = 0; i < guestCount; i++) {
      // move to guest
      if (!instagram.MoveToGuest(i + 1)) {
        continue;
      }
      Thread.Sleep(setting.AfterMoveToGuest);

      // decide number of likes (random)
      var r = new Random().Next(LIKE_MIN, LIKE_MAX - 1);
      log.Write($"Like Count: {r}");

      // like loop
      for (var j = 0; j < r; j++) {
        // do like
        //if (!instagram.Like()) {
        //  break;
        //}

        for(;;) {}

        if (!instagram.Back()) {
          continue;
        }
      }

      for(;;){}
    }

    // check exit flag
    if (!setting.ReadExit(SETTING_FILE)) {
      break;
    }
    if (setting.IsExit) {
      break;
    }

    selectedIndex++;
    for(;;){}
  } while (!setting.IsExit);

  // check exit flag
  if (setting.IsExit) {
    break;
  }
}

// close instance
instagram.Quit();
log.Write("Quit");
