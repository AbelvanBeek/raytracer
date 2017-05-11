using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template
{
    public abstract class Primitive
    {
        public Vector3 position;
            public Primitive(Vector3 position)
        {
            this.position = position;
        }
    }
}
