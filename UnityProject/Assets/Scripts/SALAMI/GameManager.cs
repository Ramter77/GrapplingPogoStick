using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int levelNumber;
    public static int level;

    //public AudioListener audioListen;

    public static bool Oculus;  //change this

    public int CurrentObjective = 0;  //level1: cigs, whiskey, radio
    public bool isCompleted = false;

    //audio
    private bool audioToggle = true;
    public RawImage audioOn;
    public RawImage audioOff;

    private PlaySong radioScript;

    private AudioSource sound;
    public AudioSource radioSong;
    private float radioSongCounter;

    //UI controller script
    private UIController UIController;

    [Header("Tassio Booleans")]
    [Tooltip("How many cigarette have to be distributed")]
    public int leftCigarettes;
    public static bool cigaretteDistributionEnabled = false;
    [Tooltip("How many cigarette have been distributed")]
    public int distributedCigarettes;

    public BoxCollider hubrechtCollider;

    public static bool bottleDistributionEnabled = false;
    [Tooltip("How many bottles have been distributed")]
    public int distributedBottles;

    public bool radioInteractionEnabled = false;

    //level2
    public static bool smokingAllowed = false;

    //cake
    public GameObject cakeLocation;
    public static bool cakeTalk = false;
    public static bool activateCake = false;
    public int leftCakeSlices;
    public static bool cakeDistributionEnabled = false;    
    public int distributedCakeSlices;

    public GameObject motherBubble;
    public BoxCollider motherCollider;
    public static bool radioTalk = false;


    public SceneTransitionController sceneTransitionController;

    public void activateCakeLocation()
    {
        cakeLocation.SetActive(true);
        activateCake = true;
    }

    private void Awake()
    {
        level = levelNumber;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneManager.LoadScene(3);
        }

        //Skip objective
        else if (Input.GetKeyDown(KeyCode.K))
        {
            skipObjective();
        }

        //SlowMo
        else if (Input.GetKeyDown(KeyCode.L))
        {
            if (Time.timeScale == 1.0f)
                Time.timeScale = 0.2f;
            else
                Time.timeScale = 1.0f;
            // Adjust fixed delta time according to timescale
            // The fixed delta time will now be 0.02 frames per real-time second
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void skipObjective()
    {
        //test
        CompleteObjective();
        UIController.RemoveObjective();
    }

    public void incrementCigarette()
    {
        distributedCigarettes++;
        leftCigarettes--;
        UIController.updateLeftCigarettes();

        if (distributedCigarettes > leftCigarettes)
        {
            //completed objective
            CompleteObjective();
            UIController.RemoveObjective();

            cigaretteDistributionEnabled = false;

            hubrechtCollider.enabled = true;
        }
    }

    public void incrementBottle()
    {
        distributedBottles++;
        if (distributedBottles > 0)
        {
            //completed objective
            CompleteObjective();
            UIController.RemoveObjective();

            bottleDistributionEnabled = false;            
        }
    }    

    public void incrementRadio()
    {
        CompleteObjective();
        UIController.RemoveObjective();

        radioInteractionEnabled = false;

        sceneTransitionController = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<SceneTransitionController>();
        StartCoroutine(increasePitchandFade(1f));
        //sceneTransitionController.FadeToBlack();
    }

    public void incrementCake()
    {
        CompleteObjective();
        UIController.RemoveObjective();

        activateCake = false;
        cakeDistributionEnabled = true;
    }

    public void incrementCakeSlices()
    {
        distributedCakeSlices++;
        leftCakeSlices--;
        UIController.updateLeftCakeSlices();

        if (distributedCakeSlices > leftCakeSlices)
        {
            //completed objective
            CompleteObjective();
            UIController.RemoveObjective();

            cakeDistributionEnabled = false;

            motherBubble.SetActive(true);
            motherCollider.enabled = true;

            radioTalk = true;
        }
    }

    private IEnumerator increasePitchandFade(float seconds) {        
        //if increased pitch 30 times: fadeToBlack
        while (radioSongCounter < 20)
        {
            //Fade delay
            yield return new WaitForSeconds(0.5f);
            incPitch();
            //StartCoroutine(increasePitch(0.1f));
        }

        //StopAllCoroutines();
        sceneTransitionController.FadeToBlack();


        Debug.Log("next scene");
        StartCoroutine(loadScene(10.5f));
        
    }

    private IEnumerator loadScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(GameManager.level);
    }

    private void incPitch()
    {
        radioSongCounter++;
        radioSong.pitch += 0.02f;
    }

    void Start()
    {
        //FADE IN SCREEN
        if (sceneTransitionController == null)
        {

            sceneTransitionController = GameObject.FindGameObjectWithTag("SceneTransition").GetComponent<SceneTransitionController>();
            sceneTransitionController.StartAnimation();
        }

        if (UIController == null)
        {
            UIController = GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>();
        }

        audioOn = GameObject.FindGameObjectWithTag("audioOn").GetComponent<RawImage>();
        audioOff = GameObject.FindGameObjectWithTag("audioOff").GetComponent<RawImage>();

        if (radioScript == null && (level != 3 || level != 4))
        {
            radioScript = GameObject.FindGameObjectWithTag("Radio").GetComponent<PlaySong>();
        }

        sound = GetComponent<AudioSource>();

        if (level == 2)
        {
            GameObject cakeLoc = GameObject.FindGameObjectWithTag("cakeLocation");
            cakeLoc.SetActive(false);
        }

        if (level == 3)
        {
            UIController.AddObjective("Mathies Slagman");
        }

        //audioListen = audioListen.GetComponent<AudioListener>();
    }

    public void CompleteObjective()
    {
        CurrentObjective++;

        Debug.Log("Current objective: " + CurrentObjective);
        //Play completed sound
        sound.PlayOneShot(sound.clip);
    }

    public void muteAudio() {

        audioToggle = !audioToggle;
        if (audioToggle) {
            AudioListener.volume = 0f;
            audioOn.enabled = false;
            audioOff.enabled = true;
        }

        if (!audioToggle)
        {
            AudioListener.volume = 1f;
            audioOn.enabled = true;
            audioOff.enabled = false;
        }


    }
}