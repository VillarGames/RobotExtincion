using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuEspacioComenzar : MonoBehaviour {

    private float Alfa;
    private float Alfa2;
    private float Volumen = 1;
    private bool Repite;
    private bool repiteCancion;
    private bool Jugar;
    private AudioSource audio;

    public GameObject Fin;

	// Use this for initialization
	void Start () {

        Jugar = false;
        Volumen = 1;
        repiteCancion = false;
        StartCoroutine(Cancion());

	}

    void Update()
    {
        if (Alfa >= 0 && Repite == false)
        {
            Alfa = Alfa - 1 * Time.deltaTime;
        }

        if (Alfa <= 0 && Repite == false && Jugar == false)
        {
            Repite = true;
            Alfa = 0;
        }

        if (Alfa >= 0 && Repite == true && Jugar == false)
        {
            Alfa = Alfa + 1 * Time.deltaTime;
        }

        if (Alfa >= 1 && Repite == true && Jugar == false)
        {
            Repite = false;
            Alfa = 1;
        }

        gameObject.GetComponent<Text>().color = new Color(255, 255, 255, Alfa);
        Fin.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, Alfa2);

        if (repiteCancion == true)
        {
            audio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
            audio.Play();
            StartCoroutine(Cancion());
            repiteCancion = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jugar = true;
            Fin.SetActive(true);
            
        }

        if (Jugar == true)
        {
            Alfa2 = Alfa2 + 0.001f;
            Volumen = Volumen - 0.001f;
            if (Alfa2 >= 1)
            {
                StartCoroutine(Final());
            }
        }

        AudioListener.volume = Volumen;
    }

    IEnumerator Final()
    {
        yield return new WaitForSeconds(2);

        Application.LoadLevel(2);

    }

    IEnumerator Cancion()
    {
        yield return new WaitForSeconds(100);

        repiteCancion = true;

    }
}
