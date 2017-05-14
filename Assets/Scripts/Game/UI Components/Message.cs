using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour {

	Text text;
	static Message instance;
	Timed messageDelayBeforeDisappear;

	public static Message Instance{
		get{
			return instance;
		}
	}

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
		instance = gameObject.GetComponent<Message> ();
		text = gameObject.GetComponent<Text> ();
		messageDelayBeforeDisappear = gameObject.GetComponent<Timed> ();
		GameState.Instance.characterSet += (id) => {
			if (id == Constants.NULL_ID) { 
				messageDelayBeforeDisappear.Set (2);
				messageDelayBeforeDisappear.Done += () => {
					gameObject.SetActive (false);
					text.text="";
				};
				messageDelayBeforeDisappear.On ();
			}
		};

		GameState.Instance.messageSet+= () => {
			string newMessage = GameState.Instance.MessageState.message;
			text.text = newMessage;
			gameObject.SetActive(newMessage.Length > 0);
		};
	}

}
