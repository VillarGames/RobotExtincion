using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public CharacterController CharCont;
    public GameObject Player;
    public Camera MainCamera;
    public GUISkin Skin;

    public Transform Bala;
    public AudioClip Metal;

    public float Vida;

    //Holder for weapons
    public Transform WalkAnimationHolder;
    public Transform JumpAnimationHolder;
    public Transform SwayHolder;
    public Transform RecoilHolder;

    public GameObject BulletHole;

    private bool canReload;
    private bool Recargando;

    //Movement booleans
    private WalkingState walkingState = WalkingState.Idle;
    private bool isAiming;
    private bool WasStanding; 

    private float VelocityMagn;
    private AudioSource Audio;

    public WeaponInfo CurrentWeapon;
    public List<WeaponInfo> WeaponList = new List<WeaponInfo>();

    private Vector3 currentRecoil1;
    private Vector3 currentRecoil2;
    private Vector3 currentRecoil3;
    private Vector3 currentRecoil4;

    public int Arma = 0;

    private float shootTime = 0;

    public string L;
    public GameObject T;

    void Update()
    {
        ShootController();

        if (Arma == 0)
        {
            CurrentWeapon = WeaponList[0];
        }

        if (Arma == 1)
        {
            CurrentWeapon = WeaponList[1];
        }
    }

    void FixedUpdate()
    {
        RecoilController();
        SwayController();
        AnimationController();
        SpeedController();
        Fall();
        ADSController();
        VelocityMagn = CharCont.velocity.magnitude;

        if (Arma == 0)
        {
            GameObject.Find(gameObject.name + "/Recoil/MainCamera/WeaponWalkAnimation/WeaponJumpAnimation/WeaponSwayHolder/WeaponRecoilHolder/AK47").SetActive(true);
            GameObject.Find(gameObject.name + "/Recoil/MainCamera/WeaponWalkAnimation/WeaponJumpAnimation/WeaponSwayHolder/WeaponRecoilHolder/M9").SetActive(false);
            Audio = CurrentWeapon.spawnPoint.GetComponent<AudioSource>();
        } 
        
        if (Arma == 1)
        {
            GameObject.Find(gameObject.name + "/Recoil/MainCamera/WeaponWalkAnimation/WeaponJumpAnimation/WeaponSwayHolder/WeaponRecoilHolder/AK47").SetActive(false);
            GameObject.Find(gameObject.name + "/Recoil/MainCamera/WeaponWalkAnimation/WeaponJumpAnimation/WeaponSwayHolder/WeaponRecoilHolder/M9").SetActive(true);
            Audio = CurrentWeapon.spawnPoint.GetComponent<AudioSource>();
        }
    }

    void SpeedController()
    {
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && VelocityMagn > 0 && isAiming == false)
        {
            if (Input.GetButton("Run"))
            {
                walkingState = WalkingState.Running;
            }
            else
            {
                walkingState = WalkingState.Walking;
            }
        }
        else
        {
            walkingState = WalkingState.Idle;
        }

    }

    void AnimationController()
    {

        if (WasStanding && !CharCont.isGrounded)
        {
            WasStanding = false;
            JumpAnimationHolder.GetComponent<Animation>().CrossFade("WeaponTakeOff");
        }
        else if (!WasStanding && CharCont.isGrounded)
        {
            WasStanding = true;
            JumpAnimationHolder.GetComponent<Animation>().CrossFade("WeaponLand");
        }
        if (isAiming == false)
        {
            if (walkingState == WalkingState.Running && CharCont.isGrounded && isAiming == false)
            {
                WalkAnimationHolder.GetComponent<Animation>().CrossFade("WeaponRun");
            }
            else if (walkingState == WalkingState.Walking && CharCont.isGrounded && isAiming == false)
            {
                WalkAnimationHolder.GetComponent<Animation>().CrossFade("WeaponWalk");
            }
            else if (walkingState == WalkingState.Idle && CharCont.isGrounded && isAiming == false)
            {
                WalkAnimationHolder.GetComponent<Animation>().CrossFade("WeaponIdle");
            }
        }
    }

    void SwayController()
    {

    }

    void ADSController()
    {
        if (Arma != -1 && !Recargando)
        {
            if (Input.GetButton("Fire2"))
            {
                isAiming = true;
                CurrentWeapon.ADSHolder.localPosition = Vector3.Lerp(CurrentWeapon.ADSHolder.localPosition, CurrentWeapon.Scopes[CurrentWeapon.currentScope].adsPosition, CurrentWeapon.VelocidadAgarre);
                WalkAnimationHolder.GetComponent<Animation>().Play("WeaponAiming");
            }
            else
            {
                isAiming = false;
                CurrentWeapon.ADSHolder.localPosition = Vector3.Lerp(CurrentWeapon.ADSHolder.localPosition, Vector3.zero, CurrentWeapon.VelocidadAgarre);
            }
        }
    }

    public enum WalkingState
    {
        Idle,
        Running,
        Walking,
        Aiming,
        Shooting
    }

    IEnumerator Recarga()
    {
        yield return new WaitForSeconds(CurrentWeapon.RecargaTime);

        CurrentWeapon.Difference = CurrentWeapon.BalasMax - CurrentWeapon.Balas;
        CurrentWeapon.TotalB -= CurrentWeapon.Difference;
        CurrentWeapon.Balas += CurrentWeapon.Difference;
        Recargando = false;
    }

    void ShootController()
    {
        if (Arma != -1)
        {
            if (Input.GetButton("Fire1") && CurrentWeapon.Balas > 0 && Recargando == false)
            {
                if (shootTime <= Time.time)
                {
                    walkingState = WalkingState.Shooting;
                    shootTime = Time.time + CurrentWeapon.fireRate;
                    currentRecoil1 += new Vector3(CurrentWeapon.recoilRotation.x, Random.Range(-CurrentWeapon.recoilRotation.y, CurrentWeapon.recoilRotation.y));
                    currentRecoil3 += new Vector3(Random.Range(-CurrentWeapon.recoilKickBack.x, CurrentWeapon.recoilKickBack.x), Random.Range(-CurrentWeapon.recoilKickBack.y, CurrentWeapon.recoilKickBack.y), CurrentWeapon.recoilKickBack.z);

                    Audio.clip = CurrentWeapon.disparoFX;
                    Audio.Play();

                    CurrentWeapon.weaponTransform.GetComponent<Animation>().Play(CurrentWeapon.ShotAnimGun);

                    CurrentWeapon.Balas -= 1;
                    canReload = false;

                    RaycastHit Hit;
                    if (Physics.Raycast(CurrentWeapon.spawnPoint.position, CurrentWeapon.spawnPoint.TransformDirection(Vector3.forward), out Hit, 250))
                    {
                        Hit.transform.SendMessageUpwards("GetBulletDamage", CurrentWeapon.Name, SendMessageOptions.DontRequireReceiver);

                        if (Hit.transform && Hit.collider.tag != "Enemy" && Hit.collider.tag != "ArmaTrigger" && Hit.collider.tag != "AscensorInt" && Hit.collider.tag != "NoBullet")
                        {
                            Instantiate(BulletHole, Hit.point, Quaternion.FromToRotation(Vector3.up, Hit.normal));
                        }

                        if (Hit.collider.tag == "Enemy")
                        {
                            Hit.transform.gameObject.GetComponent<EnemyDead>().Health -= CurrentWeapon.Damage;
                            GameObject Bullet;
                            Bullet = (GameObject) Instantiate(BulletHole, Hit.point, Quaternion.FromToRotation(Vector3.up, Hit.normal));
                            Bullet.transform.parent = Hit.transform;
                            Hit.transform.gameObject.GetComponent<AudioSource>().clip = Metal;
                            Hit.transform.gameObject.GetComponent<AudioSource>().Play();
                        }

                        if (Hit.collider.tag == "AscensorInt")
                        {
                            Hit.transform.gameObject.GetComponent<Ascensor>().Activado = true;
                        }
                    }

                }
            }
            else if (Input.GetButtonUp("Fire1") && Recargando == false)
            {
                canReload = true;
            }
        }

        if (Input.GetButton("Reload") && canReload == true && Recargando == false && CurrentWeapon.TotalB > 0)
        {
            canReload = false;
            Recargando = true;

            Audio.clip = CurrentWeapon.recargaFX;
            Audio.Play();

            CurrentWeapon.Manos.GetComponent<Animation>().Play(CurrentWeapon.ReloadAnimHand);
            CurrentWeapon.weaponTransform.GetComponent<Animation>().Play(CurrentWeapon.ReloadAnimGun);

            StartCoroutine(Recarga());

            
        }

    }

    void RecoilController()
    {
        currentRecoil1 = Vector3.Lerp(currentRecoil1, Vector3.zero, 0.2f);
        currentRecoil2 = Vector3.Lerp(currentRecoil2, currentRecoil1, 0.1f);
        currentRecoil3 = Vector3.Lerp(currentRecoil3, Vector3.zero, 0.1f);
        currentRecoil4 = Vector3.Lerp(currentRecoil4, currentRecoil3, 0.1f);

        RecoilHolder.localEulerAngles = currentRecoil2;
        RecoilHolder.localPosition = currentRecoil4;
    }

    void OnGUI()
    {
        if (Arma != -1)
        {
            GUI.skin = Skin;
            GUI.Label(new Rect(25, Screen.height - 100, 500, 40), "  " + CurrentWeapon.Balas);
            GUI.Label(new Rect(25, Screen.height - 50, 500, 40), "  " + CurrentWeapon.TotalB);
        }
    }

    void Fall()
    {
        
    }


    public int BulletHoleLayer { get; set; }
}

[System.Serializable]
public class WeaponInfo
{

    public string Name;
    public float fireRate;
    public Transform weaponTransform;
    public GameObject Manos;
    public Transform ADSHolder;
    public AudioClip disparoFX;
    public AudioClip recargaFX;
    public float RecargaTime;
    public string ReloadAnimHand;
    public string ReloadAnimGun;
    public string ShotAnimGun;

    public Vector3 recoilRotation;
    public Vector3 recoilKickBack;

    public Transform spawnPoint;

    public float VelocidadAgarre;

    public int Damage;

    public int BalasMax;
    public int Balas;
    public int TotalB;
    public int Difference;

    public int currentScope;
    public List<WeaponScope> Scopes = new List<WeaponScope>();

}

[System.Serializable]
public class WeaponScope
{
    public string Name;
    public Vector3 adsPosition;
}