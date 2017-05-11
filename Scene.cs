using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template
{
    public class Scene
    {
        List<Primitive> primitves = new List<Primitive>();
        List<Light> lightSources = new List<Light>();

        public Scene()
        {
        }
        void AddPlane()
        {
            Vector3 position = new Vector3(0, 0, 0);
            Vector3 distance = new Vector3(10, 0, 10);
            Vector3 normal = new Vector3(1, 0, 1);
            Plane plane = new Plane(normal, distance, position);
        }
        void AddSphere()
        {
            Vector3 position = new Vector3(0, 0, 0);
            float radius = 1f;
            Sphere sphere = new Sphere(radius, position);
        }
    }
}
 