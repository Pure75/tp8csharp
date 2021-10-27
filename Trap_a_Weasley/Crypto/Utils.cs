using System.Collections.Generic;

namespace Crypto
{
    public class Utils
    {
        private static Dictionary<string, char> Init_morse()
        {
            Dictionary<string, char> Morse = new Dictionary<string, char>();
            Morse.Add(".-", 'A');
            Morse.Add("-...", 'B');
            Morse.Add("-.-.", 'C');
            Morse.Add("-..", 'D');
            Morse.Add(".", 'E');
            Morse.Add("..-.", 'F');
            Morse.Add("--.", 'G');
            Morse.Add("....", 'H');
            Morse.Add("..", 'I');
            Morse.Add(".---", 'J');
            Morse.Add("-.-", 'K');
            Morse.Add(".-..", 'L');
            Morse.Add("--", 'M');
            Morse.Add("-.", 'N');
            Morse.Add("---", 'O');
            Morse.Add(".--.", 'P');
            Morse.Add("--.-", 'Q');
            Morse.Add(".-.", 'R');
            Morse.Add("...", 'S');
            Morse.Add("-", 'T');
            Morse.Add("..-", 'U');
            Morse.Add("...-", 'V');
            Morse.Add(".--", 'W');
            Morse.Add("-..-", 'X');
            Morse.Add("-.--", 'Y');
            Morse.Add("--..", 'Z');
            Morse.Add("..--.-", ' ');

            return Morse;
        }
        
        public static Dictionary<string, char> Morse = Init_morse();
    }
}