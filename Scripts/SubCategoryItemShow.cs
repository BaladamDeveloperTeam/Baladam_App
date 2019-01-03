using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;

public class SubCategoryItemShow : MonoBehaviour
{

    public Sprite[] Images;
    private string[] SubCategoryText = { "طراحي و گرافيک", "برنامه نويسي و تکنولوژي", "تجرت و کسب کار", "موسيقي و صدا"};
    private GameObject[] SubCategoryItems;
    public Image SubCategoryImageView;
    public RtlText SubCategoryLabel;
    public int target;

	void Start ()
    {
        SubCategoryItems = GameObject.FindGameObjectsWithTag("SubCategoryItems_image_text");
        for(int i = 0; i < SubCategoryItems.Length; i++)
        {
            SubCategoryLabel = SubCategoryItems[i].gameObject.GetComponentInChildren<RtlText>();
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
        return Images[Item];
    }

    private string SelectText(int Item)
    {
        return SubCategoryText[Item];
    }
}
