using System;
using System.Drawing;
using System.Xml;

namespace Steganography
{
    class Program
    {
        static void Main(string[] args)
        {
            Bitmap image = Utils.OpenImage("../../../../example.bmp");

            string secret ="test";

            Embed.EmbedMsg(Utils.TextToBin(secret), image);

            Console.WriteLine(Utils.BinToText(Exctract.ExtractMsg(image, 4)));

            Utils.SaveImage("../../../../hidden.bmp", image);
        }
    }
}