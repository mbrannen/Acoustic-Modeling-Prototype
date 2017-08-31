
using System;

using UnityEngine;
using UnityEngine.UI;

using MySpace.Synthesizer.PM8;


public class ToneEditor : MonoBehaviour
{
    private MusicPlayer musicPlayer;
    public int ChNo = 15;

    private ToneParam param;

    private GameObject MMLEditorCanvas;

    private void setSliderValue(GameObject obj, string name, string label, int val)
    {
        Text text = obj.transform.Find(name + "/Text").gameObject.GetComponent<Text>();
        Slider slider = obj.transform.Find(name + "/Slider").gameObject.GetComponent<Slider>();
        text.text = label + val;
        slider.value = val;
    }
    private void setupSlider(GameObject obj, string name, string label, bool wholeNum, float min, float max, float cur, Action<float> apply)
    {
        Text text = obj.transform.Find(name + "/Text").gameObject.GetComponent<Text>();
        Slider slider = obj.transform.Find(name + "/Slider").gameObject.GetComponent<Slider>();
        text.text = label + cur;
        slider.wholeNumbers = wholeNum;
        slider.minValue = min;
        slider.maxValue = max;
        slider.value = cur;
        slider.onValueChanged.AddListener((value) =>
        {
            text.text = label + (wholeNum ? (int)value : value);
            apply.Invoke(value);
        });
    }

