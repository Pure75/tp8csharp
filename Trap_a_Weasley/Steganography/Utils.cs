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
            char letter;
            int[] res = new int[8 * secret.Length];
            for (int i = 0; i < secret.Length; i++)
            {
                letter = secret[i];
                for (int j = 0; j < 8; j++)
                {
                    res[8 * i + j] = SaveLSB(letter >> 7-j, 1);
                }
            }
            return res;
        }

        // This function converts an array of bits into a string.
        // We consider that the array length is a multiple of 8.
        public static string BinToText(int[] bin)
        {
            int letter;
            string res = "";
            int taille = bin.Length/8;
            for (int i = 0; i < taille; i++)
            {
                letter = 0;
                for (int j = 0; j < 8; j++)
                {
                    letter += bin[i * 8 + j] << (7 - j);
                }
                res += (char)letter;
            }
            return res;
        }

        // This function extract the nbBits bits beginning from the index position from the secret array.
        // It is also converting the binary result into decimal.
        public static int ExtractBits(int[] secret, int index, int nbBits)
        {
            int res = 0;
            if (nbBits <= secret.Length)
            {
                for (int i = 0; i < nbBits; i++)
                {
                    if (index + i < secret.Length)
                    {
                        res += secret[index + i] << nbBits - i - 1;
                    }
                    else
                    {
                        res += 0;
                    }
                }
            }
            return res;
        }

        // This function translates the int value (in decimal) into binary in the secret array.
        public static void InsertBits(int[] secret, int index, int nbBits, int value)
        {
            for (int i = 0; i < nbBits; i++)
            {
                if (i+index < secret.Length)
                {
                    secret[index + i] = SaveLSB(value >> nbBits - i - 1, 1);
                }
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
    }
}