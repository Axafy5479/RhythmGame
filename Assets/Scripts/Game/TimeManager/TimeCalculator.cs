using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Game
{
    [RequireComponent(typeof(AudioSource))]
    public class TimeCalculator : MonoSingleton<TimeCalculator>, INeedInitializing
    {
        private AudioSource audioSource;

        private RealTime_AudioTime_Relation real_audio_relation;

        public void Initialize(GameInfo gameInfo)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = gameInfo.GetChart().Clip;
        }

        /// <summary>
        ///     楽曲を(続きから)再生する
        /// </summary>
        public void Play()
        {
            audioSource.Play();

            // 再生のタイミングで、RealTimeとAudioTimeを関係づける
            real_audio_relation = new RealTime_AudioTime_Relation(Time.realtimeSinceStartup, audioSource.time);
        }

        public void Stop()
        {
            audioSource.Stop();
        }

        /// <summary>
        ///     現在の再生時刻を取得する
        /// </summary>
        /// <returns></returns>
        public int GetTime()
        {
            return (int)(audioSource.time * 1000);
        }

        /// <summary>
        ///     RealTimeからAudioSource.timeを導出する。
        ///     具体的な用途は、FingerのlastTouchの時刻から、楽曲時間でいつ叩いたかを計算する
        /// </summary>
        /// <param name="beat_time_as_realtime"></param>
        /// <returns></returns>
        public int GetBeatTime(Finger finger)
        {
            double realTime = finger.lastTouch.time;
            float audioTime = real_audio_relation.GetAudioTimeFromRealTime(realTime);
            return (int)(audioTime * 1000);
        }

        /// <summary>
        ///     Time.realtimeSinceStartup と AudioSource.time の関係
        ///     両者の原点が異なるため、そのズレを保持している。
        ///     具体的には、RealTime と AudioTimeは値は異なるが同じ時点を示すため、両者の差が原点のズレを表している。
        /// </summary>
        private struct RealTime_AudioTime_Relation
        {
            /// <summary>
            ///     同じ時点を示すRealTimeとAudioTimeを引数にとるコンストラクタ
            /// </summary>
            /// <param name="realTime"></param>
            /// <param name="audioTime"></param>
            public RealTime_AudioTime_Relation(float realTime, float audioTime)
            {
                RealTime = realTime;
                AudioTime = audioTime;
            }

            private float RealTime { get; }
            private float AudioTime { get; }

            /// <summary>
            ///     RealTimeからAudioTimeに変換する
            /// </summary>
            /// <param name="realTime"></param>
            /// <returns></returns>
            public float GetAudioTimeFromRealTime(double realTime)
            {
                return (float)(AudioTime + realTime - RealTime);
            }
        }
    }
}