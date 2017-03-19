
public class DayController  {


	public void CreateDay(){
		int dayId = IntegerGUIDCreator.CreateGUID ();
		Command command = new CreateDay (dayId);
		Aggregate day = new Day ();
		//CommandHandler.handleCommand(day, dayId, command);
	}

}
