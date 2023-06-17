using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CaptureTrailRenderer : MonoBehaviour
{
    public RenderTexture renderTexture; // Asigna la Render Texture desde el Inspector o busca una en la escena

    private void Update()
    {
        // Captura el TrailRenderer como una imagen en el momento que desees
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
            RenderTexture.active = renderTexture;
            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture.Apply();
            byte[] pngData = texture.EncodeToPNG();
            File.WriteAllBytes("Ruta/Para/Guardar/Imagen.png", pngData);
            RenderTexture.active = null;
        }
    }
}
