using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[AddComponentMenu("UI/My Input Field")]
public class MyInputField : InputField
{
    private readonly Event eventWork = new Event();
    private MyInputFieldProperty property;
#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();
        if (gameObject.GetComponent<MyInputFieldProperty>() == null)
        {
            gameObject.AddComponent<MyInputFieldProperty>();
        }
        if (textComponent == null)
        {
            textComponent = gameObject.transform.Find("Text").GetComponent<Text>();
        }
        if (placeholder == null)
        {
            placeholder = gameObject.transform.Find("Placeholder").GetComponent<Graphic>();
        }
    }
#endif
    protected override void Awake()
    {
        base.Awake();
        property = gameObject.GetComponent<MyInputFieldProperty>();
    }
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        RectTransform caretTransform = transform.Find(gameObject.name + " Input Caret").GetComponent<RectTransform>();
        caretTransform.pivot = new Vector2(0.5f, property.PivotY);
    }
    public override void OnUpdateSelected(BaseEventData eventData)
    {
#if false
        base.OnUpdateSelected(eventData);
#else
        while (Event.PopEvent(eventWork))
        {
            //UnityEngine.Debug.Log(eventWork.ToString());
            if ((eventWork.rawType == EventType.KeyDown) || (eventWork.rawType == EventType.KeyUp))
            {
                if (!IsAllowedCombination(eventWork))
                {
                    continue;
                }
            }
            ProcessEvent(eventWork);
        }
        UpdateLabel();
        eventData.Use();
#endif
    }

    private bool IsAllowedCombination(Event evt)
    {
        bool keyUp = (evt.rawType == EventType.KeyUp);
        EventModifiers currentEventModifiers = evt.modifiers;
        if ((currentEventModifiers & EventModifiers.Alt) != 0)
        {
            return true;
        }
        EventModifiers controlModifier;
        switch(Application.platform)
        {
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
            case RuntimePlatform.OSXWebPlayer:
                controlModifier = EventModifiers.Command;
                break;
            case RuntimePlatform.LinuxPlayer:
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsWebPlayer:
            case RuntimePlatform.WP8Player:
            case RuntimePlatform.WSAPlayerARM:
            case RuntimePlatform.WSAPlayerX64:
            case RuntimePlatform.WSAPlayerX86:
                controlModifier = EventModifiers.Control;
                break;
            default:
                controlModifier = EventModifiers.None;
                break;
        }
        bool ctrl = (currentEventModifiers & controlModifier) != 0;
        switch (evt.keyCode)
        {
            case KeyCode.LeftArrow:
            case KeyCode.RightArrow:
            case KeyCode.UpArrow:
            case KeyCode.DownArrow:
            case KeyCode.Insert:
            case KeyCode.Home:
            case KeyCode.End:
            case KeyCode.Escape:
            case KeyCode.PageUp:
            case KeyCode.PageDown:
            case KeyCode.LeftControl:
            case KeyCode.RightControl:
                if (!keyUp)
                {
                    return true;
                }
                break;
            //case KeyCode.Return:
            case KeyCode.None:
                if(eventWork.character == '\n')
                {
                    bool submit = true;
                    if (multiLine)
                    {
                        submit = evt.shift ^ property.SwapShiftReturn;
                    }
                    if (submit)
                    {
                        SendOnSubmit();
                        DeactivateInputField();
                    }
                    else
                    {
                        Append('\n');
                    }
                    return false;
                }
                break;
            case KeyCode.A:
                if (ctrl)
                {
                    // select all
                    return true;
                }
                break;
            case KeyCode.C:
                if (ctrl)
                {
                    // copy
                    return true;
                }
                break;
            case KeyCode.V:
                if (ctrl)
                {
                    // past
                    if(!keyUp)
                    {
                        if(property.ReadOnly)
                        {
                            return false;
                        }
                        char prv = '\0';
                        foreach (char c in GUIUtility.systemCopyBuffer)
                        {
                            char p = prv;
                            prv = c;
                            if((c == '\n') || (c == '\r'))
                            {
                                if ((c == '\r') && (p == '\n'))
                                {
                                    continue;
                                }
                                if ((c == '\n') && (p == '\r'))
                                {
                                    continue;
                                }
                                if (!multiLine)
                                {
                                    continue;
                                }
                                Append('\n');
                                continue;
                            }
                            if (c < 0x20)
                            {
                                continue;
                            }
                            Append(c);
                        }
                    }
                    return false;
                }
                break;
            default:
                break;
        }
        if(keyUp)
        {
            return false;
        }
        return property.ReadOnly ? false : true;
    }
}
