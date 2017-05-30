TEAM MEMBERS
__________________________________
Abel van Beek	| Wouter Castelein
5978831		| 5936608
__________________________________

Camera controls
__________________________________

Move up
| Up-arrow
Move down
| Down-arrow
Move left
| Left-arrow
Move right
| Right-arrow
Move forward
| Z-key
Move backwards
| X-key
Pan up
| W-key
Pan down
| S-key
Pan left
| A-key
Pan right
| D-key
Narrow field of view
| C-key
Broaden fiel of view
| V-key
Photograph Mode on
| P-key
Photograph Mode off
| O-key
__________________________________

BONUS ASSINGMENTS
__________________________________

Glossy reflections 
| For every intersection with a glossy object, ten random rays are casted for every lightsource.
| Using the spherical coordinate system and two random values between 0 and 1, the direction vectors
| of the random vectors are calculated. Then the shadow rays of the random generated rays are cast and
| their color is multiplied by the intensity value of the intersected object. Eventually the average
| color is calculated and returned for the pixel color.
|
| Location
| Raytracer.cs --> GlossIllumination()

Anti-aliasing
| For every ray that is being cast, 8 more rays are casted that are very close to the original ray. 
| Each of these rays are traced and their colors are averaged into a variable and eventually the color
| of the original ray is also added to the average color. This completely averaged color is then returned
| and the pixel takes the color of this value.
|
| Location
| Raytracer.cs --> AntiAliasing()

Textures
| Through a .png file in the directory, a texture is given to a plane. This .png file is converted
| into a two dimensional array which contains the colors of the image at the given position. If a texture is 
| applied, the color of the plane at an intersection x/y/z will become the color value from the
| texture x/y. For the checkerboard texture we used a calculation rather that a full-blown texture.
| Sadly texture mapping is not working for spheres.
| 
| Location
| Primitive.cs --> Primitive()
| Plane.cs --> CalculateIntersection()

Refraction/absorption
| 
| 
| 
| 

Acceleration structure
| We tried implementing multi-threading by starting a new thread for casting the rays. However, 
| it seems like some variables overlap in and outside the thread. This causes the screen to 
| flicker. This is why we chose to leave the amount of threads at 1. Increasing the amount of threads
| does increase performance, but at the cost of image quality.
|
| Location
| Raytracer.cs --> CastRays()
__________________________________

MATERIALS
__________________________________

Team Codermind tutorial
| As a general guideline for specific subjects we used the extensive tutorial on raytracing by the 
| Codermind Team. For example, we used this tutorial to see how the Fresnel calculated had to be done.

INFOGR slides
| We have used the slides for the intersect scene code of the spheres. On top of that, we turned to
| the slides for basic geometry equations and reflection calculations.  

Stackoverflow --> Glossy reflection in ray tracing
| Since our own glossy reflection code did not seem to work, we started looking around on the internet.
| Having tried numerous examples we ended up using one from stackoverflow. However, the glossy effect
| is still far from working properly. 