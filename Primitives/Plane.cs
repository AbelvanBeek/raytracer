using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Constants;
namespace template
{
    class Plane : Primitive
    {
        Vector3 normal;
        Vector3 distance;
        public Plane(Vector3 normal, Vector3 distance, Vector3 position, Vector3 color, float reflectiveness) : base(position, color, reflectiveness)
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
            Vector3 u = new Vector3(1, 0, 0);
            Vector3 v = new Vector3(0, 0, 1);
            Vector3 p = new Vector3(intersection.X / u.X, intersection.Y, intersection.Z / v.Z);
            int x = (int)Math.Abs(p.X + 100) % 2;
            int z = (int) (p.Z + 100) % 2;
            if (x == z)
                x = 1;
            else x = 0;
            color = new Vector3(x, x, x);

            if (intersection.Z < -1)
                return null;
            return new Intersection(intersection, Length(intersection), this);
        }
        public override Vector3 Normal(Vector3 intersection)
        {
            return normal;
        }
    }
}
