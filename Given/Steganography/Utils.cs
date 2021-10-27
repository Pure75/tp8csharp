using System;
using System.Collections.Generic;
using System.Drawing;

namespace Steganography
{
    public class Utils
    {
        // This Quantization table is used to determine the number of bits to use to hide a message in a pixel.
        static public Dictionary<int[] , int> QuantizationTable = new Dictionary<int[], int>()
        {
            { new int[] {0, 1}, 1},
            { new int[] {2, 32}, 2},
            { new int[] {33, 64}, 3},
            { new int[] {65, 255}, 4}
        };

        // This function converts a string into an array of bits.
        public static int[] TextToBin(string secret)
        {
            List<int> list = new List<int>();
            int z = -8;
            foreach (char letter in secret)
            {
                z += 8;
                int cpt = 0;
                int x = (int) (letter);
                while (x > 0)
                {
                    int y = x % 2;
                    list.Insert(z,y);
                    cpt++;
                    x = x / 2;
                }
                if (cpt < 8)
                {
                    while (cpt < 8)
                    {
                        list.Insert(z,0);
                        cpt++;
                    }
                }
            }
            return list.ToArray();
        }

        // This function converts an array of bits into a string.
        // We consider that the array length is a multiple of 8.
        public static string BinToText(int[] bin)
        {
            int index = 0;
            int i = 7;
            int tmp = 0;
            string res = "";
            int l = bin.Length;
            while (l > 0)
            {
                tmp = 0;
                int elem = 0;
                while (i >= 0)
                {
                    elem = bin[index] * (power(2, i));
                    tmp += elem;
                    index++;
                    i--;
                }
                res = res + (char)(tmp);
                i = 7;
                l = l - 8;
            }
            return res;
        }

        // This function extract the nbBits bits beginning from the index position from the secret array.
        // It is also converting the binary result into decimal.
        public static int ExtractBits(int[] secret, int index, int nbBits)
        {
            int elem;
            int res = 0;
            int i = nbBits - 1;
            while (i >= 0)
            {
                if (index == secret.Length)
                {
                    elem = 0;
                }
                else {elem = secret[index] * (power(2, i));}
                res += elem;
                index++;
                i--;
            }
            return res;
        }

        // This function translates the int value (in decimal) into binary in the secret array.
        public static void InsertBits(int[] secret, int index, int nbBits, int value)
        {
            int i = 0;
            int[] binaryvalue = tobinary(value, nbBits);
            while (i < nbBits && index < secret.Length)
            {
                secret[index] = binaryvalue[i];
                index++;
                i++;
            }
        }

        // Assuming we already have grey pixels (R = G = B).
        // This is | pixel1.R - pixel2.R |.
        public static int GetDifference(Color pixel1, Color pixel2)
        {
            int pixelcolor1 = pixel1.R;
            int pixelcolor2 = pixel2.R;
            return Math.Abs(pixelcolor1 - pixelcolor2);
        }

        
        // This function opens an image.
        public static Bitmap OpenImage(string path)
        {
            return new Bitmap(path);
        }

        // This function saves the image into the file 'name'.
        public static void SaveImage(string name, Bitmap image)
        {
            image.Save(name);
            image.Dispose();
        }
        
        // This function clears the nbBits LSB of the int color.
        public static int ClearLSB(int color, int nbBits)
        {
            if (color <= 0)
                return color;
            color >>= nbBits;
            color <<= nbBits;
            return color;
        }
        
        // This function replaces the nbBits LSB by newLSB.
        public static int ReplaceLSB(int color, int nbBits, int newLSB)
        {
            color = ClearLSB(color, nbBits);

            return color + newLSB;
        }
        
        // This function saves ONLY the nbBits LSB of the int color.
        public static int SaveLSB(int color, int nbBits)
        {
            if (color <= 0)
                return color;
            color <<= 8 - nbBits;
            color %= 256;
            color >>= 8 - nbBits;
            return color;
        }

        public static int power(int n, int p)
        {
            if (p == 0) return 1;
            int tmp = n;
            while (p > 1)
            {
                p--;
                n = tmp * n;
            }
            return n;
        }

        public static int[] tobinary(int x, int bits) //Transforme un entier en binaire codé en x bits.
        {
            List<int> tmp = new List<int>();
            int y;
            while (x > 0)
            {
                y = x % 2;
                tmp.Insert(0, y);
                x = x / 2;
                bits--;
            }
            while (bits > 0)
            {
                tmp.Insert(0,0);
                bits--;
            }
            return tmp.ToArray();
        }
    }
}