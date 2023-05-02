using UnityEngine;
using UnityEngine.Rendering;

public class BlurEffect : MonoBehaviour
{
    private float blurSize = 1.0f;
    private float velocityThreshold = 10.0f;
    private Material blurMaterial;
    private Camera mainCamera;

    private void Start()
    {
        blurMaterial = new Material(Shader.Find("Custom/Blur"));
        mainCamera = Camera.main;
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (mainCamera == null || blurMaterial == null)
        {
            Graphics.Blit(source, destination);
            return;
        }

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null || player.GetComponent<Rigidbody2D>() == null)
        {
            Graphics.Blit(source, destination);
            return;
        }

        var playerVelocity = player.GetComponent<Rigidbody2D>().velocity.magnitude;
        if (playerVelocity < velocityThreshold)
        {
            Graphics.Blit(source, destination);
            return;
        }

        blurMaterial.SetFloat("_BlurSize", blurSize);

        var temporaryTexture = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
        Graphics.Blit(source, temporaryTexture, blurMaterial, 0);
        Graphics.Blit(temporaryTexture, destination);
        RenderTexture.ReleaseTemporary(temporaryTexture);
    }
}
