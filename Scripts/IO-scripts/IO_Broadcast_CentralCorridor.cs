using System.Collections.Generic;
using UnityEngine;

public class IO_Broadcast_CentralCorridor : MonoBehaviour
{
    [Header("Output logic component names")]

        [SerializeField, Tooltip("Light on")]
        private string a_broadcast_off;

        [SerializeField, Tooltip("Light off")]
        private string a_broadcast_live;

        [SerializeField, Tooltip("Light on")]
        private string a_broadcast_message;

    [Header("Input logic component names")]

        [SerializeField, Tooltip("The current recording is stopped")]
        private string s_recordingStopped;

    [Header("Settings")]

        [SerializeField, Tooltip("Warning box that shows when multiple actuators are turned on")]
        private GameObject WarningBox;

        [SerializeField, Tooltip("Audio source of the speaker for broadcast mode")]
        private AudioSource SpeakerAudioSourceBroadcast;

        [SerializeField, Tooltip("Audio clip playing when a broadcast message is enabled")]
        private AudioClip Message_Broadcast;

        [SerializeField, Tooltip("Audio clip playing when a broadcast message is enabled")]
        private AudioClip Live_Broadcast;

    private Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();
    private bool StartedPlayingMessage = false;

    private void Start()
    {
        // Filling the I/O dictionary

            // Actuators
            IO_dict.Add("a_broadcast_off", Static_Functions.FindBoolToggle(a_broadcast_off));
            IO_dict.Add("a_broadcast_live", Static_Functions.FindBoolToggle(a_broadcast_live));
            IO_dict.Add("a_broadcast_message", Static_Functions.FindBoolToggle(a_broadcast_message));

            // Sensors
            IO_dict.Add("s_recordingStopped", Static_Functions.FindBoolToggle(s_recordingStopped));
    }

    private void Update()
    {
        // Check whether there are conflicting output signals
        if (IO_dict["a_broadcast_off"].Boolean && IO_dict["a_broadcast_live"].Boolean && IO_dict["a_broadcast_message"].Boolean)
        {
            WarningBox.SetActive(true);
        }

        // --- Enable events based on the actuator states ---
        if (IO_dict["a_broadcast_off"].Boolean)
        {
            StartedPlayingMessage = false;
            SpeakerAudioSourceBroadcast.mute = true;
        }
        else if (IO_dict["a_broadcast_live"].Boolean)
        {
            StartedPlayingMessage = false;
            SpeakerAudioSourceBroadcast.mute = false;
        }
        else if (IO_dict["a_broadcast_message"].Boolean)
        {
            SpeakerAudioSourceBroadcast.mute = false;

            if (!StartedPlayingMessage)
            {
                SpeakerAudioSourceBroadcast.PlayOneShot(Message_Broadcast);
                StartedPlayingMessage = true;
            }
        }

        // --- Set the sensor values based on states of the components ---
        IO_dict["s_recordingStopped"].Boolean = !SpeakerAudioSourceBroadcast.isPlaying && StartedPlayingMessage;
    }
}