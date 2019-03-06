using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySong : MonoBehaviour {

    public GameManager GameManager;
    public AudioSource RadioSoundData;
    public bool isRadioPlaying = false;
    public bool RadioActive = false;

    void Start() {        
        if (GameManager == null)
        {
            GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
        RadioSoundData = GetComponent<AudioSource>();
    }

    public void RadioToggle() {
        if (GameManager.radioInteractionEnabled && isRadioPlaying == false)
        {
            RadioActive = false;

            isRadioPlaying = false;
            GameManager.incrementRadio();

            Debug.Log("End of level " + GameManager.level.ToString());
        }


        /*
        if (RadioActive == true && isRadioPlaying == false)
        {
            RadioActive = false;
            isRadioPlaying = true;
            GameManager.incrementRadio();

            Debug.Log("End of level 1");
        }
        */

        if (RadioActive == false)
        {
            if (isRadioPlaying == false)
            {
                RadioSoundData.Play();
                isRadioPlaying = true;
            }
            else if (isRadioPlaying == true)
            {
                turnOffRadio();
            }
        }
    }

    public void turnOffRadio()
    {
        RadioSoundData.Stop();
        isRadioPlaying = false;
    }
}
