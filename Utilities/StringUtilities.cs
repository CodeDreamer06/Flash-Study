using System;

namespace FlashStudy.Utilities
{
  class StringUtilities {
    public static int splitInteger(string word, string keyword, string errorMessage) {
        try {
          return Convert.ToInt32(word.Split(keyword)[1]);
        }
        catch(System.FormatException) {
          Console.WriteLine(errorMessage);
          return 0;
        }
      }
  }
}
