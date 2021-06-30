using System.Collections.Generic;
using UnityEngine;

public class IO_Broadcast : MonoBehaviour
{
    [Header("Output logic component names")]

        [SerializeField, Tooltip("Light on")]
        private string a_HF_radio;

        [SerializeField, Tooltip("Light off")]
        private string a_HF_message;

        [SerializeField, Tooltip("Light on")]
        private string a_HF_mute;

        [SerializeField, Tooltip("Light on")]
        private string a_broadcast_off;

        [SerializeField, Tooltip("Light off")]
        private string a_broadcast_live;

        [SerializeField, Tooltip("Light on")]
        private string a_broadcast_message;

        [SerializeField, Tooltip("Light on")]
        private string a_broadcastSection_tube;

        [SerializeField, Tooltip("Light off")]
        private string a_broadcastSection_section;

    [Header("Input logic component names")]

        [SerializeField, Tooltip("The current HF message is finished")]
        private string s_HF_messageFinished;

        [SerializeField, Tooltip("The current HF message is finished")]
        private string s_broadcast_recordingFinished;

    [Header("Settings")]

        [SerializeField, Tooltip("Warning box that shows when multiple actuators are turned on")]
        private GameObject WarningBox;

        [SerializeField, Tooltip("Audio source of the speaker for broadcast mode")]
        private AudioSource SpeakerAudioSourceBroadcast;

        [SerializeField, Tooltip("Audio source of the speaker for HF mode")]
        private AudioSource SpeakerAudioSourceHF;

        [SerializeField, Tooltip("Audio clip playing when a broadcast message is enabled")]
        private AudioClip Message_Broadcast;

        [SerializeField, Tooltip("Audio clip playing when a broadcast message is enabled")]
        private AudioClip Live_Broadcast;

        [SerializeField, Tooltip("Audio clip playing when a HF message is enabled")]
        private AudioClip Message_HF;

        [SerializeField, Tooltip("Audio clip playing when HF radio is enabled")]
        private AudioClip Radio_HF;

    private Dictionary<string, BoolToggle> IO_dict = new Dictionary<string, BoolToggle>();
    private bool StartedPlayingMessage = false;

    private void Start()
    {
        // Filling the I/O dictionary

            // Actuators
            //IO_dict.Add("a_HF_radio", Static_Functions.FindBoolToggle(a_HF_radio));
            //IO_dict.Add("a_HF_message", Static_Functions.FindBoolToggle(a_HF_message));
            //IO_dict.Add("a_HF_mute", Static_Functions.FindBoolToggle(a_HF_mute));
            IO_dict.Add("a_broadcast_off", Static_Functions.FindBoolToggle(a_broadcast_off));
            IO_dict.Add("a_broadcast_live", Static_Functions.FindBoolToggle(a_broadcast_live));
            IO_dict.Add("a_broadcast_message", Static_Functions.FindBoolToggle(a_broadcast_message));
            IO_dict.Add("a_broadcastSection_tube", Static_Functions.FindBoolToggle(a_broadcastSection_tube));
            IO_dict.Add("a_broadcastSection_section", Static_Functions.FindBoolToggle(a_broadcastSection_section));

            // Sensors
            //IO_dict.Add("s_HF_messageFinished", Static_Functions.FindBoolToggle(s_HF_messageFinished));
            IO_dict.Add("s_broadcast_recordingFinished", Static_Functions.FindBoolToggle(s_broadcast_recordingFinished));
    }

    private void Update()
    {
        // Check whether there are conflicting output signals
        if (//IO_dict["a_HF_radio"].Boolean && IO_dict["a_HF_message"].Boolean && IO_dict["a_HF_mute"].Boolean ||
            IO_dict["a_broadcast_off"].Boolean && IO_dict["a_broadcast_live"].Boolean && IO_dict["a_broadcast_message"].Boolean ||
            IO_dict["a_broadcastSection_tube"].Boolean && IO_dict["a_broadcastSection_section"].Boolean)
        {
            WarningBox.SetActive(true);
        }

        // --- Enable events based on the actuator states ---

        /*
        if (IO_dict["a_HF_radio"].Boolean)
        {
            SpeakerAudioSourceHF.mute = false;

            if (!SpeakerAudioSourceHF.isPlaying)
            {
                SpeakerAudioSourceHF.PlayOneShot(Radio_HF);
            }
        }
        else if (IO_dict["a_HF_message"].Boolean)
        {
            SpeakerAudioSourceHF.mute = false;

            if (!SpeakerAudioSourceHF.isPlaying)
            {
                SpeakerAudioSourceHF.PlayOneShot(Message_HF);
            }
        }
        else if (IO_dict["a_HF_mute"].Boolean)
        {
            SpeakerAudioSourceHF.mute = true;
        }*/

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

        if (IO_dict["a_broadcastSection_tube"].Boolean)
        {
            ;
        }
        else if (IO_dict["a_broadcastSection_section"].Boolean)
        {
            ;
        }

        // --- Set the sensor values based on states of the components ---
        //IO_dict["s_HF_messageFinished"].Boolean = !SpeakerAudioSourceHF.isPlaying;
        IO_dict["s_broadcast_recordingFinished"].Boolean = !SpeakerAudioSourceBroadcast.isPlaying && StartedPlayingMessage;
    }
}