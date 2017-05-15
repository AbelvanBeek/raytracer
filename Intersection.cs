using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template
{
    public class Intersection
    {
        public Vector3 intersection;
        public float distance;
        public Primitive nearestPrimitive;
        public Intersection(Vector3 intersection, float distance,  Primitive nearestPrimitive)
        {
            this.distance = distance;
            this.intersection = intersection;
            this.nearestPrimitive = nearestPrimitive;
        }

    }
}
