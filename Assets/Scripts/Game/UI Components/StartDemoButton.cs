using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartDemoButton : MonoBehaviour {


	void Start () {
		Button button = GetComponent<Button> ();
		button.onClick.AddListener (StartDemo);
		gameObject.SetActive (false);
		GameState.Instance.characterAdded += (characterId) => {
			gameObject.SetActive (true);
		};

		GameState.Instance.messageSet += () => {
			gameObject.SetActive (false);
		};
			
	}

	void StartDemo(){
		ActionCreator.Instance.InitiateStory (GlobalGameConstants.DEMO_CHARACTER_NAME);
	}
}
