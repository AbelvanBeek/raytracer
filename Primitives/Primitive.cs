using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template;
using static Constants;

namespace template
{
    public abstract class Primitive
    {
        public Vector3 position;
        public Vector3 color;
        public float reflectiveness;
        public float gloss;
        public Surface map;
        public Vector3[,] texture;
        public bool textured;
        public bool glass = false;

        public Primitive(Vector3 position, Vector3 color, float reflectiveness, float gloss, string texfile)
        {
            this.position = position;
            this.color = color;
            this.reflectiveness = reflectiveness;
            this.gloss = gloss;

            if (texfile != null)
            {
                textured = true;
                map = new Surface(texfile);
                texture = new Vector3[map.width, map.height];
                for (int x = 0; x < map.width; x++)
                    for (int y = 0; y < map.width; y++)
                    { texture[x, y] = CalculateColor(map.pixels[map.width * y + x]); }
            }
        }

        public virtual void DrawPrimitive() { }
        public virtual Intersection CalculateIntersection(Ray ray) { return null; }

        public virtual Vector3 Normal(Vector3 intersection, Vector3 rayDir)
        {
            return new Vector3(0, 0, 0);
        }

    }
}
