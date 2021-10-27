using System;

namespace Crypto
{
    public class Vigenere
    {
        public static int mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        public static string vigenere_decode(string msg, string key)
        {
            string res = "";
            int x = key.Length;
            int y = 0;
            for (int i = 0; i < msg.Length; i++)
            {
                if (msg[i] == ' ')
                {
                    res += " ";
                }
                else
                {
                    res += (char)(mod(msg[i] - key[y % x],26)+'A');
                    y++;
                }
            }
            return res;
        }

        public static string vigenere_encode(string msg, string key)
        {          
            string res = "";
            int x = key.Length;
            int y = 0;
            for (int i = 0; i < msg.Length; i++)
            {
                if (msg[i] == ' ')
                {
                    res += " ";
                }
                else
                {
                    char maj1 = Char.ToUpper(msg[i]);
                    char maj2 = Char.ToUpper(key[y%x]);
                    res += (char)(mod(maj1 + maj2,26)+'A');
                    y++;
                }
            }
            return res;
        }
    }
}