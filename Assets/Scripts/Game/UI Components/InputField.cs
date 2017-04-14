using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InputField : MonoBehaviour {
	public Text text;

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
		GameState.Instance.messageSet += () => {
			gameObject.SetActive (true);
		};
	}

	public void OnPressEnter(){
		string userInput = text.text;
		ActionCreator.Instance.AdvanceDialogue (userInput);
	}
	

}
