using UnityEngine;
using System.Collections;

public class VersionInGame : MonoBehaviour {

    public GUISkin Skin;

    void OnGUI()
    {
        GUI.skin = Skin;
        GUI.Label(new Rect(Screen.width - 180, 15, 180, 100), "Pre-Alpha");
    }
}
