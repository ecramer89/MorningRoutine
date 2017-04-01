using System;
public class PlayerAggregate : Aggregate {

	protected DayAggregate day;

	public PlayerAggregate() : base(){
		this.day = new DayAggregate ();
	}

	public override Event[] execute(Command command){

		if (command.GetType () == typeof(BeginNewGame)) {
			return this.BeginNewGame ((BeginNewGame)command);
		}

		if(command.GetType() == typeof(CreateDay)){
			return this.CreateDay ((CreateDay)command);
		}

		return new Event[]{};
	}

	public override void hydrate(Event evt){
		if(evt.GetType() == typeof(NewGameBegun)){
			this.OnNewGameBegun((NewGameBegun)evt);
		}
		if (evt.GetType () == typeof(DayCreated)) {
			this.OnDayCreated ((DayCreated)evt);
		}
	}


	private Event[] BeginNewGame(BeginNewGame command){
		if (this.id != Aggregate.NullId) {
			throw new ValidationException ("id", "Already exists.");
		}
		return new Event[] {
			new NewGameBegun(command.playerId, command.playerName)
		};

	}

	private Event[] CreateDay(CreateDay command){

		if (this.id == Aggregate.NullId) {
			throw new ValidationException ("id", "Player not found.");
		}

		return day.execute (command);
	}


	private void OnDayCreated(DayCreated evt){
		this.day.hydrate (evt);
	}

	private void OnNewGameBegun(NewGameBegun evt){
		this.id = evt.playerId;
	}
}
