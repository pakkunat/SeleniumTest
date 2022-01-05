using System.Diagnostics;

namespace PLib {
  public class Log {
    private const string PATH = "./log";
    public static void Write(string message) {
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
