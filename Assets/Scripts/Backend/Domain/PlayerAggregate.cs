using System;
public class PlayerAggregate : Aggregate {



	public override Event[] execute(Command command){

		if (command.GetType () == typeof(BeginNewGame)) {
			return this.BeginNewGame ((BeginNewGame)command);
		}


		return new Event[]{};
	}

	public override void hydrate(Event evt){
		if(evt.GetType() == typeof(NewGameBegun)){
			this.OnNewGameBegun((NewGameBegun)evt);
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


	private void OnNewGameBegun(NewGameBegun evt){
		this.id = evt.playerId;
	}
}
