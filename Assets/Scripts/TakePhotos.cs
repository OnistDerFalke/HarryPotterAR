using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TakePhotos : MonoBehaviour
{

    [SerializeField] private Camera camera;
    
    public void TakePhoto()
    {
        StartCoroutine(TakeAPhoto());
    }

    IEnumerator TakeAPhoto()
    {
        
        yield return new WaitForEndOfFrame();
        
        var width = Screen.width;
        var height = Screen.height;
        
        
        var rt = new RenderTexture(width, height, 24);
        camera.targetTexture = rt;
        
        var currentRT = RenderTexture.active;
        RenderTexture.active = rt;
        
        camera.Render();
        
        var image = new Texture2D(width, height);
        image.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        image.Apply();
        
        camera.targetTexture = null;
        RenderTexture.active = currentRT;

       
        var bytes = image.EncodeToPNG();
        var fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        var filePath = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllBytes(filePath, bytes);
        GameManager.DebugLog($"Photo has been save to: {Application.persistentDataPath}.");
        Destroy(rt);
        Destroy(image);
    }
}
