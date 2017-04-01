using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameServerInterface : MonoBehaviour {
	//"action creators"
	public void CreateDay(){
		CreateDayResponse response = DayController.CreateDay ();
		if (!response.error) {
			GameState state = GameState.Instance;
			state.DayId = response.dayId;
			Debug.Log ($"Success: New Day {state.DayId}");
		} else
			Debug.Log ("Failed");
	}
}
