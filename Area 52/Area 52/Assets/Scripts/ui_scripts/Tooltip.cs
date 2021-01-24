using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is for the tooltip.
//Call it anywhere with functions ShowTooltip_Static() and HideTooltip_Static()
public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;

    [SerializeField]
    private Camera uiCamera;

    private Text toolTipText;
    private RectTransform backgroundRectTransform;
    private void Awake()
    {
        instance = this;
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        toolTipText = transform.Find("text").GetComponent<Text>();
    }

    private void Update()
    {
        //Transforming the mouse position into local point to which then we move the object
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = localPoint;
    }

    private void ShowTooltip(string tooltipString)
    {
        //showing the tooltip and ajusting the size acording to the text inside
        toolTipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(toolTipText.preferredWidth + textPaddingSize*2, toolTipText.preferredHeight + textPaddingSize * 2);
        backgroundRectTransform.sizeDelta = backgroundSize;
        gameObject.SetActive(true);
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }
    //this two functions are static and can be called from anywhere
    //I use a static referance that I initialize in the Awake() method
    public static void ShowTooltip_Static(string tooltipString)
    {
        instance.ShowTooltip(tooltipString);
    }

    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }
}
