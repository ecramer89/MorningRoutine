using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InputField : MonoBehaviour {
	public Text text;
	public UnityEngine.UI.InputField inputField;

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);

		inputField = gameObject.GetComponent<UnityEngine.UI.InputField> ();
		GameState.Instance.messageSet += () => {
			gameObject.SetActive (true);
		};
	}

	public void OnPressEnter(){
		string userInput = text.text;
		ActionCreator.Instance.AdvanceStory (userInput);
		ClearInputField ();
		inputField.ActivateInputField ();
	}

	void ClearInputField(){
		inputField.text = "";
		text.text = "";
	}
	

}