    private void loadTone(ToneParam tone)
    {
        param = tone.Clone();
        {
            GameObject obj = gameObject.transform.Find("Panel/FM").gameObject;
            setSliderValue(obj, "Algorithm", "al:", param.Al);
            setSliderValue(obj, "Feedback", "fb:", param.Fb);
            setSliderValue(obj, "WaveForm", "lw:", param.Lfo.WS);
            setSliderValue(obj, "Frequency", "lf:", param.Lfo.LF);
            setSliderValue(obj, "PMPower", "lp:", param.Lfo.LP);
            setSliderValue(obj, "AMPower", "la:", param.Lfo.LA);
            setSliderValue(obj, "AttackRate", "ar:", param.Lfo.Env.AR);
            setSliderValue(obj, "DecayRate", "dr:", param.Lfo.Env.DR);
            setSliderValue(obj, "SustainLevel", "sl:", param.Lfo.Env.SL);
            setSliderValue(obj, "SustainRate", "sr:", param.Lfo.Env.SR);
            setSliderValue(obj, "ReleaseRate", "rr:", param.Lfo.Env.RR);
        }
        for (int i = 0; i < 4; i++)
        {
            int n = i;
            GameObject obj = gameObject.transform.Find("Panel/OP" + (i + 1)).gameObject;
            setSliderValue(obj, "WaveStyle", "ws:", param.Op[n].WS);
            setSliderValue(obj, "AMEnable", "ae:", param.Op[n].AE);
            setSliderValue(obj, "Multiple", "ml:", param.Op[n].Ml);
            setSliderValue(obj, "Detune", "dt:", param.Op[n].Dt);
            setSliderValue(obj, "KeyScale", "ks:", param.Op[n].Env.KS);
            setSliderValue(obj, "VelocitySense", "vs:", param.Op[n].Env.VS);
            setSliderValue(obj, "TotalLevel", "tl:", param.Op[n].Env.TL);
            setSliderValue(obj, "AttackRate", "ar:", param.Op[n].Env.AR);
            setSliderValue(obj, "DecayRate", "dr:", param.Op[n].Env.DR);
            setSliderValue(obj, "SustainLevel", "sl:", param.Op[n].Env.SL);
            setSliderValue(obj, "SustainRate", "sr:", param.Op[n].Env.SR);
            setSliderValue(obj, "ReleaseRate", "rr:", param.Op[n].Env.RR);
        }
        if (musicPlayer.Synthesizer != null)
        {
            musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param);
        }
    }
    void Awake()
    {
        musicPlayer = GameObject.FindObjectOfType<MusicPlayer>();
        MMLEditorCanvas = transform.parent.Find("MMLPlayerCanvas").gameObject;
        //string defaultTone = "@fmt[7 0 op1[0 1 1 0 0 0 0 1 0 31 0 0 0 7] op2[0 1 1 0 0 0 0 1 127 31 0 0 0 7] op3[0 1 1 0 0 0 0 1 127 31 0 0 0 7] op4[0 1 1 0 0 0 0 1 127 31 0 0 0 7] lfo[0 127 0 0 31 0 0 0 0] ]";
        string defaultTone = "@pm8[7 0 op1[0 0 1 0 0 0 0 1 0 31 0 0 0 15] op2[0 0 1 0 0 0 0 1 127 31 0 0 0 15] op3[0 0 1 0 0 0 0 1 127 31 0 0 0 15] op4[0 0 1 0 0 0 0 1 127 31 0 0 0 15] lfo[0 127 0 0 31 0 0 0 0]]";
        param = new ToneParam(defaultTone);
        {
            Keyboard keyboard = gameObject.transform.Find("Panel/Keyboard").gameObject.GetComponent<Keyboard>();
            keyboard.MusicPlayer = musicPlayer;
            keyboard.ChNo = ChNo;
        }
        {
            InputField inputField = gameObject.transform.Find("Panel/InputField").gameObject.GetComponent<InputField>();
            inputField.onEndEdit.AddListener((v) =>
            {
                if ((v != null) && (v.Length != 0))
                {
                    loadTone(new ToneParam(v));
                    inputField.text = param.ToString();
                }
                else
                {
                    loadTone(new ToneParam(defaultTone));
                }
            });

            Button button = gameObject.transform.Find("Panel/InputField/Button").gameObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                inputField.text = param.ToString();
            });

            PresetButton [] presets = gameObject.GetComponentsInChildren<PresetButton>();
            foreach(PresetButton pb in presets)
            {
                Button btn = pb.GetComponent<Button>();
                ToneParam tp = new ToneParam(pb.ToneData);
                btn.onClick.AddListener(() =>
                {
                    loadTone(tp);
                    inputField.text = param.ToString();
                });
            }
        }
        {
            GameObject obj = gameObject.transform.Find("Panel/FM").gameObject;
            setupSlider(obj, "Algorithm", "al:", true, 0, 7, param.Al, (val) => { param.Al = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "Feedback", "fb:", true, 0, 7, param.Fb, (val) => { param.Fb = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "WaveForm", "lw:", true, 0, 7, param.Lfo.WS, (val) => { param.Lfo.WS = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "Frequency", "lf:", true, 0, 127, param.Lfo.LF, (val) => { param.Lfo.LF = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "PMPower", "lp:", true, 0, 127, param.Lfo.LP, (val) => { param.Lfo.LP = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "AMPower", "la:", true, 0, 127, param.Lfo.LA, (val) => { param.Lfo.LA = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "AttackRate", "ar:", true, 0, 31, param.Lfo.Env.AR, (val) => { param.Lfo.Env.AR = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "DecayRate", "dr:", true, 0, 31, param.Lfo.Env.DR, (val) => { param.Lfo.Env.DR = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "SustainLevel", "sl:", true, 0, 15, param.Lfo.Env.SL, (val) => { param.Lfo.Env.SL = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "SustainRate", "sr:", true, 0, 31, param.Lfo.Env.SR, (val) => { param.Lfo.Env.SR = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "ReleaseRate", "rr:", true, 0, 15, param.Lfo.Env.RR, (val) => { param.Lfo.Env.RR = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
        }
        for (int i = 0; i < 4; i++)
        {
            int n = i;
            GameObject obj = gameObject.transform.Find("Panel/OP" + (i + 1)).gameObject;
            setupSlider(obj, "WaveStyle", "ws:", true, 0, 7, param.Op[n].WS, (val) => { param.Op[n].WS = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "AMEnable", "ae:", true, 0, 1, param.Op[n].AE, (val) => { param.Op[n].AE = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "Multiple", "ml:", true, 0, 15, param.Op[n].Ml, (val) => { param.Op[n].Ml = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "Detune", "dt:", true, 0, 7, param.Op[n].Dt, (val) => { param.Op[n].Dt = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "KeyScale", "ks:", true, 0, 3, param.Op[n].Env.KS, (val) => { param.Op[n].Env.KS = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "VelocitySense", "vs:", true, 0, 7, param.Op[n].Env.VS, (val) => { param.Op[n].Env.VS = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "TotalLevel", "tl:", true, 0, 127, param.Op[n].Env.TL, (val) => { param.Op[n].Env.TL = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "AttackRate", "ar:", true, 0, 31, param.Op[n].Env.AR, (val) => { param.Op[n].Env.AR = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "DecayRate", "dr:", true, 0, 31, param.Op[n].Env.DR, (val) => { param.Op[n].Env.DR = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "SustainLevel", "sl:", true, 0, 15, param.Op[n].Env.SL, (val) => { param.Op[n].Env.SL = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "SustainRate", "sr:", true, 0, 15, param.Op[n].Env.SR, (val) => { param.Op[n].Env.SR = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
            setupSlider(obj, "ReleaseRate", "rr:", true, 0, 31, param.Op[n].Env.RR, (val) => { param.Op[n].Env.RR = (Byte)val; musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param); });
        }
        {
            Button button = gameObject.transform.Find("Panel/MMLEditorButton").gameObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                MMLEditorCanvas.SetActive(true);
                gameObject.SetActive(false);
            });
        }
    }
    void Start()
    {
        musicPlayer.Synthesizer.Channel[ChNo].ProgramChange(param);
    }
}

