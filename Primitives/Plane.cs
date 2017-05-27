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
            Vector3 intersection;
            float t = (Dot(position - ray.origin, normal) / Dot(ray.direction, normal));
            
            if(t < 0)
                return null;
            intersection = ray.origin + t * ray.direction;
            //Z lijkt omgedraaid te zijn vor planes, zonder deze code zie je de plane wel, met zie je hem niet. (z is dus kleiner dan -1
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
