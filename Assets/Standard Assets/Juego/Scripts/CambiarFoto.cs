using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CambiarFoto : MonoBehaviour {

    public string url = "http://images.earthcam.com/ec_metros/ourcams/fridays.jpg";
    public string Sizeamos;
    IEnumerator Start()
    {
        WWW www = new WWW(url);
        yield return www;
        gameObject.GetComponent<RawImage>().texture = www.texture;

        if (www.isDone)
        {
            gameObject.GetComponent<RawImage>().color = new Color(255, 255, 255, 1);
        }

    }

    void Update()
    {
        
    }

}
