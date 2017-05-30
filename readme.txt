TEAM MEMBERS
__________________________________
Abel van Beek	| Wouter Castelein
5978831		| 5936608
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
| We implemented textures for planes, using two perpendicular vectors on the plane we determine x and y coordinates
| depending on the location of the intersection and use these coordinates to get the right color from a 
| specified texture.
| If no colors are specified the plane will get the default chessboard coloring.

Refraction
|We added refraction in glass spheres. The refraction is calculated using a specific refraction index (1.52 by default)
|Using fresnel calculations we make sure that the glass sphere partially reflects a ray and partially transmits it.


Acceleration structure
|
|
|
|

__________________________________

MATERIALS
__________________________________

Team Codermind tutorial
| As a general guideline for specific subjects we used the extensive tutorial on raytracing by the 
| Codermind Team. For example, we used this tutorial to see how the Fresnel calculated had to be done.

Stackoverflow --> Glossy reflection in ray tracing
|
|
|
|