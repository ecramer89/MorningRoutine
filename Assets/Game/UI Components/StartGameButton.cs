using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour {

	void Start () {
		GameState.Instance.DayIdChanged += (int oldDayId, int newDayId) => {
			gameObject.SetActive (false);
		};
	}
}
