using System.Collections;
using System.Collections.Generic;


public class GameState {
	//GameState is a Singleton.
	static GameState instance = null;
	public static GameState Instance{
		get {
			if(instance == null) instance = new GameState();
			return instance;
		}
	}

	//Each property is observable.
	public delegate void DayIdChangeHandler(int oldDayId, int newDayId);
	public event DayIdChangeHandler DayIdChanged;
	int dayId;
	public int DayId{
		get {
			return dayId;
		}
		set {
			int temp = dayId;
			dayId = value;
			DayIdChanged (temp, dayId);
		}

	}



}
