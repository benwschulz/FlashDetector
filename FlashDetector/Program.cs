using System.Collections.Generic;
using System.IO;

namespace FlashDetector
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = @"C:\Users\benws\source\repos\FlashDetector\FlashDetector\Gifs";
            string[] paths = Directory.GetFiles(directory);
            List<Gif> gifs = new List<Gif>();

            foreach (string path in paths)
            {
                gifs.Add(new Gif(path));
            }
        }
    }
}
