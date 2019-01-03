using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{

    public Image img;

    void Start()
    {
        //StartCoroutine(FadeImage(false));
        StartCoroutine(Example());
    }

    void Update()
    {

    }

    IEnumerator Example()
    {
        yield return new WaitForSeconds(1);
        yield return SceneManager.LoadSceneAsync(1);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
}
