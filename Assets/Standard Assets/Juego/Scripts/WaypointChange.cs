using UnityEngine;
using System.Collections;

public class WaypointChange : MonoBehaviour {

    public GameObject siguiente_way;

	void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyDead>().Objetivo = siguiente_way;
        }
    }
}
