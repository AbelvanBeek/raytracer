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
        public List<Primitive> primitives = new List<Primitive>();
        public List<Light> lightSources = new List<Light>();

        public Scene()
        {
                AddPlane(new Vector3(0, -2f, 0), new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 0, 1), 0f);
                AddPlane(new Vector3(0, 0f, 10f), new Vector3(0, 0, 0), new Vector3(0, 0, -1), new Vector3(1, 1, 1), 0f);
                AddSphere(1.5f, new Vector3(-3.5f, 0f, 7.5f), new Vector3(1, 0, 0), 0f);
                AddSphere(1.5f, new Vector3(0, 0f, 7.5f), new Vector3(0, 1, 0), 1f);
                AddSphere(1.5f, new Vector3(3.5f, 0f, 7.5f), new Vector3(0, 0, 1), 0f);
                //afhankelijk van waar je het licht plaats wordt de hele plane opeens zwart.
                AddLight(new Vector3(0, 5f, 0f), new Vector3(65,65,65));
        }
        void AddPlane(Vector3 position, Vector3 distance, Vector3 normal, Vector3 color, float reflectiveness)
        {
            Plane plane = new Plane(normal, distance, position, color, reflectiveness);
            primitives.Add(plane);
        }
        void AddSphere(float radius, Vector3 position, Vector3 color, float reflectiveness)
        {
            Sphere sphere = new Sphere(radius, position, color, reflectiveness);
            primitives.Add(sphere);
        }
        void AddLight(Vector3 origin, Vector3 intensity)
        {
            Light light = new Light(origin, intensity);
            lightSources.Add(light);
        }
    }
}
 