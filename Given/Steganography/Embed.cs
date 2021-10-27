using System;
using System.Collections.Generic;
using System.Drawing;

namespace Steganography
{
    public class Embed
    {
        public static void EmbedMsg(int[] secret, Bitmap image)
        {
            int index = 0;
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j + 1 < image.Height; j += 2)
                {
                    // Get the two colors of the pair of pixels.
                    Color color1 = image.GetPixel(i, j);
                    Color color2 = image.GetPixel(i, j + 1);
                    
                    // Get the color difference between the pixels.
                    int difference = Utils.GetDifference(color1, color2);
                    
                    // Get the number of bits to embed into the first pixel.
                    int bitsToEmbed = GetBitsToEmbed(difference);

                    // Getting the secret message bits to embed.
                    int newLSB = Utils.ExtractBits(secret, index, bitsToEmbed);
                    
                    // Getting the new color of the fist pixel (the one containing the message).
                    int newColor = Utils.ReplaceLSB(color1.R , bitsToEmbed, newLSB);
                    
                    // Updating the pixels colors : the first is the color with the message and the second is the color
                    // with the LSB containing the number of bits that are dedicated to the message in the first.
                    (int new1, int new2) = (newColor, Utils.ReplaceLSB(color2.R, 3, bitsToEmbed));
                    image.SetPixel(i, j, Color.FromArgb(new1, new1, new1));
                    image.SetPixel(i, j + 1, Color.FromArgb(new2, new2, new2));
                    
                    index += bitsToEmbed;

                    if (index > secret.Length)
                        return;
                }
            }
        }

        // This function is getting the number of bits to embed thanks to the difference and the Quantization table
        // in Utils.cs.
        public static int GetBitsToEmbed(int difference)
        {
            int hiddenbits = 0;
            foreach (KeyValuePair<int[],int> elem in Utils.QuantizationTable)
            {
                if (difference <= elem.Key[1] || difference >= elem.Key[0])
                {
                    hiddenbits = elem.Value;
                    break;
                }
            }
            return hiddenbits;
        }
        
    }
}