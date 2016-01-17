using UnityEngine;
using System.Collections;

public class EnemyDead : MonoBehaviour {

    public int Health;
    public GameObject Enemigo;
    
    public bool Alerta;
    public bool Camina;
    public bool Dispara;

    public float Velocidad;
    public GameObject Objetivo;
    private GameObject Player;

    private bool comenzaCaminar;
    private bool terminarCaminar;

    private Vector3 lastPosition = Vector3.zero;
    private float velocidad;

    public float distanciaPlayer;

    void Start()
    {
        Objetivo = GameObject.Find("Waypoint1");
        Player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        velocidad = (((transform.position - lastPosition).magnitude) / Time.deltaTime);
        lastPosition = transform.position;
    }

	void Update () {

        distanciaPlayer = Vector3.Distance(gameObject.transform.position, Player.transform.position);

        if (Health <= 0)
        {
            Destroy(Enemigo);
        }

        if (Health < 100)
        {
            Alerta = true;
        }

        if (Dispara)
        {
            Alerta = false;
            Camina = false;
            GetComponent<Animator>().SetBool("Alerta", false);
            GetComponent<Animator>().SetFloat("Speed", 0);
            GetComponent<Animator>().SetBool("Dispara", true);
            GetComponent<NavMeshAgent>().Stop();
            Disparar();
            transform.LookAt(Player.transform);
            Vector3 eulerAngles = transform.rotation.eulerAngles;
            eulerAngles.x = 0;
            eulerAngles.z = 0;
            transform.rotation = Quaternion.Euler(eulerAngles);
        }

        if (Alerta)
        {
            GetComponent<Animator>().SetBool("Alerta", true);
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * Velocidad);
            terminarCaminar = true;
            Camina = false;
            GetComponent<NavMeshAgent>().SetDestination (Player.transform.position);
            if (velocidad < 1)
            {
                GetComponent<Animator>().SetFloat("Speed", 0);
            }
            else
            {
                GetComponent<Animator>().SetFloat("Speed", 1);
            }
            if (distanciaPlayer <= 5)
            {
                Dispara = true;
                Disparar();
            }
        }

        if (Camina)
        {
            GetComponent<Animator>().SetFloat("Speed", 1);
            transform.LookAt(Objetivo.transform);
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * Velocidad);

            if (comenzaCaminar == false)
            {
                Velocidad = 0;
                comenzaCaminar = true;
            }

            if (comenzaCaminar == true && Velocidad < 2)
            {
                Velocidad = Velocidad + 1 * Time.deltaTime;
            }  
            
        }
        else
        {
            //GetComponent<Animator>().SetFloat("Speed", 0);

            if (terminarCaminar == false)
            {
                Velocidad = 2;
                terminarCaminar = true;
            }

            if (comenzaCaminar == true && Velocidad > 0)
            {
                Velocidad = Velocidad - 1f * Time.deltaTime;
                comenzaCaminar = false;
            } 
            
        }

	}

    void Disparar()
    {
        Debug.DrawRay(GameObject.Find("LauncherBot").transform.position, Vector3.right);
        RaycastHit hit;
        if(Physics.Raycast(GameObject.Find("LauncherBot").transform.position, Vector3.forward, out hit, 500))
        {
            if(hit.transform.tag == "Player")
            {
                hit.transform.GetComponent<PlayerController>().Vida -= 0.25f;
            }
        }
    }
}
