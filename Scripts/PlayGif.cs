using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGif : MonoBehaviour
{

    public Sprite[] SpriteImages;
    public Image ShowPlace;

	void Start ()
    {
		
	}
	

	void Update ()
    {
        ShowPlace.sprite = SpriteImages[(int)(Time.time * 10) % SpriteImages.Length];
	}
}
