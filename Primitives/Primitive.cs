using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Constants;

namespace template
{
    public abstract class Primitive
    {
        public Vector3 position;
        public Vector3 color;
        public float reflectiveness;

        public Primitive(Vector3 position, Vector3 color, float reflectiveness)
        {
            this.position = position;
            this.color = color;
            this.reflectiveness = reflectiveness;
        }

        public virtual void DrawPrimitive() { }
        public virtual Intersection CalculateIntersection(Ray ray) { return null; }

        public virtual Vector3 Normal(Vector3 intersection)
        {
            return new Vector3(0, 0, 0);
        }
    }
}
