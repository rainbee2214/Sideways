//using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public enum DefaultColors
{
    Red,
    Orange,
    Yellow,
    Green,
    Blue,
    Indigo,
    Violet,
    Pink,
    White,
    Black
}
public class MessageController : MonoBehaviour
{

    //[MenuItem("SSS/Create Message Controller")]
    public static void CreateMessageController()
    {
        Debug.Log("Creating message controller. Send a message by calling MessageController.messageController.DispalyMessage(message);");
        GameObject g = new GameObject("MessageController");
        g.AddComponent<MessageController>();
    }

    public static Color32 red = HexToColor("E57373");
    public static Color32 orange = HexToColor("FFB74D");
    public static Color32 yellow = HexToColor("FFF176");
    public static Color32 green = HexToColor("81C784");
    public static Color32 blue = HexToColor("4FC3F7");
    public static Color32 indigo = HexToColor("7986CB");
    public static Color32 violet = HexToColor("BA68C8");
    public static Color32 pink = HexToColor("F06292");
    public static Color32 white = HexToColor("FFFFFF");
    public static Color32 black = HexToColor("000000");

    public static MessageController messageController;

    public float messageDelay = 0.5f;
    [Range(0f, 1f)]
    public float fadeSpeed = 0.5f;
    //x and y positions must be [0,1] : think of them as percentages of screen width and height
    public Vector2 startPosition = new Vector2(0.2f, 0.2f), endPosition = new Vector2(0.8f, 0.4f);

    public MessagePanel messagePanel;
    Canvas canvas;

    [Header("For testing")]
    public bool showMessage = false;
    public string messageToShow = "My message";

    public Color fadedColor;
    public Color startColor;

    [Header("To use a prefab it must be in the Resources Folder with the relative path: Prefabs/MessagePanel")]
    public bool usePrefab = false;
    public DefaultColors defaultMessagePanelColor = DefaultColors.Pink;
    public DefaultColors defaultMessageTextColor = DefaultColors.Black;

    float startDelay, startFadeSpeed;

