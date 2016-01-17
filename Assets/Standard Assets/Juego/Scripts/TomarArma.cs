using UnityEngine;
using System.Collections;

public class TomarArma : MonoBehaviour {

    public GUISkin Skin;
    public int Y;
    public int Arma;
    public string ArmaS;
    private bool guiBool;

    void Start()
    {
    }

	void OnTriggerStay (Collider other) {

        if (other.tag == "Player")
        {
            guiBool = true;
            if (Input.GetButtonDown("E"))
            {
                other.GetComponent<PlayerController>().Arma = Arma;
                Destroy(gameObject);
            }
        }

	}

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            guiBool = false;
        }
    }

    void OnGUI()
    {
        GUI.skin = Skin;

        if (guiBool && GameObject.Find("Player").GetComponent<PauseMenu>().isPaused == false)
        {
            GUI.Label(new Rect(Screen.width / 2 - Y, Screen.height / 2 + 240, 320, 64), "Presione E para tomar " + ArmaS);
        }
    }
}
