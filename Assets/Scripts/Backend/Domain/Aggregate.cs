public abstract class Aggregate  {

	static string nullId = Constants.NULL_ID;
	public static string NullId{
		get { 
			return nullId;
		}
	}

	protected string id;

	public Aggregate(){
		this.id = Aggregate.NullId;
	}
		
	public abstract void hydrate(Event evt);
	public abstract Event[] execute(Command command);

}
