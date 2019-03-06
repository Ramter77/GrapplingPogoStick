using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateChatButton : MonoBehaviour {

    public void deactivateChatButton() {
        gameObject.SetActive(false);
    }

    public void activateChatButton()
    {
        gameObject.SetActive(true);
    }
}
