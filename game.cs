using System;
using System.IO;
using template;

namespace Template
{
    class Game
    {

	    //Member Variables_ Place member variables here
	    public Surface screen;
        public Raytracer rayTracer;

	    //Init_ Initializes the program
	    public void Init()
	    {
            rayTracer = new Raytracer(screen);
	    }

	    //Tick_ Renders one frame
	    public void Tick()
	    {
            screen.Line(513, 0, 513, 513, 0xffffff);
            rayTracer.Render();
        }
    }

}