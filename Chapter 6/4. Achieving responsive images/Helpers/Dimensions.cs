using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreCookbook.Helpers
{
    public class Dimensions
    {
        public int ScreenSize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Dimensions(int screenSize, int width, int height)
        {
            ScreenSize = screenSize;
            Width = width;
            Height = height;
        }
    }

}