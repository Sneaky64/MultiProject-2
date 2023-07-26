using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetColour : MonoBehaviour
{
    public Material playerMat;
    public Material defaultMaterial;

    [SerializeField] Color colour;
    float intensityCore = 20f;
    float intensityFace = 20f;

    bool faceGlowing = true;
    bool coreGlowing = true;

    public bool activeFace;
    public bool activeBody;
    public bool activeCore;

    public Toggle faceToggle;
    public Toggle coreToggle;
    public Toggle bodyToggle;

    private void Awake()
    {
        playerMat.SetColor("_PlayerColour", defaultMaterial.GetColor("_PlayerColour"));
        playerMat.SetColor("_CoreColour", defaultMaterial.GetColor("_CoreColour") * intensityCore);
        playerMat.SetColor("_FaceColour", defaultMaterial.GetColor("_FaceColour") * intensityFace);
    }
    private void Update()
    {
        activeFace = faceToggle.isOn;
        activeBody = bodyToggle.isOn;
        activeCore = coreToggle.isOn;

        if(activeFace)
            playerMat.SetColor("_FaceColour", colour * intensityFace);
        if(activeCore)
            playerMat.SetColor("_CoreColour", colour * intensityCore);
        if(activeBody)
            playerMat.SetColor("_PlayerColour", colour);
    }
    public void SetPlayerColour(bool active_)
    {
        activeBody = active_;
    }
    public void SetCoreColour(bool active_)
    {
        activeCore = active_;
    }
    public void SetFaceColour(bool active_)
    {
        activeFace = active_;
    }

    public void ToggleFaceGlow()
    {
        faceGlowing = !faceGlowing;
        if (faceGlowing)
        {
            intensityFace = 20f;
        }
        else
        {
            intensityFace = 1f;
        }
    }

    public void ToggleCoreGlow()
    {
        coreGlowing = !coreGlowing;
        if (coreGlowing)
        {
            intensityCore = 20f;
        }
        else
        {
            intensityCore = 1f;
        }
    }
}
