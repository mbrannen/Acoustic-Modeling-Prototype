
/* - Description -

This is a synthesizer of nostalgic FM sound generator.
Like OPM.
Real-time composition.
There is a sequencer of simple MML.

*Benefit
 Wonderful FM sound!
 Sound data are very very small.
 You can change the tempo and key dynamically.

*Free version limitations.
 8 voices only.
 Low resolution. (31250sps)
 High load. (not native code)
 Not compatible with perfection with OPM.
 There are some delays.
 Supports c# only.
 Poor tools..
 (It is free that you release superior tools for this synthesizer.)

*Sample Tone
 Preset tone data are produced by Takeshi Abo.
 Web page: VAL-SOUND url:http://www.valsound.com/
 I got permission to use. Thank you, Mr. Abo.
 Notice:
   These tone data are made for OPM.
   There is some difference in OPM and this synthesizer.
   If you have any question contact me.



- Overview -

# File
  MySynthesizer.dll

# Classes
  // Mixer class. Mixing and buffering MySynthesizer's outputs.
  MySpace.Synthesizer.MyMixer;

  // Synthesizer base class
  MySpace.Synthesizer.MySynthesizer;

  // MML sequence data of parsing from text.
  MySpace.Synthesizer.MMLSequencer.MyMMLSequence;

  // MML sequencer
  MySpace.Synthesizer.MMLSequencer.MyMMLSequencer;

  // 8 voices Phase Modulation Synthesizer.
  MySpace.Synthesizer.PM8.MySynthesizerPM8;

# Thread model
  main     - mixer(),sequencer(),synthesizer()   synthe.channel[].xxx   mixer.update ...
  worker       +-mixer.thread start                +--internal process    +--internal process --> sequencer.tick -->synthesize.channel[].xxx

  Unity (OnAudioFilterRead)    -mixer.output  -mixer.output  -mixer.output ....

# Volume
  velocity * channel[].Volume * synthesizer.MasterVolume * mixer.MasterVolume

# Tools
 MySynthesizer/ToolForWindows/MyMMLChecker.exe
   This is commandline program.
   It can output to wav file.
   Source codes are bundled.(MyMMLChecker.zip)

 MySynthesizer/Sample/Sample.unity
   This is a sample scene for Unity.
   Simple MMLPlayer & Tone editor.

# Supports
   URL:http://cottonia-cotton.cocolog-nifty.com/backyard/unityasset/
   Mail:r.benjamin.cotton@outlook.jp

# Release notes
   2015/xx version 1.0 first release.


- Usage -
    * Add component like this class to the GameObject having AudioListner.
    public class YourMusicClass : MonoBehaviour{
        private MyMixer mix;
        private MySynthesizer ss0;
        private MyMMLSequencer seq;

        void OnEnable(){
            mix = new MyMixer(freq, false);         // <-- raise worker thread!
            ss0 = new MySynthesizerPM8(mix);        // <-- connect synthesizer to mixer
            seq = new MyMMLSequencer(mix.TickFrequency);
            mix.TickCallback = () => seq.Tick();    // <-- connect sequencer to mixer
        }
        void OnDisable(){
            ss0.Terminate();                        // <-- disconnect mixer
            mix.Terminate();                        // <-- terminate thread
            seq = null;
            ss0 = null;
            mix = null;
        }
        void Update(){
            mix.Update();                           // <-- This promotes the processing in the other thread
        }
        void OnAudioFilterRead(float[] data, int channels){
            var m = mix;
            if(m != null){
                m.Output(data, channels, data.Length / channels);
            }
        }

        void PlayMusic(string mmlText)
        {
            // parse mml text
            MyMMLSequence mml = new MyMMLSequence(mmlText);
            if(mml.ErrorLine != 0){
                throw new System.FormatException("mml parser error. " + mml.ErrorLine + ":" + mml.ErrorPosition + ":" + mml.ErrorString);
            }
            // setup tones
            List<object> toneSet0 = new List<object>();
            Dictionary<int, int> toneMap0 = new Dictionary<int, int>();
            for (int i = 0; i < mml.ToneData.Count; i++)
            {
                object tone = ss0.CreateToneObject(mml.ToneData[i]);
                toneMap0.Add(i, toneSet0.Count);
                toneSet0.Add(tone);
            }

            seq.SetSynthesizer(0, ss0, toneSet0, toneMap0, 0xffffffffU);
            seq.KeyShift = 0;
            seq.VolumeShift = 0.0f;
            seq.TempoShift = 0.0f;
            seq.AppDataEvent = () =>{
                // If there is any processing...
            }
            seq.Play(mml, 0.0f, true);
        }
    }


- Class reference -

namespace MySpace
{
    namespace Synthesizer
    {
        // mixer
        class MyMixer
        {
            // outputSampleRate: Number of samples per second
            // offline: false=The output is complemented, true:Only the thing that input is output
            public MyMixer(uint32_t outputSampleRate, bool offline);

            // linear multiplyer default:1.0f
            public float MasterVolume;

            // ticks per second. readonly. Value fixed now 3125/sec
            public uint32_t TickFrequency;

            // TickCallbak is called every ticks.
            // In onlinemode, this is called from another thread.
            public Action TickCallback;

            // Update with connected synthesizers. fill internal buffer.
            public void Update();

            // Merge output to data.
            // returns outputs length.
            // It is filled up with the online mode.
            // Only an existing sapmles are output with offline.(The frequency is converted)
            public int Output(float[] data, int channels, int length);
        }

        // midi like channel interface
        // [] default value [-] not defined
        // {} Original expansion
        // () offset value
        interface IChannel
        {
            void NoteOn(uint8_t nn, uint8_t vel);   // [-,-] 0~127, 0~127
            void NoteOff(uint8_t nn);               // [-] 0~127
            void FineTuning(uint16_t val);          // [0x4000] 0~16383(-8192)
            void PitchBend(uint16_t val);           // [0x4000] 0~16383(-8192)
            void BendRange(uint8_t val);            // [0x02] 1~12
            void Pressure(uint8_t val);             // [-] 0~127
            void Damper(uint8_t val);               // [0] 0~127 open {, 128~255(-256) close }
            void Modulation(uint8_t val);           // [0] 0~127 {, 128~255(-256) }
            void Portament(uint8_t val);            // [0] 0~63:off 64~127:on
            void PortamentTime(uint8_t val);        // [0] 0~127
            void PortamentCtrl(uint8_t val);        // [-] 0~127
            void AllNoteOff();
            void AllSoundOff();
            void AllReset();
            void ProgramChange(object val);
            void Volume(uint8_t val);               // [100] 0~127
            void Panpot(uint8_t val);               // [64] 0~127
            void Polyphonic(uint8_t val);           // { [15][0~15](+1) }
            void Priority(uint8_t val);             // { [8] 0~15 }
            void VoiceMask(uint32_t val);           // { [0xffffffff] 0~0xffffffff }
        }

        // my synthesizer base class.
        class MySynthesizer
        {
            // The base frequency is 31250
            public const uint32_t BaseFrequency;

            // The number of the channels is 16
            public const int NumChannels;

            // The number of the voices is returned by inherit this.
            public abstract int MaxNumVoices

            // midi like channel interfaces
            public Collection<IChannel> Channel

            // voice state. each bit 0:idle 1:playing
            public uint32_t VoiceState;

            // 127=-0db default:100
            public void MasterVolume(uint8_t);

            // create tone object from string
            public abstract object CreateToneObject(string data);

            // validate tone object
            // return: cloneing of validated tone object.
            public abstract object ValidateToneObject(object tone);
        }

        // phase modulation synthesizer like opm
        namespace PM8
        {
            class EnvelopeParam
            {
                public uint8_t KS;              // 2bit key scale
                public uint8_t VS;              // 3bit velocity sense
                                                //  vs0: 1.0 constant
                                                //  vs1: vel
                                                //  vs2: vel^2
                                                //  vs3: vel^4
                                                //  vs4: vel^2 * 0.3 + 0.82858
                                                //  vs5: vel^2 * 0.6 + 0.65716
                                                //  vs7: 0.0 reserved
                public uint8_t TL;              // 7bit total level
                public uint8_t AR;              // 5bit attack rate
                public uint8_t DR;              // 5bit decay rate
                public uint8_t SL;              // 4bit sustain level
                public uint8_t SR;              // 5bit sustain rate
                public uint8_t RR;              // 4bit release rate
            }
            class LFOParam
            {
                public uint8_t WS;              // 3bit wave style 0:sine,1:triangle,2:sawtooth,3:square,4~7:reserved
                public uint8_t LF;              // 7bit LFO frequency
                public uint8_t LP;              // 7bit pitch modulation power
                public uint8_t LA;              // 7bit amplitude modulation power
                public EnvelopeParam Env;       // envelope
            }
            class OperatorParam{
                public uint8_t WS;              // 3bit wave style 0:sine,1:triangle,2:sawtooth,3:square,4:half sine, 5~6:reserved, 7:noise
                public uint8_t AE;              // 1bit amplitude modulation
                public uint8_t Ml;              // 4bit multiple
                public uint8_t MF;              // 7bit fraction of multiple  Ml.MF (0.0=0.5)
                public uint8_t Dt;              // 3bit detune
                public uint8_t Fx;              // 7bit fixed note number. 0==disabled
                public EnvelopeParam Env;       // envelope param
            }
            class ToneParam{
                public uint8_t Al;              // 3bit algorithm
                                                //  al0 fb-0-1-2-3
                                                //  al1 [[fb-0]+1]-2-3
                                                //  al2 [[fb-0]+[1-2]]-3
                                                //  al3 [[fb-0-1]+2]-3
                                                //  al4 [fb-0-1]+[2-3]
                                                //  al5 [fb-0]-[1+2+3]
                                                //  al6 [fb-0-1]+2+3
                                                //  al7 [fb-0]+1+2+3
                public uint8_t Fb;              // 3bit feedback
                public Op[4];                   // quad operator params
                public LFOParam Lfo;            // lfo param

                // formatted text
                // @fmt[Al Fb
                // op1[WS AE Ml MF Dt Fx KS VS TL AR DR SL SR RR]
                // op2[WS AE Ml MF Dt Fx KS VS TL AR DR SL SR RR]
                // op3[WS AE Ml MF Dt Fx KS VS TL AR DR SL SR RR]
                // op4[WS AE Ml MF Dt Fx KS VS TL AR DR SL SR RR]
                // lfo[WS FQ PM AM AR DR SL SR RR]]
                // true:success
                public bool LoadFromString(string text);

                static public string ToString(ToneParam param);

                // deep cloning.
                public ToneParam Clone()
            }
            class MySynthesizerPM8
            {
                // construct and connect to mixer.
                public MySynthesizerPM8(MyMixer mix);

                // inherit from MySynthesizer
                // return ToneParam
                public override object CreateToneObject(string data)
                public override object ValidateToneObject(object obj)

                // 8!
                public override int NumVoices;
            }
        }

        namespace MMLSequencer
        {
            delegate void AppDataEventFunc(int Port, int Channel, int TrackNo, uint MeasureCount, uint TickCount, string Data);
            delegate void PlayingEventFunc(int Port, int Channel, int TrackNo, uint MeasureCount, uint TickCount, uint Step, uint Gate, Instruction Inst, MyMMLSequence Sequence, int SectionIndex, int InstructionIndex);

            class MyMMLSequencer
            {
                // 480
                public UInt32 TimeBase;

                // mml %appDataEvent%
                public AppDataEventFunc AppDataEvent;

                // Called by all instructions
                // for debug and monitor
                public PlayingEventFunc PlayingEvent;

                // -1.0f~+1.0f(+1.0f)[0.0f] volume * 0.0f~2.0f
                public float VolumeShift;

                // -1.0f~+1.0f(+1.0f)[0.0f] tempo * 0.0f~2.0f
                public float TempoShift;

                // -12~+12[0]
                public int KeyShift;

                // The tempo that playing now
                public Byte Tempo;

                // true is playing
                // It continues until a sound goes out
                public bool Playing;

                // total tick counts until now
                public UInt32 TickCount;

                // constructor
                // tickFreq = ticks per second
                public MyMMLSequencer(UInt32 tickFreq);

                // toneSet list of tone objects
                // toneMap.key=index in the sequence. value=index in the toneSet
                // voiceMask=0xffffffff
                public void SetSynthesizer(int port, MySynthesizer synth, List<object> toneSet, Dictionary<int, int> toneMap, UInt32 voiceMask);

                // process one tick
                public void Tick();

                // Set toneSet,toneMap and synthesizers beforehand.
                // fadeInTime in second
                // loop is true theh endless mode.
                // if false, d.s. and d.c. becomes only once forcibly
                public void Play(MyMMLSequence seq, float fadeInTime, bool loop);

                // fadeOutTime Stop the music in second.
                public void Stop(float fadeOutTime);

                public void Pause(float fadeOutTime);

                public void Continue(float fadeInTime);
            }
            class MyMMLSequence
            {
                public const int MaxNumPorts = 4;
                public const int MaxNumTracks = 100;
                public const int MaxNumRepeats = 8;
                public const int MaxNumChord = 8;

                // The line where the error occurred. 0=no error
                public int ErrorLine;

                // Character offset into the line where the error occurred.
                public int ErrorPosition;

                // String of the place that the error occurred
                public string ErrorString;

                // Property map $<key>=value
                public Dictionary<string, string> Property

                // List of all $@(sss)=toneData in mml
                public List<string> ToneData

                // List of all $@(toneName) in mml
                public List<string> ToneName

                // List of all %AppData% in mml
                public List<string> AppData

                // Music instructions.
                public List<Section> Score
                public List<TrackInfo> Track


                // Prase mml text and construct music data.
                public MyMMLSequence(string mml)
            }
        }
    }
}

