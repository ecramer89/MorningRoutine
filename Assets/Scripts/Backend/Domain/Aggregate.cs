﻿public abstract class Aggregate  {

	static int nullId = Constants.NULL_ID;
	public static int NullId{
		get { 
			return nullId;
		}
	}

	protected int id;

	public Aggregate(){
		this.id = Aggregate.NullId;
	}
		
	public abstract void hydrate(Event evt);
	public abstract Event[] execute(Command command);

}
