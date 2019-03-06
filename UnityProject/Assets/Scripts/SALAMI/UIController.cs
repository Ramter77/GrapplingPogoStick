using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Text objectiveText;
    private GameManager GameManager;


    public activateBubble HubrechtBubble;
    public activateBubble RadioBubble;


    void Start () {
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        RemoveObjective();
    }

    public void updateLeftCigarettes()
    {
        objectiveText.text = ("Distribute cigarettes \n around. \n Cigarettes left: " + (GameManager.leftCigarettes - 1).ToString());
    }

    public void updateLeftCakeSlices()
    {
        objectiveText.text = ("Distribute cake slices. \n Slices left: " + (GameManager.leftCakeSlices - 1).ToString());
    }

    public void AddObjective(string name) {


        if (name == "Mother") {
            GameManager.cigaretteDistributionEnabled = true;
            objectiveText.text = ("Distribute cigarettes \n around. \n Cigarettes left: " + (GameManager.leftCigarettes - 1).ToString());
        }

        else if (name == "Hubrecht Breukers")
        {
            Debug.Log("Bring Whiskey objective activated");

            GameManager.bottleDistributionEnabled = true;
            objectiveText.text = ("Get your uncle \n some whiskey");
        }

        //level2

        else if (name == "MotherSmoke")
        {
            Debug.Log("LungCancer");

            GameManager.smokingAllowed = true;
            objectiveText.text = ("Go talk to \n your aunt");
        }

        else if (name == "MotherTalk")
        {
            Debug.Log("go back to mother");

            objectiveText.text = ("Go talk to \n your mother");
        }

        else if (name == "MotherCake")
        {
            Debug.Log("Bring Cake");

            GameManager.activateCakeLocation();
            objectiveText.text = ("Bring the cake \n to the table");
        }

        else if (name == "Mathies Slagman")
        {
            Debug.Log("Talk to Mathies Slagman");
            objectiveText.text = ("Go talk to \n Mathies Slagman");
        }

        else if (name == "Radio3")
        {
            Debug.Log("Radio3");
            objectiveText.text = ("Go turn on \n the Radio");
        }

        /*
        else if (name == "MotherCakeSlice")
        {
            Debug.Log("Distribute -=---------------------------- Cake");
            //objectiveText.text = ("Disdfgsdrgtribute cake slices. \n Slices left: " + (GameManager.leftCakeSlices).ToString());


            //objectiveText.text = ("Bring the cake slices \n to the table Cigarettes left: " + (GameManager.leftCigarettes).ToString());

            //objectiveText.text = ("Bring the cake slices \n to the table");
        }
        */

        else if (name == "Radio2")
        {
            Debug.Log("Turn on radio");
            objectiveText.text = ("Turn on the radio");

            GameManager.radioInteractionEnabled = true;

            if (GameManager.level == 2 || GameManager.level == 1)
            {
                RadioBubble.gameObject.SetActive(true);
            }
            

            GameObject.FindGameObjectWithTag("Radio").GetComponent<PlaySong>().turnOffRadio();
        }


    }

    public void RemoveObjective() {
        if (GameManager.level == 1)
        {
            if (GameManager.CurrentObjective == 0)
            {
                objectiveText.text = ("Go talk to \n your mother");
                //highlight material/object
            }

            else if (GameManager.CurrentObjective == 1)
            {
                objectiveText.text = ("Go talk to \n your uncle");
                HubrechtBubble.enable();

                //highlight material/object
            }

            else if (GameManager.CurrentObjective == 2)
            {
                HubrechtBubble.disable();
                objectiveText.text = ("Turn on the radio");

                RadioBubble.enable();
                GameManager.radioInteractionEnabled = true;

                GameObject.FindGameObjectWithTag("Radio").GetComponent<PlaySong>().turnOffRadio();


                //highlight material/object
            }
            else if (GameManager.CurrentObjective == 3)
            {
                //fade to next level
                RadioBubble.disable();
            }

            else if (GameManager.CurrentObjective == 4)
            {
                //fade to next level
                Debug.Log("ENDED");
            }

            else
            {
                Debug.Log("Objective Error: its a feature");
            }
        }

        else if (GameManager.level == 2)
        {
            Debug.Log("currentObj: " + GameManager.CurrentObjective);


            if (GameManager.CurrentObjective == 0)
            {
                objectiveText.text = ("Go talk to \n your mother");
                //highlight material/object

                
            }

            else if (GameManager.CurrentObjective == 1)
            {
                //Because there is no dialog its in here
                objectiveText.text = ("Distribute cake slices \n Slices left: " + (GameManager.leftCakeSlices - 1).ToString());
            }

            else if (GameManager.CurrentObjective == 2)
            {
                objectiveText.text = ("Go talk to \n your mother again");

                
            }
        }

    }
}
