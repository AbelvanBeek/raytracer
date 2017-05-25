using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Constants;

namespace template
{
    class Sphere : Primitive
    {
        float radius;
        float steps = 100f;

        public Sphere(float radius, Vector3 position, Vector3 color, float reflectiveness) : base(position, color, reflectiveness)
        {
            this.radius = radius;
        }

        public override void DrawPrimitive()
        {
            float angle = 2 / steps * (float)Math.PI;
            
            for (int i = 0; i < steps; i++)
            {
                display.Line(Tx(CalculateX(angle * i)), Ty(CalculateY(angle * i)), Tx(CalculateX(angle * (i + 1))), Ty(CalculateY(angle * (i + 1))), CalculateHex(color));
            }
        }

        public float CalculateX(float angle)
        {
            return radius * (float)Math.Cos(angle) + position.X;
        }

        public float CalculateY(float angle)
        {
            return radius * (float)Math.Sin(angle) + position.Z;
        }
     
        public override Intersection CalculateIntersection(Ray ray)
        {
            float rSq = radius * radius;
            Vector3 c = this.position - ray.origin;
            float t = Dot(c, ray.direction);
            Vector3 q = c - t* ray.direction;
            float p = Dot(q, q);
            if (p > rSq) return null;
            t -= (float)Math.Sqrt(rSq - p);
            if ((t < ray.distance) && (t > 0))
                ray.distance = t;
            return new Intersection(ray.origin + ray.direction*ray.distance, t , this);
        }
        public override Vector3 Normal(Vector3 intersection)
        {
            Vector3 n = (intersection - position);
            n.Normalize();
            return n;
        }

    }
}
