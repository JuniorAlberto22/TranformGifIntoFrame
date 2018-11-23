using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace GifHandle
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Processing...");
                Console.WriteLine("Origin: {0}", args[0]);
                Console.WriteLine("Destiny: {0}", args[1]);
                Console.WriteLine("Separating images...");
                List<Image> images = GifToArray(args[0]);
                Console.WriteLine("Saving images...");

                if(args.Length == 3)
                {
                    SaveImages(images, args[1], args[2]);
                }
                else
                {
                    SaveImages(images, args[1]);
                }
            }
            else
            {
                Console.WriteLine("Number of parameters are wrong.");
            }
            
        }

        public static void SaveImages(List<Image> images, string destiny, string prefix = "img")
        {
            ManegerFiles(destiny);

            int y = 0;
            foreach(var i in images)
            {
                var pathToSave = string.Format("{0}/{1}{2}.jpg", destiny, prefix, y);
                Console.WriteLine("Saving frame {0} to {1}", y + 1, pathToSave);
                i.Save(pathToSave, ImageFormat.Jpeg);
                y++;
            }
        }

        public static void ManegerFiles(string destiny)
        {
            if (!Directory.Exists(destiny))
            {
                Directory.CreateDirectory(destiny);
            }
            else
            {
                foreach (var f in Directory.GetFiles(destiny))
                {
                    File.Delete(f);
                }
            }
        }

        public static List<Image> GifToArray(string origin)
        {
            List<Image> images = new List<Image>();
            Console.WriteLine("Opening gif...");
            using (FileStream fs = File.Open(origin, FileMode.Open))
            {
                Console.WriteLine("Gif opend.");
                Image gif = Image.FromStream(fs);
                int imagesNumber = gif.GetFrameCount(FrameDimension.Time);
                Console.WriteLine("Number of frames: {0}", imagesNumber);
                
                for(int i = 0; i < imagesNumber; i++)
                {
                    Console.WriteLine("Getting frame {0}...", i);
                    gif.SelectActiveFrame(FrameDimension.Time, i);
                    images.Add(new Bitmap(gif));
                }
            }
            Console.WriteLine("All frames separated.");
            return images;
        }
    }
}
