using System;
public class IntegerGUIDCreator {

	public static int CreateGUID(){
		return System.Guid.NewGuid ().ToString().GetHashCode();
	}
}
