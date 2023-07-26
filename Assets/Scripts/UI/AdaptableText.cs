using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class AdaptableText : MonoBehaviour
{
    public string messageStart, messageEnd;

    public InputActionReference input;

    public TMP_Text text;

    private void Start()
    {
        string keybind = input.action.bindings[0].ToString().Replace("Restart:<Keyboard>/", "");
        text.text = messageStart + "<color=red>" + keybind + "</color>" + messageEnd;
    }
}
