using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.component;
using u040.prespective.prelogic.signal;
using UnityEngine;

public class Output_Bool_Logic: PreLogicComponent
{
#if UNITY_EDITOR || UNITY_EDITOR_BETA
    [HideInInspector] public int toolbarTab;
#endif

    [Header("Logic and I/O")]

        [Tooltip("BooleanToggle component for the in or output")]
        public BoolToggle BooleanToggle;
        [Tooltip("Input Boolean (used for input to the PLC)")]
        public bool OutputBool;

    #region <<PLC Signals>>
    #region <<Signal Definitions>>
    /// <summary>
    /// Declare the IO signals
    /// </summary>
    /// 

    public override List<SignalDefinition> SignalDefinitions
    {
        get
        {
            return new List<SignalDefinition>
            {
                new SignalDefinition(gameObject.name, PLCSignalDirection.OUTPUT, SupportedSignalType.BOOL, "", "Value", onSignalChanged, null, false),
            };
        }
    }
    #endregion
    #region <<PLC Outputs>>
    /// <summary>
    /// General callback for the IOs
    /// </summary>
    /// <param name="_signal">the signal that has changed</param>
    /// <param name="_newValue">the new value</param>
    /// <param name="_newValueReceived">the time of the value change</param>
    /// <param name="_oldValue">the old value</param>
    /// <param name="_oldValueReceived">the time of the old value change</param>
    void onSignalChanged(SignalInstance _signal, object _newValue, DateTime _newValueReceived, object _oldValue, DateTime _oldValueReceived)
    {
        if (_signal.definition.defaultSignalName == gameObject.name) 
        {
            OutputBool = (bool)_newValue;
            if (BooleanToggle.Boolean != OutputBool)
            {
                BooleanToggle.Boolean = OutputBool;
            }
        }
        else 
        {
            Debug.LogWarning("Unknown Signal received:" + _signal.definition.defaultSignalName);
        }
    }
    #endregion
}
#endregion
