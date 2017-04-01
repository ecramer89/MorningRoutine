using System.Collections;
using System.Collections.Generic;
using System;
public class ModelNameGetter {


	public static string GetModelName(Type type){
		string fullName = type.Name;
		string roleName = type.BaseType.Name;
		return fullName.Substring (0, fullName.Length - roleName.Length).ToLower();
	}
		
}
