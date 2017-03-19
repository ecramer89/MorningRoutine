using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameServerInterface : MonoBehaviour {

	public void CreateDay(){
		CreateDayResponse response = DayController.CreateDay ();
		if (!response.error) {
			GameState.DayId = response.dayId;
			Debug.Log ($"Success: New Day {GameState.DayId}");
		} else
			Debug.Log ("Failed");
	}
}
