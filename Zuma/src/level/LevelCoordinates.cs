using System.Windows;

namespace Zuma.src.level
{
    public struct LevelCoordinates
    {
        public Point Frog { get; private set; }

        public LevelCoordinates(Point frog)
        {
            Frog = frog;
        }
    }
}
