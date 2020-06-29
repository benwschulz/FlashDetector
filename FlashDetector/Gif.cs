using System.Drawing;

namespace FlashDetector
{
    public class Gif
    {
        public Image data;

        public Gif(string path)
        {
            data = Image.FromFile(path);
        }
    }
}
