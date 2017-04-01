using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerReducer : Reducer {
	public override void Reduce(Event evt, ModelTable table){
		Type type = evt.GetType ();
		if(type == typeof(NewGameBegun)){
			NewGameBegun gameBegun = (NewGameBegun)evt;
			table.InsertModel (gameBegun.playerId, new PlayerReadModel(gameBegun.playerId, gameBegun.playerName));
			return;
		}
	}
}
