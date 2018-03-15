Sharpen 
Post processing camera effect.

Usage:

- you can find this effect in the Components/Effects/Zololgo menu
- once it's applied, you can control the effect with the sharpness slider. Best practice is to use values between 1-2.

After applied to your camera, the rendered image will have a more crisp, sharpen look. 
With the right amount of sharpness, you can dramatically improve the graphics quality of your game. 
Please note, high values introduce halos in the edges, also enhance texture noise, and may make graduated color areas blocky.

Features:

- simple script and shader code
- one parameter for sharpness
- works on both Forward and Deferred rendering
- compatible with other post effects*
- preview in Editor SceneView
- low hardware requirements (SM2.0)
- not using RenderTextures
- sample scene included

*Please make sure that Sharpen component is come after any Anti-aliasing post effect on your camera.