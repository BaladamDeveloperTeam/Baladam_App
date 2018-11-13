using Kakera;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageClass : MonoBehaviour
{

    
    public Unimgpicker imagePicker;
    private Sprite FinalImage;
    private string ImagePath;
    public Image image;

    public void Awake()
    {
        imagePicker.Completed += (string path) =>
            {
                StartCoroutine(LoadImage(path, FinalImage));
            };
    }

    private IEnumerator LoadImage(string path, Sprite output)
    {
        ImagePath = path;
        var url = "file://" + path;
        var www = new WWW(url);
        yield return www;

        var texture = www.texture;
        if (texture == null)
        {
            Debug.LogError("Failed to load texture url:" + url);
        }

        FinalImage = SpriteFromTex2D(texture);
        image.sprite = SpriteFromTex2D(texture);
        var tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;
        ImagePath = path;
    }

    public void OnPressShowPicker(string name)
    {
        imagePicker.Show("Select Image", name, 1024);
    }

    static Sprite SpriteFromTex2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    public Sprite GetSprite()
    {
        return FinalImage;
    }

    public string GetPath()
    {
        return ImagePath;
    }

    public void MyScreenshot(string filename)
    {
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        Texture2D newScreenshot = ScaleTexture(screenshot, 1024, 576);

        byte[] bytes = newScreenshot.EncodeToPNG();
        File.WriteAllBytes(filename, bytes);
    }

    private Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);
        Color[] rpixels = result.GetPixels(0);
        float incX = ((float)1 / source.width) * ((float)source.width / targetWidth);
        float incY = ((float)1 / source.height) * ((float)source.height / targetHeight);
        for (int px = 0; px < rpixels.Length; px++)
        {
            rpixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth), incY * ((float)Mathf.Floor(px / targetWidth)));
        }
        result.SetPixels(rpixels, 0);
        result.Apply();
        return result;
    }
}
