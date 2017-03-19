using System;

public class ValidationException : Exception {
	public string Field;
	public ValidationException(string field, string msg) : base(msg){
		this.Field = field;
	}
}



