using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {
	public Text timerLabel;
	public float maxTimer = 60f;
    private float timer = 60f;
    public bool stopTimer;
    public bool ended;

    // Use this for initialization
    void Awake () {
		ended = false;
		stopTimer = false;
		timer = maxTimer;
	}
	
	// Update is called once per frame
	void Update () {
		if (!ended) {
			if (!stopTimer) {
				timer -= Time.deltaTime;
				if ( timer <= 0 )
				{
					//GameOver();
					ended = true;
					Debug.Log("LOST");
				}
				else
				{
					//ended = true;
					timerLabel.text = string.Format ("{0:00}", timer);
				}
			}
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Space)) {
				Scene loadedLevel = SceneManager.GetActiveScene();
				SceneManager.LoadScene(loadedLevel.buildIndex);
			}
		}

		if (Input.GetButtonDown("Submit")) {
			Scene loadedLevel = SceneManager.GetActiveScene();
				SceneManager.LoadScene(loadedLevel.buildIndex);
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	public void StopTimer() {
		stopTimer = true;
	}
}
