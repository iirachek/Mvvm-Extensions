using System;

namespace MvvmExtensions.Samples
{
    static class MathHelper
    {
        public static int CalculateRectangleArea(int width, int height)
        {
            if (width < 0)
                throw new ArgumentException("Width cannot be less than zero");

            if (height < 0)
                throw new ArgumentException("Height cannot be less than zero");

            return width * height;
        }

        public static int CalculateRectanglePerimeter(int width, int height)
        {
            if (width < 0)
                throw new ArgumentException("Width cannot be less than zero");

            if (height < 0)
                throw new ArgumentException("Height cannot be less than zero");

            return width * 2 + height * 2;
        }
    }
}
