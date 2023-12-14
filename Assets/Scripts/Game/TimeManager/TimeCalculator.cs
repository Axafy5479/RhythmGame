using UnityEngine;

namespace Game
{
    public class TimeCalculator : MonoSingleton<TimeCalculator>
    {
        [SerializeField] private AudioSource audioSource;

        private RealTime_AudioTime_Relation real_audio_relation;

        private void Start()
        {
            Stop();
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
        public float GetTime()
        {
            return audioSource.time;
        }

        /// <summary>
        ///     RealTimeからAudioSource.timeを導出する。
        ///     具体的な用途は、FingerのlastTouchの時刻から、楽曲時間でいつ叩いたかを計算する
        /// </summary>
        /// <param name="beat_time_as_realtime"></param>
        /// <returns></returns>
        public float GetBeatTime(float beat_time_as_realtime)
        {
            return real_audio_relation.GetAudioTimeFromRealTime(beat_time_as_realtime);
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
            public float GetAudioTimeFromRealTime(float realTime)
            {
                return AudioTime + realTime - RealTime;
            }
        }
    }
}