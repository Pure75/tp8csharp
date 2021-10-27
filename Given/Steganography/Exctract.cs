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
            List<int> tmp = new List<int>();
            int[] arr = tmp.ToArray();
            int index = 0;
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j+1 < image.Height; j+=2)
                {
                    // On récupere la paire de pixels
                    Color pixel1 = image.GetPixel(i,j);
                    Color pixel2 = image.GetPixel(i, j + 1);
                    
                    //On transforme la couleur en string
                    string stringvalue = pixel2.ToString();
                    //On transforme le string en tableau
                    int[] binaryvalue = Utils.TextToBin(stringvalue);
                    //On récuere les 3 derniers bits
                    int bits = Utils.ExtractBits(binaryvalue, 5, 3);
                    //On récupere la valeur correspondante dans le tableau
                    int bitstoembed = Embed.GetBitsToEmbed(bits);
                    
                    //On récupere les x derniers bits du premier pixel
                    string stringvalue2 = pixel1.ToString();
                    int[] binaryvalue2 = Utils.TextToBin(stringvalue2);
                    int start = binaryvalue2.Length - bitstoembed;
                    int decimalvalue = Utils.ExtractBits(binaryvalue2,start , bitstoembed);
                    
                    //On insere les bits dans notre tableau
                    Utils.InsertBits(arr,index,bitstoembed,decimalvalue);
                    
                    //On implemente l'index, et pose notre condition d'arrêt
                    index += bitstoembed;
                    if (index > length) return arr;
                }
            }
            return arr;
        }
    }
}