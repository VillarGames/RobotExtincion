using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VideoIntro : MonoBehaviour {

    public MovieTexture movie;
    public int Duracion;
    public int escena;
    private AudioSource audio;

	// Use this for initialization
	void Start () {
        GetComponent<RawImage>().texture = movie as MovieTexture;
        audio = GetComponent<AudioSource>();
        audio.clip = movie.audioClip;
        audio.Play();
        movie.Play();
        StartCoroutine(Final());
	}

    IEnumerator Final()
    {
        yield return new WaitForSeconds(Duracion);

        Application.LoadLevel(escena);
    }
}
