using System;
public class IntegerGUIDCreator {

	public static int CreateGUID(){
		//Since C# does not allow for uninitialized int types to be null,
		//reserve a special integer to represent "id not initialized" in all Aggregate classes.
		//thus must gaurantee that new aggegate id never equals the NullId.
		int guuid = Aggregate.NullId;
		while (guuid == Aggregate.NullId) {
			guuid = System.Guid.NewGuid ().ToString ().GetHashCode ();
		}
		return guuid;
	}
}
