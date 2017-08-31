
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour{
    public MusicPlayer MusicPlayer;
    public KeyProperty BlackKey;
    public KeyProperty WhiteKey;
    public int BaseNote = 72;
    public int ChNo = 0;

    private int numKeys;
    private int vel = 64;
    private int vol = 100;

    void OnKeyDown(int index)
    {
        //UnityEngine.Debug.Log("key dw:" + (baseNote + index));
        MusicPlayer.Synthesizer.Channel[ChNo].NoteOn((byte)(BaseNote + index), (byte)vel);
    }
    void OnKeyUp(int index)
    {
        //UnityEngine.Debug.Log("key up:" + (baseNote + index));
        MusicPlayer.Synthesizer.Channel[ChNo].NoteOff((byte)(BaseNote + index));
    }

    // Use this for initialization
    void Awake() {
        MusicPlayer = GameObject.FindObjectOfType<MusicPlayer>();

        RectTransform p = gameObject.GetComponent<RectTransform>();
        RectTransform r = WhiteKey.gameObject.GetComponent<RectTransform>();
        float width = r.rect.width;
        Vector3 basePos = WhiteKey.transform.localPosition;
        int[] ofs = { 0, 1, 2, 3, 4, 6, 7, 8, 9, 10, 11, 12 };
        numKeys = (int)((p.rect.width - (p.rect.width / 2 + (basePos.x - width / 2)) * 2) / width) * 12 / 7;
        for (int i = 0; i < numKeys; i++)
        {
            int o = ofs[i % 12];
            if((o & 1) != 0){
                continue;
            }
            int index = i;
            KeyProperty key = Instantiate(WhiteKey);
            Vector3 pos = basePos;
            Vector3 scl = key.transform.localScale;
            pos.x += ((i / 12) * 14 + o) * (width / 2);
            key.transform.SetParent(gameObject.transform);
            key.transform.localPosition = pos;
            key.transform.localScale = scl;
            key.OnKeyDownEvent.AddListener(() => OnKeyDown(index));
            key.OnKeyUpEvent.AddListener(() => OnKeyUp(index));
        }
        for (int i = 0; i < numKeys; i++)
        {
            int o = ofs[i % 12];
            if ((o & 1) == 0)
            {
                continue;
            }
            int index = i;
            KeyProperty key = Instantiate(BlackKey);
            Vector3 pos = basePos;
            Vector3 scl = key.transform.localScale;
            pos.x += ((i / 12) * 14 + o) * (width / 2);
            key.transform.SetParent(gameObject.transform);
            key.transform.localPosition = pos;
            key.transform.localScale = scl;
            key.OnKeyDownEvent.AddListener(() => OnKeyDown(index));
            key.OnKeyUpEvent.AddListener(() => OnKeyUp(index));
        }

        Text position = gameObject.transform.Find("Position").gameObject.GetComponent<Text>();
        Button lshift = gameObject.transform.Find("LShift").gameObject.GetComponent<Button>();
        BaseNote -= BaseNote % 12;
        position.text = "^C" + (BaseNote / 12 - 2 + 1);
        lshift.onClick.AddListener(() =>
        {
            if (BaseNote - 12 >= 0)
            {
                BaseNote -= 12;
                position.text = "^C" + (BaseNote / 12 - 2 + 1);
            }
        });
        Button rshift = gameObject.transform.Find("RShift").gameObject.GetComponent<Button>();
        rshift.onClick.AddListener(() =>
        {
            if (BaseNote + numKeys + 12 <= 128)
            {
                BaseNote += 12;
                position.text = "^C" + (BaseNote / 12 - 2 + 1);
            }
        });
        Slider velocity = gameObject.transform.Find("Velocity").gameObject.GetComponent<Slider>();
        velocity.onValueChanged.AddListener((value) =>
        {
            vel = (int)value;
            velocity.gameObject.transform.Find("Value").gameObject.GetComponent<Text>().text = "" + vel;
        });

        Slider volume = gameObject.transform.Find("Volume").gameObject.GetComponent<Slider>();
        volume.onValueChanged.AddListener((value) =>
        {
            vol = (int)value;
            MusicPlayer.Synthesizer.MasterVolume((byte)value);
            volume.gameObject.transform.Find("Value").gameObject.GetComponent<Text>().text = "" + (byte)value;
        });

        KeyProperty hold = gameObject.transform.Find("Hold").gameObject.GetComponent<KeyProperty>();
        hold.OnKeyDownEvent.AddListener(() => MusicPlayer.Synthesizer.Channel[ChNo].Damper(+127));
        hold.OnKeyUpEvent.AddListener(() => MusicPlayer.Synthesizer.Channel[ChNo].Damper(0));

        KeyProperty damp = gameObject.transform.Find("Damp").gameObject.GetComponent<KeyProperty>();
        damp.OnKeyDownEvent.AddListener(() => MusicPlayer.Synthesizer.Channel[ChNo].Damper(-127 + 256));
        damp.OnKeyUpEvent.AddListener(() => MusicPlayer.Synthesizer.Channel[ChNo].Damper(0));
    }
    void Start()
    {
        Slider velocity = gameObject.transform.Find("Velocity").gameObject.GetComponent<Slider>();
        velocity.value = vel;
        Slider volume = gameObject.transform.Find("Volume").gameObject.GetComponent<Slider>();
        volume.value = vol;
    }
}
