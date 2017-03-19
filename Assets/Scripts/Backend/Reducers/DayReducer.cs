using System.Collections.Generic;
using System;
using UnityEngine;

public class DayReducer : Reducer {
	public override void Reduce(Event evt, ModelTable table){
		Type type = evt.GetType ();
		if(type == typeof(DayCreated)){
			DayCreated dayCreated = (DayCreated)evt;
			table.CreateOne (dayCreated.dayId, new DayReadModel(dayCreated.dayId));
			return;
		}
	}
}
