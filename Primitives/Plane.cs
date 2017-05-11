using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template
{
    class Plane : Primitive
    {
        //Not sure how to define a plane
        Vector3 normal;
        Vector3 distance;
        public Plane(Vector3 normal, Vector3 distance, Vector3 position) :base(position)
        {
            this.normal = normal;
            this.distance = distance;
        }
    }
}
