using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template
{
    class Light
    {
        Vector3 Origin;
        Vector3 intensity;
        public Light()
        {
            intensity = new Vector3(1f, 1f, 1f);
        }
    }
}
