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
    class Plane : Primitive
    {
        Vector3 normal;
        Vector3 distance;

        public Plane(Vector3 normal, Vector3 position, Vector3 color, float reflectiveness, float gloss, string file) : base(position, color, reflectiveness, gloss, file)
        {
            this.normal = normal;
        }
        public override Intersection CalculateIntersection(Ray ray)
        {
            Vector3 intersection;
            float t = (Dot(position - ray.origin, normal) / Dot(ray.direction, normal));
            if (t < 0) 
                return null;
            intersection = ray.origin + t * ray.direction;
            if (textured && t < rayLength) //intersection points (nearly) infinitely far cause some problems
            {
                Vector3 u = new Vector3(normal.Y, normal.Z, -normal.X);
                Vector3 v = Cross(u, normal);
                Vector2 p = new Vector2((Math.Abs(Dot(intersection, u) +50) * 100) % (map.width), Math.Abs((Dot(intersection, v) +50) * 100) % (map.height));
                color = texture[(int)p.X, (int)p.Y];
            }
            else //chessboard texture
            {
                Vector3 u = new Vector3(normal.Y, normal.Z, -normal.X);
                Vector3 v = Cross(u, normal);
                Vector2 p = new Vector2(((Dot(intersection, u) +100) * 0.75f) % (2), ((Dot(intersection, v) + 100) * 0.75f) % (2));
                int x = (int)(p.X + 100) % 2;
                int z = (int)(p.Y + 100) % 2;
                if (x == z)
                    x = 1;
                else
                    x = 0;
                color = new Vector3(x ,x,x);
            }
            return new Intersection(intersection, Length(intersection), this);
        }
        public override Vector3 Normal(Vector3 intersection, Vector3 rayDir)
        {
            return normal;
        }
    }
}
