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


	/*
	 * Each property is observable.
	 * Each property is a representation of the state of a read model in the server
	 * There are no 'high level' controllers in this application.
	 * Every single Unity GameObject, component, whatever, subscribes to exactly those properties of state
	 * it needs to know about. When those properties change, they respond accordingly. In turn, each component
	 * has the ability to issue a command to the back end through the Game GameServerInterface component. 
	 * The server handles the validations of the command and any state changes that result.
	 * The GameServerInterface, in turn, updates any important Game state variables here
	 * And the listening components update in turn.
	 * */
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
