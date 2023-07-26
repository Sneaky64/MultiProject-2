using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SetColour : MonoBehaviour
{
    public Material playerMat;
    public Material defaultMaterial;

    [SerializeField] Color colour;
    float intensityCore = 20f;
    float intensityFace = 20f;

    bool faceGlowing = true;
    bool coreGlowing = true;

    private void Awake()
    {
        playerMat.SetColor("_PlayerColour", defaultMaterial.GetColor("_PlayerColour"));
        playerMat.SetColor("_CoreColour", defaultMaterial.GetColor("_CoreColour") * intensityCore);
        playerMat.SetColor("_FaceColour", defaultMaterial.GetColor("_FaceColour") * intensityFace);
    }
    public void SetPlayerColour()
    {
        playerMat.SetColor("_PlayerColour", colour);
    }
    public void SetCoreColour()
    {
        playerMat.SetColor("_CoreColour", colour * intensityCore);
    }
    public void SetFaceColour()
    {
        playerMat.SetColor("_FaceColour", colour * intensityFace);
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
        SetFaceColour();
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
        SetCoreColour();
    }
}
