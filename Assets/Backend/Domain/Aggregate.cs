public abstract class Aggregate  {

	string name = null;

	public string Name{
		get{
			if (this.name == null) {
				this.name = this.GetType().Name;
			}
			return this.name;
		}
	}

	public abstract void hydrate(Event evt);
	public abstract Event[] execute(Command command);

}
