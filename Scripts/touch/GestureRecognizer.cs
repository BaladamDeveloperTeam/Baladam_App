using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureRecognizer : MonoBehaviour
{

    public float minSwipeDistY;

    public float minSwipeDistX;

    private Vector2 startPos;

    int LeftCount = 0;
    int RightCount = 0;
    public Animator Anims;

    void Update ()
    {
        if (Input.touchCount > 0)

        {

            Touch touch = Input.touches[0];



            switch (touch.phase)

            {

                case TouchPhase.Began:

                    startPos = touch.position;

                    break;



                case TouchPhase.Ended:

                    float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;

                    if (swipeDistVertical > minSwipeDistY)

                    {

                        float swipeValue = Mathf.Sign(touch.position.y - startPos.y);

                        if (swipeValue > 0)//up swipe
                        { }
                        //Jump ();

                        else if (swipeValue < 0)//down swipe
                        { }
                        //Shrink ();

                    }

                    float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;

                    if (swipeDistHorizontal > minSwipeDistX)

                    {

                        float swipeValue = Mathf.Sign(touch.position.x - startPos.x);

                        if (swipeValue > 0)//right swipe
                            RightCount++;
                        //MoveRight ();

                        else if (swipeValue < 0)//left swipe
                            LeftCount++;
                        //MoveLeft ();

                    }
                    break;
            }
        }
    }

    public void GoNext()
    {
        if (LeftCount == 0)
            Anims.SetTrigger("Page1");
        else if (LeftCount == 1)
            Anims.SetTrigger("Page2");
        LeftCount++;
    }
}
