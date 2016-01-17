using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {
	
	// Update is called once per frame
    void Start()
    {
        StartCoroutine(Fin());
    }

    IEnumerator Fin()
    {
        yield return new WaitForSeconds(5);

        gameObject.GetComponent<MeshCollider>().enabled = false;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 5, gameObject.transform.position.z), 0.05f);
        Destroy(gameObject, 2);
    }
}
