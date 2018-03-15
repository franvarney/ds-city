using UnityEngine;
[ExecuteInEditMode]
[ImageEffectAllowedInSceneView]
[AddComponentMenu("Effects/Zololgo/Sharpen")]
[RequireComponent(typeof(Camera))]
public class Sharpen : MonoBehaviour {
	[Range(0, 2)]
	public float sharpness = 1.0f;
	Material mat;
	bool isSupported;
	void OnRenderImage(RenderTexture src, RenderTexture dest) {
		if (!mat) mat = new Material(Shader.Find("Hidden/Zololgo/Post/Sharpen"));
		mat.SetFloat("_Sharpness", sharpness);
		Graphics.Blit(src, dest, mat, 0);
	}
}

