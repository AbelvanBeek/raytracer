﻿using OpenTK;
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
            AddPlane(new Vector3(0, -2f, 0), new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 1f), 0.2f, 0f, "../../assets/wood.png"); //floor
            AddPlane(new Vector3(0, 7f, 0), new Vector3(0, 0, 0), new Vector3(0, -1, 0), new Vector3(1, 1, 1f), 0.2f, 0f, null); //ceiling
            AddPlane(new Vector3(0, 0f, 10f), new Vector3(0, 0, 0), new Vector3(0, 0, -1), new Vector3(1, 1, 1), 0f, 0f, null); //back wall
            AddPlane(new Vector3(0, 0f, -.5f), new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 1, 1), 0f, 0f, null); //behind wall
            AddPlane(new Vector3(-7, 0f, 0f), new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 1, 1), 0f, 0f, null); // left wall
            AddPlane(new Vector3(7, 0f, 0f), new Vector3(0, 0, 0), new Vector3(-1, 0, 0), new Vector3(1, 1, 1), 0f, 0f, null); // right wall

            AddSphere(1.5f, new Vector3(-3.5f, 0f, 8f), new Vector3(1, 0, 0), 0f, 1f);
            AddSphere(1.5f, new Vector3(0, 0f, 8f), new Vector3(0, 1, 0), 1f, 0f);
            AddSphere(1.5f, new Vector3(3.5f, 0f, 8f), new Vector3(0, 0, 1), 0.2f, 0f);
            AddGlassSphere(1f, new Vector3(1.75f, 0, 6), new Vector3(1, 1, 1), 0f, 0f);

            AddLight(new Vector3(0, 5, 3f), new Vector3(20, 20, 20));
        }
        void AddPlane(Vector3 position, Vector3 distance, Vector3 normal, Vector3 color, float reflectiveness, float gloss, string texFile)
        {
            Plane plane = new Plane(normal, distance, position, color, reflectiveness, gloss, texFile);
            primitives.Add(plane);
        }
        void AddSphere(float radius, Vector3 position, Vector3 color, float reflectiveness, float gloss)
        {
            Sphere sphere = new Sphere(radius, position, color, reflectiveness, gloss, null);
            primitives.Add(sphere);
        }
        void AddGlassSphere(float radius, Vector3 position, Vector3 color, float reflectiveness, float gloss)
        {
            Sphere sphere = new Sphere(radius, position, color, reflectiveness, gloss, null);
            sphere.glass = true;
            primitives.Add(sphere);
        }

        void AddLight(Vector3 origin, Vector3 intensity)
        {
            Light light = new Light(origin, intensity);
            lightSources.Add(light);
        }
    }
}
 