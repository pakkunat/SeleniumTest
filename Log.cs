using System.Diagnostics;

namespace PLib {
  public class Log {
    private const string PATH = "./log";

    private static Log _instance = new Log();
    public static Log Instance { get { return _instance; } }
    
    private Log() {}

    public void Write(string message) {
      var now = DateTime.Now;
      var contents = $"[{now:yyyyMMdd HH:mm:ss.fff}] {message}";
      Debug.Print(contents);
      try {
        if (!Directory.Exists(PATH)) {
          Directory.CreateDirectory(PATH);
        }
        File.AppendAllText($"{PATH}/{now:yyyyMMdd}_log.txt", $"{contents}\n");
      } catch {
      }
    }
  }
}
