using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.EventSystems;
public class RebindScript : MonoBehaviour
{
    [SerializeField] private InputActionReference actionToRemap;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] InputActionRebindingExtensions.RebindingOperation rebindingOperation;



    private void Awake()
    {
        ResetButtonText();
    }

    void ResetButtonText()
    {
        buttonText.text = InputControlPath.ToHumanReadableString(actionToRemap.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
    }

    public void StartRebinding()
    {
        buttonText.text = "PRESS ANY KEY";
        
        rebindingOperation = actionToRemap.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete())
            .Start();

    }
    
    private void RebindComplete()
    {
        rebindingOperation.Dispose();
        ResetButtonText();
    }
}
