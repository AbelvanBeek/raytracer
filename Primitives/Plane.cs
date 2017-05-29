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

        public Plane(Vector3 normal, Vector3 distance, Vector3 position, Vector3 color, float reflectiveness, float gloss, string file) : base(position, color, reflectiveness, gloss, file)
        {
            this.normal = normal;
            this.distance = distance;
        }
        public override Intersection CalculateIntersection(Ray ray)
        {
            Vector3 intersection;
            float t = (Dot(position - ray.origin, normal) / Dot(ray.direction, normal));
            if (t < 0)
                return null;
            intersection = ray.origin + t * ray.direction;
            if (t < 10)
            {
                Vector3 u = new Vector3(normal.Y, normal.Z, -normal.X);
                Vector3 v = Cross(u, normal);
                Vector2 p = new Vector2((Math.Abs(Dot(intersection, u) +50) * 100) % (map.width), Math.Abs((Dot(intersection, v) +50) * 100) % (map.height));
                color = texture[(int)p.X, (int)p.Y];
            }
            return new Intersection(intersection, Length(intersection), this);
        }
        public override Vector3 Normal(Vector3 intersection)
        {
            return normal;
        }
    }
}
