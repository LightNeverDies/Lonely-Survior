using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Button button;
    Text uiText;
    void Start()
    {
        uiText = button.GetComponentInChildren<Text>();
    }

    public void MouseEnterUI()
    {
        Debug.Log("Entry");
        //Button is changing his size when mouse is inside it.
        // button.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 400);
        uiText.color = Color.red;
    }
    public void MouseExitUI()
    {
        Debug.Log("Exit");
        // Button is going to his default settings from Inspector Menu.
        // button.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 320);
        uiText.color = Color.white;
    }
}
