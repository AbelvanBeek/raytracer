using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template
{
    class Sphere : Primitive
    {
        float radius;
        public Sphere(float radius, Vector3 position) : base(position)
        {
            this.radius = radius;
        }

    }
}
