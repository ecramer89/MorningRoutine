using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour {
	int secondsBetweenText = 2;

	void Start () {
		gameObject.SetActive (false);
	
		GameState.Instance.playerFetched += () => {
			gameObject.SetActive (true);
			string playerName = GameState.Instance.PlayerState.name;
			string[] introText = new string[]{
				$"Hello {playerName}.",
				"What would you like to do?"
			};
			int introTextIndex = 0;
			Timer textTimer = GameObject.Instantiate(CommonPrefabs.Instance.Timer).GetComponent<Timer>();
			textTimer.SetTimer(secondsBetweenText);
			textTimer.TimerDone += () => {
				ActionCreator.Instance.SetMessage(introText[introTextIndex]);
				introTextIndex++;
				if(introTextIndex < introText.Length){
					textTimer.SetTimer(secondsBetweenText);
				}
				else {
					GameObject.Destroy(textTimer.gameObject);
				}
			};
			textTimer.StartTimer();
		};
		
	}

}
