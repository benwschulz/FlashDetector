using System.IO;

namespace FlashDetector
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = @"C:\Users\benws\source\repos\FlashDetector\FlashDetector\Gifs";
            var gifs = Directory.GetFiles(directory);

            var foo = new Gif(gifs[0]);
        }
    }
}
