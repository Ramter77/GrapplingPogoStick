using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHelper : MonoBehaviour {

    public Transform[] options;

    public GameObject mother2Option;

	void Start () {    
        if (GameManager.level == 2)
        {
            if (transform.parent.name == "Mother2" )
            {
                options[0] = transform.GetChild(0).GetChild(0).gameObject.transform;
                options[1] = transform.GetChild(0).GetChild(1).gameObject.transform;

                options[1].gameObject.SetActive(false);
            }  
            else
            {
                deactivateChildren();
            }
        }
        else
        {
            deactivateChildren();
        }
        
    }

    private void deactivateChildren()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i] = transform.GetChild(0).GetChild(i).gameObject.transform;
            options[i].gameObject.SetActive(false);
        }
    }

    public void Helper(int j) {
        //Debug.Log(options[j]);

        //transform.GetChild(0).GetChild(j).gameObject.SetActive(true);

        options[j].gameObject.SetActive(true);
    }


    public void Mother2()
    {
        Debug.Log("Option1: " +options[0]);
        Debug.Log(options[1]);
        options[1].gameObject.SetActive(true);
    }
}
