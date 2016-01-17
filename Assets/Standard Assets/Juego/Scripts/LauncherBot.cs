using UnityEngine;
using System.Collections;

public class LauncherBot : MonoBehaviour {

	void Update () {
        transform.LookAt(GameObject.Find("Player").transform);
	}
}
