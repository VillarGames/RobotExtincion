using UnityEngine;
using System.Collections;

public class AlphaEnemy : MonoBehaviour {

    public int Vida;
    public GameObject destructedPiece;

	void Update () {

        if (Vida <= 0)
        {
            Instantiate(destructedPiece, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }

	}
}
