using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseMenu : MonoBehaviour {

    public GUISkin Skin;
    public bool isPaused;

    private GameObject Camara;

    private bool guiAnim;
    private bool guiAnim2;
    private bool guiMP;
    private bool guiOp;
    private bool guiSave;
    private bool guiExit;

    private bool VignetteCam;
    private bool MotionBlurCam;
    private bool SunShaftsCam;
    private bool FisheyeCam;
    private bool SSAOCam;

    private float guiX = 80f;

    void Start()
    {
        Cursor.visible = false;
        isPaused = false;
        AudioListener.pause = false;
        AudioListener.volume = 1; 
        Camara = GameObject.Find("Player/Recoil/MainCamera");

        VignetteCam = Camara.GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>().enabled;
        MotionBlurCam = Camara.GetComponent<UnityStandardAssets.ImageEffects.CameraMotionBlur>().enabled;
        SunShaftsCam = Camara.GetComponent<UnityStandardAssets.ImageEffects.SunShafts>().enabled;
        FisheyeCam = Camara.GetComponent<UnityStandardAssets.ImageEffects.Fisheye>().enabled;
        SSAOCam = Camara.GetComponent<UnityStandardAssets.ImageEffects.ScreenSpaceAmbientOcclusion>().enabled;
    }
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cambio();
        }

        if (guiAnim)
        {
            guiX = guiX + 50;
            if (guiX >= 580)
            {
                guiAnim = false;
            }
        }

        if (guiAnim2)
        {
            guiX = guiX - 50;
            if (guiX == 80)
            {
                guiAnim2 = false;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //Vignette
        if (VignetteCam == true)
        {
            Camara.GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>().enabled = true;
        }
        else
        {
            Camara.GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>().enabled = false;
        }

        //MotionBlurCam
        if (MotionBlurCam == true)
        {
            Camara.GetComponent<UnityStandardAssets.ImageEffects.CameraMotionBlur>().enabled = true;
        }
        else
        {
            Camara.GetComponent<UnityStandardAssets.ImageEffects.CameraMotionBlur>().enabled = false;
        }

        //SunShafts
        if (SunShaftsCam == true)
        {
            Camara.GetComponent<UnityStandardAssets.ImageEffects.SunShafts>().enabled = true;
        }
        else
        {
            Camara.GetComponent<UnityStandardAssets.ImageEffects.SunShafts>().enabled = false;
        }

        //Fisheye
        if (FisheyeCam == true)
        {
            Camara.GetComponent<UnityStandardAssets.ImageEffects.Fisheye>().enabled = true;
        }
        else
        {
            Camara.GetComponent<UnityStandardAssets.ImageEffects.Fisheye>().enabled = false;
        }

        //SSAO
        if (SSAOCam == true)
        {
            Camara.GetComponent<UnityStandardAssets.ImageEffects.ScreenSpaceAmbientOcclusion>().enabled = true;
        }
        else
        {
            Camara.GetComponent<UnityStandardAssets.ImageEffects.ScreenSpaceAmbientOcclusion>().enabled = false;
        }

	}

    void Cambio()
    {
        if (Time.timeScale == 1)
            Pausear();

        else if (Time.timeScale == 0)
            Continuar();
    }

    void Pausear()
    {
        isPaused = true;
        Time.timeScale = 0;
        GameObject.Find("Player").GetComponent<FirstPersonController>().enabled = false;
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
        GameObject.Find("Player").GetComponent<Crosshair>().enabled = false;
        Camara.GetComponent<UnityStandardAssets.ImageEffects.Blur>().enabled = true;
        Cursor.visible = true;
        AudioListener.pause = true;
    }

    void Continuar()
    {
        isPaused = false;
        Time.timeScale = 1;
        GameObject.Find("Player").GetComponent<FirstPersonController>().enabled = true;
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
        GameObject.Find("Player").GetComponent<Crosshair>().enabled = true;
        Camara.GetComponent<UnityStandardAssets.ImageEffects.Blur>().enabled = false;
        Cursor.visible = false;
        AudioListener.pause = false;
    }

    void OnGUI()
    {
        GUI.skin = Skin;

        if (isPaused == true)
        {
            GUI.Box(new Rect(Screen.width / 2 - 600, Screen.height / 2 - 250, 1200, 500), "");

            if (GUI.Button(new Rect(Screen.width / 2 - guiX, Screen.height / 2 - 120, 160, 40), "Continuar"))
            {
                Continuar();
            }

            if (GUI.Button(new Rect(Screen.width / 2 - guiX, Screen.height / 2 - 40, 160, 40), "Opciones") && guiX == 80)
            {
                guiAnim = true;
                guiOp = true;
            }

            if (GUI.Button(new Rect(Screen.width / 2 - guiX, Screen.height / 2 + 40, 160, 40), "Guardar") && guiX == 80)
            {
                guiAnim = true;
                guiSave = true;
            }

            if (GUI.Button(new Rect(Screen.width / 2 - guiX, Screen.height / 2 + 120, 160, 40), "Salir") && guiX == 80)
            {
                guiAnim = true;
                guiMP = true;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (guiOp == true)
            {

                VignetteCam = GUI.Toggle(new Rect(Screen.width / 2 - 240, Screen.height / 2 - 100, 160, 40), VignetteCam, "Vignette");
                MotionBlurCam = GUI.Toggle(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 100, 180, 40), MotionBlurCam, "Motion Blur");
                SunShaftsCam = GUI.Toggle(new Rect(Screen.width / 2 - 240, Screen.height / 2 - 40, 160, 40), SunShaftsCam, "Sun Shafts");
                FisheyeCam = GUI.Toggle(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 40, 160, 40), FisheyeCam, "Fisheye");
                SSAOCam = GUI.Toggle(new Rect(Screen.width / 2 - 240, Screen.height / 2 + 20, 160, 40), SSAOCam, "SSAO");

                //Media
                if (QualitySettings.GetQualityLevel() == 0 && GUI.Button(new Rect(Screen.width / 2 + 240, Screen.height / 2 - 100, 120, 40), "Baja"))
                {
                    QualitySettings.SetQualityLevel(1, true);
                    VignetteCam = true;
                    MotionBlurCam = false;
                    SunShaftsCam = true;
                    FisheyeCam = true;
                    SSAOCam = false;
                }

                //Alta
                if (QualitySettings.GetQualityLevel() == 1 && GUI.Button(new Rect(Screen.width / 2 + 240, Screen.height / 2 - 100, 120, 40), "Media"))
                {
                    QualitySettings.SetQualityLevel(2, true);
                    VignetteCam = true;
                    MotionBlurCam = true;
                    SunShaftsCam = true;
                    FisheyeCam = true;
                    SSAOCam = true;
                }

                //Baja
                if (QualitySettings.GetQualityLevel() == 2 && GUI.Button(new Rect(Screen.width / 2 + 240, Screen.height / 2 - 100, 120, 40), "Alta"))
                {
                    QualitySettings.SetQualityLevel(0, true);
                    VignetteCam = false;
                    MotionBlurCam = false;
                    SunShaftsCam = false;
                    FisheyeCam = false;
                    SSAOCam = false;
                }

                if (GUI.Button(new Rect(Screen.width / 2 - 160, Screen.height / 2 + 120, 160, 40), "Atras"))
                {
                    guiAnim2 = true;
                    guiOp = false;
                }
            }

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (guiMP)
            {

                GUI.Label(new Rect(Screen.width / 2 - 80, Screen.height / 2 - 40, 320, 40), "¿Seguro que quiere salir?");

                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 0, 160, 40), "Si"))
                {
                    Continuar();
                    Cursor.visible = true;
                    Application.LoadLevel(1);
                }
                if (GUI.Button(new Rect(Screen.width / 2 + 80, Screen.height / 2 - 0, 160, 40), "No"))
                {
                    guiAnim2 = true;
                    guiMP = false;
                }
            }
         }
    }
}
