using System;
using System.Windows;

namespace Zuma.src.helpers
{
    public static class Utils
    {
        public static readonly GridLengthConverter gridLengthConverter = new GridLengthConverter();

        public static double AddAngels(double angel1, double angel2)
        {
            double sum = angel1 + angel2;
            return Math.Abs(sum) <= 360 ? sum : sum > 0 ? sum - 360 : sum + 360;
        }

        public static float AddAngels(float angel1, float angel2)
        {
            double sum = angel1 + angel2;
            return (float) ( Math.Abs(sum) <= 360 ? sum : sum > 0 ? sum - 360 : sum + 360 );
        }
    }
}
