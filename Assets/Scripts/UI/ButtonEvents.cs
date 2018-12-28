using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoToChooseCharacter()
    {
        SceneManager.LoadScene("ChooseCharacter");
    }

    public void GoToMainLevel()
    {
        if(CharaterManager.choosedCharacter != null)
        {
            SceneManager.LoadScene("MainLevel");
        }
    }
}
