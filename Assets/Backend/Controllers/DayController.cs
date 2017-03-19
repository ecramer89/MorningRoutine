
public class DayController  {


	public static CreateDayResponse CreateDay(){
		int dayId = IntegerGUIDCreator.CreateGUID ();
		Command command = new CreateDay (dayId);
		Aggregate day = new DayAggregate ();
		bool success = CommandHandler.HandleCommand(day, dayId, command);
		CreateDayResponse response = new CreateDayResponse (dayId, !success);
		return response;
	}

}
