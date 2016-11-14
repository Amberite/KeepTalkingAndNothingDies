using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {
    public GameObject HelpWindow;
    public bool helpWindowOpen;

    void Start ()
    {
        helpWindowOpen = false;
    }

    void Update ()
    {
        // if helpwindow is open
        if (helpWindowOpen == true)
        {
            if (Input.anyKeyDown) {
                // play fade out animation
                HelpWindow.GetComponent<Animation>().Play("FadeOut");
                HelpWindow.GetComponent<CanvasGroup>().interactable = false;
                HelpWindow.GetComponent<CanvasGroup>().blocksRaycasts = false;
                helpWindowOpen = false;

            }
        }
    }

    public void HelpButtonPress() {
        Debug.Log("Help Button Pressed!");
        HelpWindow.GetComponent<Animation>().Play("FadeIn");
        HelpWindow.GetComponent<CanvasGroup>().interactable = true;
        HelpWindow.GetComponent<CanvasGroup>().blocksRaycasts = true;
        helpWindowOpen = true;
    }

    public void StartButtonPress()
    {
       Debug.Log("Start Button Pressed! Put Load Level Here in menu script");

    }


}
