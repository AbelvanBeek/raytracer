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
        public Plane(Vector3 normal, Vector3 distance, Vector3 position, Vector3 color, float reflectiveness) :base(position, color, reflectiveness)
        {
            this.normal = normal;
            this.distance = distance;
        }
        public override Intersection CalculateIntersection(Ray ray)
        {
            //float d = Dot(normal, position);
            //float t = -(Dot(normal, ray.origin) + d) / (Dot(normal, ray.direction));

            float t = (ray.origin.Y - position.Y) / ray.direction.Y;

            if (t > 0) return null;
            Vector3 intersection = ray.origin + t * ray.direction;
            return new Intersection(intersection, Length(intersection), this);
        }
        public override Vector3 Normal(Vector3 intersection)
        {
            return normal;
        }
    }
}
