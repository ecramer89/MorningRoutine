using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour {

	Text text;
	static Message instance;
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

		GameState.Instance.messageSet+= (newMessage, oldMessage) => {
			text.text = newMessage;
			gameObject.SetActive(newMessage.Length > 0);
		};
	}

}
