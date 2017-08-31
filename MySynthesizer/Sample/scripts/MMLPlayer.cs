using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MMLPlayer : MonoBehaviour {

    public float FadeTime = 0.0f;
    public TextAsset Sample1;
    public TextAsset Sample2;
    public TextAsset Sample3;
    public TextAsset Sample4;
    private MusicPlayer musicPlayer;
    private GameObject ToneEditorCanvas;
    private Text playButtonText;
    private Text cursolPosText;
    private InputField mmlField;
    private Text consoleText;
    private Text instructionText;
    private readonly System.Text.StringBuilder stb = new System.Text.StringBuilder();
    private int lineCount;
    private bool invalid = true;
    private bool playing = false;

    private AudioSource audioSource;
    private AudioClip clickSound;

    public void LoadSample1()
    {
        audioSource.PlayOneShot(clickSound);
        mmlField.text = (Sample1 != null) ? Sample1.text : "";
    }
    public void LoadSample2()
    {
        audioSource.PlayOneShot(clickSound);
        mmlField.text = (Sample2 != null) ? Sample2.text : "";
    }
    public void LoadSample3()
    {
        audioSource.PlayOneShot(clickSound);
        mmlField.text = (Sample3 != null) ? Sample3.text : "";
    }
    public void LoadSample4()
    {
        audioSource.PlayOneShot(clickSound);
        mmlField.text = (Sample4 != null) ? Sample4.text : "";
    }
    private void resetStb()
    {
        lock (stb)
        {
            lineCount = 0;
            stb.Remove(0, stb.Length);
        }
    }
    private void appendStb(string str)
    {
        lock (stb)
        {
            if (lineCount >= 100)
            {
                char[] cbuf = new char[1];
                for (var i = 0; i < stb.Length; i++)
                {
                    stb.CopyTo(i, cbuf, 0, 1);
                    if (cbuf[0] == '\n')
                    {
                        stb.Remove(0, i + 1);
                        break;
                    }
                }
                lineCount--;
            }
            stb.AppendLine(str);
            lineCount++;
        }
    }

    private void appDataEventFunc(int Port, int Channel, int TrackNo, uint MeasureCount, uint TickCount, string Data)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(Data);
        appendStb(sb.ToString());
    }
    private void playingEventFunc(int Port, int Channel, int TrackNo, uint MeasureCount, uint TickCount, uint Step, uint Gate, MySpace.Synthesizer.MMLSequencer.Instruction Inst, MySpace.Synthesizer.MMLSequencer.MyMMLSequence Sequence, int SectionIndex, int InstructionIndex)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //sb.AppendFormat("{0:D2}:{1:D2}:", Port, Channel);
        sb.AppendFormat("{0:D2} {1:D3}:{2:D3} {3:D3}:{4:D3} ", TrackNo, MeasureCount, TickCount, Step, Gate);
        if ((int)Inst.N == 0)
        {
            sb.Append(Inst.N);
        }
        else if ((int)Inst.N < 128)
        {
            char c0 = "ccddeffggaab"[(int)Inst.N % 12];
            char c1 = " # #  # # # "[(int)Inst.N % 12];
            int oct = ((int)Inst.N / 12) - 2;
            sb.AppendFormat("{0}{1}{2} {3:D3}", c0, c1, oct, Inst.V);
        }
        else
        {
            sb.AppendFormat("{0} <0x{1:X4}>", Inst.N, (Inst.V | ((int)Inst.Q << 8)));
        }
        appendStb(sb.ToString());
    }
    void Awake()
    {
        musicPlayer = GameObject.FindObjectOfType<MusicPlayer>();
        invalid = true;
        playing = false;
        ToneEditorCanvas = transform.parent.Find("ToneEditorCanvas").gameObject;
        {
            Button button = gameObject.transform.Find("Panel/ToneEditorButton").gameObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                ToneEditorCanvas.SetActive(true);
                gameObject.SetActive(false);
            });
        }
        {
            Button button = gameObject.transform.Find("Panel/PlayButton").gameObject.GetComponent<Button>();
            cursolPosText = gameObject.transform.Find("Panel/CursolPosText").gameObject.GetComponent<Text>();
            mmlField = gameObject.transform.Find("Panel/MMLField").gameObject.GetComponent<InputField>();
            consoleText = gameObject.transform.Find("Panel/ConsolePanel").gameObject.GetComponentInChildren<Text>();
            instructionText = gameObject.transform.Find("Panel/InstructionPanel").gameObject.GetComponentInChildren<Text>();
            playButtonText = button.GetComponentInChildren<Text>();
            mmlField.onValueChange.AddListener((str) =>
            {
                invalid = true;
                if (musicPlayer.Playing)
                {
                    playButtonText.text = "Stop";
                }
                else
                {
                    playButtonText.text = "Play";
                }
            });
            button.onClick.AddListener(() =>
            {
                if (invalid)
                {
                    if (musicPlayer.Playing)
                    {
                        musicPlayer.Stop(0.0f);
                        playButtonText.text = "Play";
                        consoleText.text = "";
                        playing = false;
                    }
                    else
                    {
                        MySpace.Synthesizer.MMLSequencer.MyMMLSequence mml;
                        string result = null;
                        result = musicPlayer.ParseMMLText(mmlField.text, out mml);
                        if (result == null)
                        {
                            List<object> toneSet;
                            Dictionary<int, int> toneMap;
                            result = musicPlayer.CreateDefaultToneMap(mml, out toneSet, out toneMap);
                            if (result == null)
                            {
                                musicPlayer.Play(mml, toneSet, toneMap, 0.0f, appDataEventFunc, playingEventFunc);
                                resetStb();
                                invalid = false;
                            }
                        }
                        if (result == null)
                        {
                            consoleText.text = "Playing...";
                            playButtonText.text = "Pause";
                            playing = true;
                        }
                        else
                        {
                            consoleText.text = result;
                            playButtonText.text = "Play";
                            playing = false;
                        }
                    }
                }
                else
                {
                    if (musicPlayer.Playing)
                    {
                        musicPlayer.Pause(FadeTime);
                        playButtonText.text = "Continue";
                        playing = false;
                    }
                    else
                    {
                        musicPlayer.Continue(FadeTime);
                        playButtonText.text = "Pause";
                        playing = true;
                    }
                }
            });
        }
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        {
            string clickMML = "$@(h_close5) = @pm8[4 7 op1[0 0 15 0 7 0 0 0 0 31 0 0 0 0] op2[0 0 0 0 7 0 0 1 0 21 18 5 15 13] op3[0 0 8 0 3 0 0 0 0 31 0 0 0 14] op4[0 0 15 0 3 0 1 1 5 31 17 15 13 9] lfo[0 127 0 0 31 0 0 0 0] ]\nt0=@(h_close5)a32c32";
            clickSound = musicPlayer.CreateAudioClip("click", clickMML);
        }
    }
    void Update()
    {
        if (playing)
        {
            string str;
            lock (stb)
            {
                str = stb.ToString();
            }
            instructionText.text = str;

            if (!musicPlayer.Playing)
            {
                invalid = true;
                playing = false;
                playButtonText.text = "Play";
                consoleText.text = "";
            }
        }
        {
            int line = 1;
            int pos = 1;
            for(int i = 0; i < mmlField.selectionFocusPosition; i++)
            {
                if(mmlField.text[i] == '\n')
                {
                    line++;
                    pos = 0;
                }
                pos++;
            }
            cursolPosText.text = line.ToString() + ":" + pos.ToString();
        }
    }
}
