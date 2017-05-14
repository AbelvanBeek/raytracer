using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template
{
    public class Light
    {
        Vector3 origin;
        Vector3 intensity;
        public Light()
        {
            intensity = new Vector3(1f, 1f, 1f);
        }
    }
}
