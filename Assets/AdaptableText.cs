using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdaptableText : MonoBehaviour
{
    public string messageStart, messageEnd;

    private MasterInput input;

    public TMP_Text text;

    private void Awake()
    {
        input = new MasterInput();
    }
    private void Start()
    {
        string keybind = input.InGame.Restart.bindings[0].ToString().Replace("Restart:<Keyboard>/", "");
        text.text = messageStart + "<color=red>" + keybind + "</color>" + messageEnd;
    }
}
