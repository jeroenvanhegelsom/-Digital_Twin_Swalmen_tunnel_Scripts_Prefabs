using System;
using System.Collections.Generic;
using u040.prespective.prelogic;
using u040.prespective.prelogic.component;
using u040.prespective.prelogic.signal;
using UnityEngine;

public class Input_Bool_Logic : PreLogicComponent
{
    #if UNITY_EDITOR || UNITY_EDITOR_BETA
        [HideInInspector] public int toolbarTab;
    #endif

    [Header("Logic and I/O")]

        [Tooltip("BooleanToggle component for the in or output")]
        public BoolToggle BooleanToggle;
        [Tooltip("Input Boolean (used for input to the PLC)")]
        public bool InputBool;

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
                new SignalDefinition(gameObject.name, PLCSignalDirection.INPUT, SupportedSignalType.BOOL, "", gameObject.name, null, null, false),
            };
        }
    }
    #endregion
    #endregion

    #region <<Update>>
    /// <summary>
    /// update the simulation component
    /// </summary>
    /// <param name="_simFrame">the current frame since start</param>
    /// <param name="_deltaTime">the time since last frame</param>
    /// <param name="_totalSimRunTime">total run time of the simulation</param>
    /// <param name="_simStart">the time the simulation started</param>
    protected override void onSimulatorUpdated(int _simFrame, float _deltaTime, float _totalSimRunTime, DateTime _simStart)
    {
        readComponent();
    }
    void readComponent()
    {
        if (BooleanToggle.Boolean != InputBool)
        {
            InputBool = BooleanToggle.Boolean;
            WriteValue(gameObject.name, InputBool);
        }
    }
}
#endregion