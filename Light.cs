using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Light
{
    public Vector3 origin;
    public Vector3 intensity;
    public Light(Vector3 origin, Vector3 intensity)
    {
        this.origin = origin;
        this.intensity = intensity;
    }
}
