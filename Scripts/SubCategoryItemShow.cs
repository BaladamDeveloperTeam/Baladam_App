using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubCategoryItemShow : MonoBehaviour
{

    public Sprite[] SubCategoryImage;
    public string[] SubCategoryText;
    private GameObject[] SubCategoryItems;
    public Image SubCategoryImageView;
    public Text SubCategoryLabel;
    public int target;

	void Start ()
    {
        SubCategoryItems = GameObject.FindGameObjectsWithTag("SubCategoryItems_image_text");
        for(int i = 0; i < SubCategoryItems.Length; i++)
        {
            SubCategoryLabel = SubCategoryItems[i].gameObject.GetComponentInChildren<Text>();
            SubCategoryImageView = SubCategoryItems[i].gameObject.GetComponentInChildren<Image>();
            target = MyRandom();
            SubCategoryLabel.text = SelectText(target);
            SubCategoryImageView.sprite = SelectImage(target);
        }
	}
	

	void Update ()
    {
		
	}

    private int MyRandom()
    {
        //System.Random rnd = new System.Random();
        //int Number = rnd.Next(3);
        int Number = Random.Range(0, 3);
        return Number;
    }

    private Sprite SelectImage(int Item)
    {
        return SubCategoryImage[Item];
    }

    private string SelectText(int Item)
    {
        return SubCategoryText[Item];
    }
}
