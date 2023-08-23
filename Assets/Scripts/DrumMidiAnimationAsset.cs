using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Klak.Timeline.Midi
{
    [Serializable]
    public sealed class DrumMidiAnimationAsset : PlayableAsset, ITimelineClipAsset
    {
        public List<MidiEvent> midiEvents = new List<MidiEvent>();

        public ClipCaps clipCaps => ClipCaps.None; // Adjust as needed

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            // Create and return a DrumMidiAnimation playable here
            var playable = ScriptPlayable<DrumMidiAnimation>.Create(graph);
            var behaviour = playable.GetBehaviour();
            behaviour.midiEvents = midiEvents;
            return playable;
        }

    }

    [TrackColor(0.4f, 0.4f, 0.4f)]
    [TrackClipType(typeof(DrumMidiAnimationAsset))]
    [TrackBindingType(typeof(GameObject))]
    public sealed class DrumMidiAnimationTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            // Create and return a DrumMidiAnimationMixer playable here
            return base.CreateTrackMixer(graph, go, inputCount);
        }


    }
    [ExecuteInEditMode]
    public sealed class DrumMidiAnimationMixer : PlayableBehaviour
    {
        public List<MidiEvent> midiEvents = new List<MidiEvent>();

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            base.ProcessFrame(playable, info, playerData);

            // Process MIDI events and trigger drum events
        }
    }
    [System.Serializable]
    public sealed class DrumMidiAnimation : PlayableBehaviour
    {
        internal List<MidiEvent> midiEvents;

        // You can add additional properties or methods specific to drum animations here

        public override void OnGraphStart(Playable playable)
        {
            base.OnGraphStart(playable);
            // Additional initialization specific to drum animations
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            base.OnBehaviourPause(playable, info);
            // Additional behavior for pausing drum animations
        }

        public override void PrepareFrame(Playable playable, FrameData info)
        {
            base.PrepareFrame(playable, info);
            // Additional frame preparation for drum animations
        }

        // Add more methods or properties as needed for drum animations
    }
}