
using System;
using System.Collections.Generic;

using UnityEngine;

using MySpace.Synthesizer;
using MySpace.Synthesizer.PM8;
using MySpace.Synthesizer.MMLSequencer;



public class MusicPlayer : MonoBehaviour
{
    private MyMixer Mixer;
    private MyMMLSequencer Sequencer;
    public MySynthesizer Synthesizer
    {
        get;
        private set;
    }
    public float MixerVolume
    {
        get
        {
            if (Mixer == null)
            {
                return 0.0f;
            }
            return Mixer.MasterVolume;
        }
        set
        {
            if (Mixer != null)
            {
                Mixer.MasterVolume = value;
            }
        }
    }
    public bool Playing {
        get
        {
            if (Sequencer == null)
            {
                return false;
            }
            return Sequencer.Playing;
        }
    }
    public uint TimeBase
    {
        get
        {
            if(Sequencer == null)
            {
                return 0;
            }
            return (uint)Sequencer.TimeBase;
        }
    }

    public string ParseMMLText(string text, out MyMMLSequence mml)
    {
        mml = new MySpace.Synthesizer.MMLSequencer.MyMMLSequence(text);
        if (mml.ErrorLine != 0)
        {
            return "Error line " + mml.ErrorLine + ":" + mml.ErrorPosition + " " + mml.ErrorString;
        }
        return null;
    }
    public string CreateDefaultToneMap(MyMMLSequence mml, out List<object> toneSet, out Dictionary<int, int> toneMap)
    {
        toneSet = new List<object>();
        toneMap = new Dictionary<int, int>();
        if ((mml == null) || (Synthesizer == null))
        {
            return "Failed: CreateDefaultToneMap()";
        }
        for (var i = 0; i < mml.ToneData.Count; i++)
        {
            object tone = Synthesizer.CreateToneObject(mml.ToneData[i]);
            //tone = new MySpace.Synthesizer.PM8.ToneParam(); // dummy tone
            if (tone != null)
            {
                toneMap.Add(i, toneSet.Count);
                toneSet.Add(tone);
            }
            else
            {
                return "Failed: CreateDefaultToneMap(): toneData[" + mml.ToneName[i] + "] :" + mml.ToneData[i];
            }
        }
        return null;
    }
    public void Play(MyMMLSequence mml, List<object> toneSet, Dictionary<int, int> toneMap, float fadeInTime, AppDataEventFunc appDataEventFunc, PlayingEventFunc playingEventFunc)
    {
        if (Sequencer != null)
        {
            Sequencer.PlayingEvent = playingEventFunc;
            Sequencer.AppDataEvent = appDataEventFunc;
            Sequencer.SetSynthesizer(0, Synthesizer, toneSet, toneMap, 0xffffffffU);
            Sequencer.KeyShift = 0;
            Sequencer.VolumeShift = 0.0f;
            Sequencer.TempoShift = 0.0f;
            Sequencer.Play(mml, fadeInTime, false);
        }
    }
    public void Stop(float fadeOutTime)
    {
        if (Sequencer != null)
        {
            Sequencer.Stop(fadeOutTime);
        }
    }
    public void Pause(float fadeOutTime)
    {
        if (Sequencer != null)
        {
            Sequencer.Pause(fadeOutTime);
        }
    }
    public void Continue(float fadeInTime)
    {
        if (Sequencer != null)
        {
            Sequencer.Continue(fadeInTime);
        }
    }
    void OnEnable()
    {
        Mixer = new MyMixer((UInt32)AudioSettings.outputSampleRate, false);
        Synthesizer = new MySynthesizerPM8(Mixer);
        Sequencer = new MyMMLSequencer(Mixer.TickFrequency);
        Mixer.TickCallback = () => { Sequencer.Tick(); };

        //UnityEngine.Debug.Log("sampleRate=" + AudioSettings.outputSampleRate + " numVoices=" + Synthesizer.MaxNumVoices);
        int len;
        int num;
        AudioSettings.GetDSPBufferSize(out len, out num);
        //UnityEngine.Debug.Log("len=" + len);
        //UnityEngine.Debug.Log("num=" + num);
    }
    void OnDisable()
    {
        Synthesizer.Terminate();
        Mixer.Terminate();
        Sequencer = null;
        Synthesizer = null;
        Mixer = null;
    }
    void Update()
    {
        if (Mixer != null)
        {
            Mixer.Update();
        }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        var m = Mixer;
        if (m != null)
        {
            //MyDebug.Print("len=" + data.Length + " channels=" + channels);
            m.Output(data, channels, data.Length / channels);
        }
    }


    public AudioClip CreateAudioClip(string name, string txt)
    {
        MySpace.Synthesizer.MMLSequencer.MyMMLSequence mml;
        List<object> toneSet;
        Dictionary<int, int> toneMap;
        ParseMMLText(txt, out mml);
        CreateDefaultToneMap(mml, out toneSet, out toneMap);
        return CreateAudioClip(name, mml, toneSet, toneMap);
    }
    public AudioClip CreateAudioClip(string name, MyMMLSequence mml, List<object> toneSet, Dictionary<int, int> toneMap)
    {
        const int frequency = 44100;
        const int numChannels = 2;
        MyMixer mix = new MyMixer(frequency, true);
        MySynthesizer ss0 = new MySynthesizerPM8(mix);
        MyMMLSequencer seq = new MyMMLSequencer(mix.TickFrequency);
        mix.TickCallback = () => seq.Tick();

        seq.SetSynthesizer(0, ss0, toneSet, toneMap, 0xffffffffU);
        seq.Play(mml, 0.0f, false);

        int totalSamples = 0;
        LinkedList<float[]> temp = new LinkedList<float[]>();
        const int workSize = 4096;
        float[] work = new float[workSize * numChannels];
        bool first = true;
        for (;;)
        {
            Array.Clear(work, 0, work.Length);
            if (seq.Playing)
            {
                mix.Update();
            }
            int numSamples = mix.Output(work, numChannels, workSize);
            if (numSamples == 0)
            {
                break;
            }
            if (first)
            {
                // skip leading zeros.
                for(var i = 0; i < numSamples; i++)
                {
                    if((work[i * 2 + 0] != 0.0f) || (work[i * 2 + 1] != 0.0f))
                    {
                        first = false;
                        numSamples -= i;
                        float[] block = new float[numSamples * numChannels];
                        Array.Copy(work, i * 2, block, 0, numSamples * numChannels);
                        temp.AddLast(block);
                        break;
                    }
                }
            }
            else
            {
                float[] block = new float[numSamples * numChannels];
                Array.Copy(work, block, numSamples * numChannels);
                temp.AddLast(block);
            }
            totalSamples += numSamples;
        }
        AudioClip clip = AudioClip.Create(name, totalSamples, numChannels, frequency, false);
        int pos = 0;
        foreach (var block in temp)
        {
            clip.SetData(block, pos);
            pos += block.Length / numChannels;
        }
        ss0.Terminate();
        mix.Terminate();
        return clip;
    }
}
