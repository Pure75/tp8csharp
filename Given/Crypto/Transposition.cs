using System;

namespace Crypto
{
    public class Transposition
    {
        public static int[] Permutation_rule(string key)
        {
            int x = key.Length;
            int y = 0;
            key = key.ToUpper();
            int[] arr = new int[x];
            for (int i = 0; i < x; i++)
            {
                int res = 1;
                for (int j = 0; j < x; j++)
                {
                    if (key[j] <= key[i] && i!=j)
                    {
                        res ++;
                    }
                }
                arr[y] = res;
                y++;
            }
            return arr;
        }

        public static int search_place(int[] key, int i)
        {
            throw new NotImplementedException();
        }
        
        public static char[,] Create_table_decrypt(string message, string key, int size)
        {
            int[] list = Permutation_rule(key);
            int x = key.Length;
            int y = 0;
            char[,] arr = new Char[size, x];
            foreach (int element in list)
            {
                for (int i = 0; i < size; i++)
                {
                    arr[i, x - element] = message[i+y];
                }
                y += size;
            }
            return arr;
        }

        public static string Permutation_decrypt(string message, string key)
        {
            string res = "";
            int taille1 = message.Length;
            int taille2 = key.Length;
            int size = taille1 / taille2;
            char[,] list = Create_table_decrypt(message, key, size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < taille2; j++)
                {
                    if (i == size / 2 && j == taille2 / 2)
                    {
                        res += " ";
                    }
                    res += list[i, j];
                }
            }
            return res;
        }

        public static char[,] Create_table_encrypt(string message, string key, int size)
        {
            int x = key.Length;
            int y = 0;
            char[,] arr = new Char[size, x];
            {
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (message[y] == ' ')
                        {
                            arr[i, j] = ' ';
                            y++;
                        }
                        arr[i, j] = message[y];
                            y++;
                    }
                }
                return arr;
            }
        }

        public static string Permutation_encrypt(string message, string key)
        {
            string phrase = "";
            int size = message.Length / key.Length;
            char[,] arr = Create_table_encrypt(message, key, size);
            foreach (int element in Permutation_rule(key))
            {
                int x = key.Length - element;
                for (int i = 0; i < key.Length; i++)
                {
                    phrase += arr[i, x];
                }
                phrase += " ";
            }
            return phrase;
        }
    }
}