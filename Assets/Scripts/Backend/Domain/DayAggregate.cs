using System;

public class DayAggregate : Aggregate {
	
	public override Event[] execute(Command command){
		
		if(command.GetType() == typeof(CreateDay)){
			return this.CreateDay ((CreateDay)command);
		}

		return new Event[]{};
	}

	private Event[] CreateDay(CreateDay command){
		
		if(this.id != Aggregate.NullId) {
			throw new ValidationException ("id", "Already exists.");
		}

		return new Event[] {
			new DayCreated (command.dayId)
		};
	}


	public override void hydrate(Event evt){
		if (evt.GetType () == typeof(DayCreated)) {
			this.OnDayCreated ((DayCreated)evt);
		}
	}

	private void OnDayCreated(DayCreated evt){
		this.id = evt.dayId;
	}
		

}
