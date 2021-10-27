using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace Steganography
{
    public class Exctract
    {
        public static int[] ExtractMsg(Bitmap image, int length)
        {
            int[] arr = new int[length * 8];
            int index = 0;
            for (int i = 0; i < image.Width; i+=2)
            {
                for (int j = 0; j+1 < image.Height; j+=2)
                {
                    Color pixel1 = image.GetPixel(i,j);
                    Color pixel2 = image.GetPixel(i, j + 1);
                    int bitstoembed = Utils.SaveLSB(pixel2.R,3);
                    int bits = Utils.SaveLSB(pixel1.R, bitstoembed);
                    Utils.InsertBits(arr,index,bitstoembed,bits);
                    index += bitstoembed;
                    if (index == arr.Length) return arr;
                }
            }
            return arr;
        }
    }
}