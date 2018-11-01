using Kakera;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectImage : MonoBehaviour
{

    [SerializeField]
    private Unimgpicker imagePicker;
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
}
