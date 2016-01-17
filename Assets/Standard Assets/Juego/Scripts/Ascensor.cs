using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ascensor : MonoBehaviour {

    public bool Activado;
    public Transform m_Ascensor;

	void Update () {

        if (Activado == true)
        {
            m_Ascensor.GetComponent<Animation>().Play("AscensorTest");
            StartCoroutine(Reinicio());            
        }

	}

    IEnumerator Reinicio()
    {
        yield return new WaitForSeconds(4);

        Activado = false;
        m_Ascensor.GetComponent<Animation>().Stop();
    }
}
