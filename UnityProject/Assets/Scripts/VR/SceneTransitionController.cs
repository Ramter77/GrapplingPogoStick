using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionController : MonoBehaviour {

    public Text YearText;
    public Text LocationText;
    public Text EventTypeText;
    private Animator anim;

    void Start () {
        anim = GetComponent<Animator>();

        StartAnimation();
    }

    private void FadeIn() {
        //color.a = Time.time - Mathf.Floor(Time.time);
    }

    public void StartAnimation(){
        Debug.Log(anim);
        GetComponent<Animator>().SetTrigger("fade");
        //Debug.Log("FADE TRANSITION");
    }

    public void FadeToBlack()
    {
        GetComponent<Animator>().SetTrigger("fadeToBlack");
        //Debug.Log("FADE To BLACK");
    }

}


