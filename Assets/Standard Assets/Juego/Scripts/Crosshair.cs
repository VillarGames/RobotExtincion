using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

    public Texture2D Mira;
    public float W;
    public float H;

    void OnGUI()
    {
        W = Mira.width;
        H = Mira.height;

        if (!Input.GetButton("Fire2"))
        {
            GUI.DrawTexture(new Rect((Screen.width - W) / 2, (Screen.height - H) / 2, W, H), Mira, ScaleMode.ScaleToFit);
        }
    }
}
