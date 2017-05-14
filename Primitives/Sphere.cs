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

        public Sphere(float radius, Vector3 position, Vector3 color) : base(position, color)
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
    }
}