    public static Color32 HexToColor(string HexVal)
    {
        // Convert each set of 2 digits to its corresponding byte value
        byte R = byte.Parse(HexVal.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte G = byte.Parse(HexVal.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte B = byte.Parse(HexVal.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        return new Color32(R, G, B, 255);
    }

    void Awake()
    {
        if (messageController == null)
        {
            DontDestroyOnLoad(gameObject);
            messageController = this;
        }
        else
            Destroy(gameObject);

        startDelay = messageDelay;
        startFadeSpeed = fadeSpeed;
        LoadMessageWindow();
    }

    void Start()
    {
        startColor = messagePanel.CurrentColor;
        messagePanel.transform.parent.SetParent(transform);
    }

    void Update()
    {
        if (showMessage)
        {
            //delete this once you have code to display a message elsewhere
            showMessage = false;
            DisplayMessage(messageToShow);
        }
    }
    public void SetPosition(Vector2 start, Vector2 end)
    {
        startPosition = start;
        endPosition = end;
        ResizePanel();
    }
    public void DisplayMessage(string message)
    {
        DisplayMessage(message, startDelay, startFadeSpeed);
    }
    public void DisplayMessage(string message, float messageDelay, float fadeSpeed)
    {
        this.messageDelay = messageDelay;
        this.fadeSpeed = fadeSpeed;
        StopCoroutine("DisplayMessageRoutine");
        messagePanel.CurrentMessage = message;
        StartCoroutine("DisplayMessageRoutine");
    }

    //We want to message to appear, and then disappear after a delay 
    //For this, we can use CoRoutines!
    IEnumerator DisplayMessageRoutine()
    {
        //Set the message panel to full color
        messagePanel.CurrentTextColor = Color.black; //reset the messagePanel for next time
        messagePanel.CurrentColor = startColor;

        float lowAlpha = 8 / 255f;

        if (messagePanel.NeedToResize(startPosition, endPosition)) ResizePanel();
        messagePanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(messageDelay);

        float percent = 0;

        ////Fade out instead of just turning off
        while (percent < 1 && messagePanel.CurrentColor.a > lowAlpha)
        {
            messagePanel.CurrentColor = Color.Lerp(messagePanel.CurrentColor, fadedColor, percent);
            messagePanel.CurrentTextColor = Color.Lerp(messagePanel.CurrentTextColor, fadedColor, percent);

            percent += fadeSpeed / 100f;
            yield return null; //**Don't forget this, or Unity will likely crash :)
        }
        messagePanel.gameObject.SetActive(false);
    }

    GameObject CreatePanel()
    {
        GameObject p = new GameObject("DefaultMessagePanel");
        p.AddComponent<RectTransform>();
        p.AddComponent<Image>();

        Color32 panelColor = pink;
        switch (defaultMessagePanelColor)
        {
            case DefaultColors.Red: panelColor = red; break;
            case DefaultColors.Orange: panelColor = orange; break;
            case DefaultColors.Yellow: panelColor = yellow; break;
            case DefaultColors.Green: panelColor = green; break;
            case DefaultColors.Blue: panelColor = blue; break;
            case DefaultColors.Indigo: panelColor = indigo; break;
            case DefaultColors.Violet: panelColor = violet; break;
            case DefaultColors.Pink: panelColor = pink; break;
            case DefaultColors.White: panelColor = white; break;
            case DefaultColors.Black: panelColor = black; break;
        }

        panelColor.a = 200;
        p.GetComponent<Image>().color = panelColor;
        p.AddComponent<MessagePanel>();

        GameObject c = new GameObject("Text");
        c.AddComponent<RectTransform>();
        c.AddComponent<Text>();

        switch (defaultMessageTextColor)
        {
            case DefaultColors.Red: panelColor = red; break;
            case DefaultColors.Orange: panelColor = orange; break;
            case DefaultColors.Yellow: panelColor = yellow; break;
            case DefaultColors.Green: panelColor = green; break;
            case DefaultColors.Blue: panelColor = blue; break;
            case DefaultColors.Indigo: panelColor = indigo; break;
            case DefaultColors.Violet: panelColor = violet; break;
            case DefaultColors.Pink: panelColor = pink; break;
            case DefaultColors.White: panelColor = white; break;
            case DefaultColors.Black: panelColor = black; break;
        }
        Text t = c.GetComponent<Text>();
        t.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        t.text = "This is the default message.";
        t.alignment = TextAnchor.MiddleCenter;
        t.fontSize = 20;
        t.color = panelColor;

        c.transform.SetParent(p.transform, false);
        c.GetComponent<RectTransform>().anchorMin = Vector2.zero;
        c.GetComponent<RectTransform>().anchorMax = Vector2.one;
        c.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        return p;
    }

    #region Helper Functions
    public void LoadMessageWindow()
    {
        canvas = (GameObject.Find("Canvas") != null) ? GameObject.Find("Canvas").GetComponent<Canvas>() : CreateCanvas().GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay; // this is for when we instantiate a new canvas, this doesn't need to happen if the canvas is set in the inspector

        //***The resource you want to load must be inside the Resources folder in the root of your project. The path is relative to that folder
        if (usePrefab) messagePanel = Instantiate(Resources.Load<GameObject>("Prefabs/MessagePanel").GetComponent<MessagePanel>());
        else messagePanel = CreatePanel().GetComponent<MessagePanel>();

        messagePanel.name = "MessagePanel -" + this.name; // this.name will help identify the panels if there are mulitple controllers

        messagePanel.transform.SetParent(canvas.transform, false); //the second parameter controls if the message panel will change position when its parent is set

        messagePanel.transform.SetAsLastSibling(); //to make sure the panel is rendered in front of everything else (last)
        messagePanel.transform.localScale = Vector3.one; // leave this out and see how the panel doesn't appear if it's scale is (0,0,0)
        ResizePanel(); //resize the panel to reflect any changes made to the start and end positions
    }

    public GameObject CreateCanvas()
    {
        GameObject c = new GameObject("Canvas");
        c.AddComponent<Canvas>();
        c.AddComponent<CanvasScaler>();
        c.AddComponent<GraphicRaycaster>();
        return c;
    }

    public void ResizePanel()
    {
        messagePanel.Resize(startPosition, endPosition);
    }
    #endregion
}
