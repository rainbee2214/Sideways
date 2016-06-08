using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessagePanel : MonoBehaviour
{
    RectTransform rt;
    public Image backgroundImage;
    public Text text;
    public Color CurrentColor
    {
        get { return backgroundImage.color; }
        set { backgroundImage.color = value; }
    }
    public Color CurrentTextColor
    {
        get { return text.color; }
        set { text.color = value; }
    }
    public string CurrentMessage
    {
        get { return text.text; }
        set { text.text = value; }
    }

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        backgroundImage = GetComponent<Image>();
    }
    void Start()
    {
        text = GetComponentInChildren<Text>(); //If multiple text children objects use GetComponentsInChildren<Text>()[n]
        gameObject.SetActive(false);
    }

    public void SetMessage(string message)
    {
        text.text = message;
    }

    #region Helper Functions
    public bool NeedToResize(Vector2 startPosition, Vector2 endPosition)
    {
        return (
            rt.anchorMin != startPosition ||
            rt.anchorMax != endPosition
            );
    }

    public void Resize(Vector2 startPosition, Vector2 endPosition)
    {
        rt.anchorMin = new Vector2(startPosition.x, startPosition.y);
        rt.anchorMax = new Vector2(endPosition.x, endPosition.y);
    }
    #endregion
}
