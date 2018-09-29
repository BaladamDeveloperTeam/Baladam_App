using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UPersian.Components;
using UPersian.Utils;

public class Category_Click_Handler : MonoBehaviour
{

    public Image[] UnderCategory;
    public GameObject[] SubCategoryItems;
    public GameObject[] CategoryPanels;
    
    public bool Is3D = false; 

    public Color UnSelectedColor = new Color(245, 245, 245);
    public Color SelectedColor = new Color(103, 58, 183);

    public static int SelectedItem = 1;

	void Start ()
    {
        click(0);
	}

    public void click(int Item)
    {
        switch (Item)
        {
            case 0:   
                for(int i = 0; i < UnderCategory.Length; i++)
                {
                    if(Item == i)
                    {
                        UnderCategory[i].color = SelectedColor;
                    }
                    else
                    {
                        UnderCategory[i].color = UnSelectedColor;
                    }
                }

                /////////////////////////////////////

                if (Is3D)
                {
                    for(int i = 0; i < SubCategoryItems.Length; i++)
                    {
                        if (i == Item)
                        {
                            SubCategoryItems[i].gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 134);
                        }
                        else
                        {
                            SubCategoryItems[i].gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 95);
                        }
                    }
                }

                //////////////////////////////////////

                for(int i = 0; i < CategoryPanels.Length; i++)
                {
                    if(i == Item)
                    {
                        CategoryPanels[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        CategoryPanels[i].gameObject.SetActive(false);
                    }
                }
                SelectedItem = 1;
                break;
            case 1:
                for (int i = 0; i < UnderCategory.Length; i++)
                {
                    if (Item == i)
                    {
                        UnderCategory[i].color = SelectedColor;
                    }
                    else
                    {
                        UnderCategory[i].color = UnSelectedColor;
                    }
                }

                /////////////////////////////////////

                if (Is3D)
                {
                    for (int i = 0; i < SubCategoryItems.Length; i++)
                    {
                        if (i == Item)
                        {
                            SubCategoryItems[i].gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 134);
                        }
                        else
                        {
                            SubCategoryItems[i].gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 95);
                        }
                    }
                }

                //////////////////////////////////////

                for (int i = 0; i < CategoryPanels.Length; i++)
                {
                    if (i == Item)
                    {
                        CategoryPanels[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        CategoryPanels[i].gameObject.SetActive(false);
                    }
                }
                SelectedItem = 2;
                break;
            case 2:
                for (int i = 0; i < UnderCategory.Length; i++)
                {
                    if (Item == i)
                    {
                        UnderCategory[i].color = SelectedColor;
                    }
                    else
                    {
                        UnderCategory[i].color = UnSelectedColor;
                    }
                }

                /////////////////////////////////////

                if (Is3D)
                {
                    for (int i = 0; i < SubCategoryItems.Length; i++)
                    {
                        if (i == Item)
                        {
                            SubCategoryItems[i].gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 134);
                        }
                        else
                        {
                            SubCategoryItems[i].gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 95);
                        }
                    }
                }

                //////////////////////////////////////

                for (int i = 0; i < CategoryPanels.Length; i++)
                {
                    if (i == Item)
                    {
                        CategoryPanels[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        CategoryPanels[i].gameObject.SetActive(false);
                    }
                }
                SelectedItem = 3;
                break;
            case 3:
                for (int i = 0; i < UnderCategory.Length; i++)
                {
                    if (Item == i)
                    {
                        UnderCategory[i].color = SelectedColor;
                    }
                    else
                    {
                        UnderCategory[i].color = UnSelectedColor;
                    }
                }

                /////////////////////////////////////

                if (Is3D)
                {
                    for (int i = 0; i < SubCategoryItems.Length; i++)
                    {
                        if (i == Item)
                        {
                            SubCategoryItems[i].gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 134);
                        }
                        else
                        {
                            SubCategoryItems[i].gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 95);
                        }
                    }
                }

                //////////////////////////////////////

                for (int i = 0; i < CategoryPanels.Length; i++)
                {
                    if (i == Item)
                    {
                        CategoryPanels[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        CategoryPanels[i].gameObject.SetActive(false);
                    }
                }
                SelectedItem = 4;
                break;
            case 4:
                for (int i = 0; i < UnderCategory.Length; i++)
                {
                    if (Item == i)
                    {
                        UnderCategory[i].color = SelectedColor;
                    }
                    else
                    {
                        UnderCategory[i].color = UnSelectedColor;
                    }
                }

                /////////////////////////////////////

                if (Is3D)
                {
                    for (int i = 0; i < SubCategoryItems.Length; i++)
                    {
                        if (i == Item)
                        {
                            SubCategoryItems[i].gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 134);
                        }
                        else
                        {
                            SubCategoryItems[i].gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 95);
                        }
                    }
                }

                //////////////////////////////////////

                for (int i = 0; i < CategoryPanels.Length; i++)
                {
                    if (i == Item)
                    {
                        CategoryPanels[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        CategoryPanels[i].gameObject.SetActive(false);
                    }
                }
                SelectedItem = 5;
                break;
            case 5:
                for (int i = 0; i < UnderCategory.Length; i++)
                {
                    if (Item == i)
                    {
                        UnderCategory[i].color = SelectedColor;
                    }
                    else
                    {
                        UnderCategory[i].color = UnSelectedColor;
                    }
                }

                /////////////////////////////////////
                if (Is3D)
                {
                    for (int i = 0; i < SubCategoryItems.Length; i++)
                    {
                        if (i == Item)
                        {
                            SubCategoryItems[i].gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 134);
                        }
                        else
                        {
                            SubCategoryItems[i].gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 95);
                        }
                    }
                }

                //////////////////////////////////////

                for (int i = 0; i < CategoryPanels.Length; i++)
                {
                    if (i == Item)
                    {
                        CategoryPanels[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        CategoryPanels[i].gameObject.SetActive(false);
                    }
                }
                SelectedItem = 6;
                break;
            case 6:
                for (int i = 0; i < UnderCategory.Length; i++)
                {
                    if (Item == i)
                    {
                        UnderCategory[i].color = SelectedColor;
                    }
                    else
                    {
                        UnderCategory[i].color = UnSelectedColor;
                    }
                }

                /////////////////////////////////////

                if (Is3D)
                {
                    for (int i = 0; i < SubCategoryItems.Length; i++)
                    {
                        if (i == Item)
                        {
                            SubCategoryItems[i].gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 134);
                        }
                        else
                        {
                            SubCategoryItems[i].gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 95);
                        }
                    }
                }

                //////////////////////////////////////

                for (int i = 0; i < CategoryPanels.Length; i++)
                {
                    if (i == Item)
                    {
                        CategoryPanels[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        CategoryPanels[i].gameObject.SetActive(false);
                    }
                }
                SelectedItem = 7;
                break;
            case 7:
                for (int i = 0; i < UnderCategory.Length; i++)
                {
                    if (Item == i)
                    {
                        UnderCategory[i].color = SelectedColor;
                    }
                    else
                    {
                        UnderCategory[i].color = UnSelectedColor;
                    }
                }

                /////////////////////////////////////

                if (Is3D)
                {
                    for (int i = 0; i < SubCategoryItems.Length; i++)
                    {
                        if (i == Item)
                        {
                            SubCategoryItems[i].gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 134);
                        }
                        else
                        {
                            SubCategoryItems[i].gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 95);
                        }
                    }
                }

                //////////////////////////////////////

                for (int i = 0; i < CategoryPanels.Length; i++)
                {
                    if (i == Item)
                    {
                        CategoryPanels[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        CategoryPanels[i].gameObject.SetActive(false);
                    }
                }
                SelectedItem = 8;
                break;
        }
    }


}
