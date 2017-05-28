using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Constants;

public class Ray
{
    public Vector3 origin;
    public Vector3 direction;
    public float distance = rayLength;
    public int recursionDepth;
    public Ray(Vector3 origin, Vector3 direction)
    {
        this.origin = origin;
        this.direction = direction;
        recursionDepth = recursionCount;
    }

}
