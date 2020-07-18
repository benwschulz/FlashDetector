using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace FlashDetector
{
    /// <summary>
    /// All the various pieces of data we need for a given gif
    /// </summary>
    public class Gif
    {
        public Image data; // Base information about a particular gif
        public bool safe; // Whether or not this gif is safe for photosensitive epileptics to view
        public List<double> brightnessValues; // A collection of the brightnesses of each frame in the gif
        private double brightnessLevelTolerance = 0.015; // The measure of how much change in brightness between frames is acceptable
        
        //TODO: calculate the tolerable *speed* of a change in brightness in addition to the tolerable intensity
        public Gif(string path)
        {
            safe = false; // too dangerous to give the gif the benefit of the doubt
            data = Image.FromFile(path); // load base gif data
            FrameDimension frameCollection = new FrameDimension(data.FrameDimensionsList[0]); // a collection of each frame in the gif

            int frameCount = data.GetFrameCount(frameCollection); // the total number of frames in this gif
            brightnessValues = new List<double>(); 
            
            for (int i = 0; i < frameCount; i++)
            {
                addToBrightnessCollection(frameCollection, i);
            }

            determineSafety();
        }

        private void determineSafety()
        {
            double brightnessDelta;

            for (int i = 0; i < brightnessValues.Count - 1; i++)
            {
                brightnessDelta = Math.Abs(brightnessValues[i] - brightnessValues[i + 1]);
                if (brightnessDelta > brightnessLevelTolerance)
                    return;
            }

            safe = true;
        }

        /// <summary>
        /// Gets the brightness of a frame and adds it to the brightness collection
        /// </summary>
        /// <param name="dimension">The collection of frames in the gif</param>
        private void addToBrightnessCollection(FrameDimension dimension, int index)
        {
            List<double> pixelBrightnesses = new List<double>();
            data.SelectActiveFrame(dimension, index);
            Bitmap map = new Bitmap(data);

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    pixelBrightnesses.Add(map.GetPixel(x, y).GetBrightness());
                }
            }

            brightnessValues.Add(getAverageBrightness(pixelBrightnesses));
        }

        /// <summary>
        /// Gets the average brightness of a given frame (0 == Black, 1.0 == White)
        /// </summary>
        /// <param name="values">The brightness of each pixel in the values parameter</param>
        /// <returns>The average brightness of the collection's pixels</returns>
        private double getAverageBrightness(List<double> values)
        {
            double totalBrightness = 0;

            foreach (double value in values)
            {
                totalBrightness += value;
            }

            return totalBrightness / values.Count;
        }
    }
}
