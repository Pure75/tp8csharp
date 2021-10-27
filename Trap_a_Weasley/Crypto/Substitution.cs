using System;
using System.Collections.Generic;

namespace Crypto
{
    public class Substitution
    {
        public static Dictionary<string, char> Morse = Utils.Morse;
        
        public static string Morse_decode(string message)
        {
            string msg = "";
            string[] list = message.Split();
            foreach (string elem in list)
            {
                if (!Morse.ContainsKey(elem))
                {
                    return null;
                }
                else
                    foreach (KeyValuePair<string,char> key in Morse)
                    {
                        if (key.Key == elem)
                        {
                            msg += key.Value;
                        }
                    }
            }
            return msg;
        }
        
        public static string Morse_encode(string message)
        {
            string res = "";
            foreach (char lettre in message)
            {
                if (!Morse.ContainsValue(lettre)) return null;
                foreach (KeyValuePair<string,char> l in Morse)
                {
                    if (l.Value == lettre)
                    {
                        res += l.Key;
                        res += " ";
                    }
                }
            }
            return res;
        }
    }
}