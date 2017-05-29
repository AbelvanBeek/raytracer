using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Constants;
using Template;

namespace template
{
    class SkyDome
    {
        public Surface map;
        public Vector3[,] texture;

        public SkyDome(string file)
        {
            if (file != null)
            {
                map = new Surface(file);
                texture = new Vector3[map.width, map.height];
                for (int x = 0; x < map.width; x++)
                    for (int y = 0; y < map.width; y++)
                    { texture[x, y] = CalculateColor(map.pixels[map.width * y + x]); }
            }
        }
        public Vector3 color(Vector3 direction)
        {
            float r = (float) (1 / Math.PI) * (float) Math.Acos(direction.Z / Math.Sqrt((direction.X * direction.X) + (direction.Y * direction.Y)));
            return texture[(int) (direction.X * r * 500), (int) (direction.Y*r * 500 )];
        }
    }
}
