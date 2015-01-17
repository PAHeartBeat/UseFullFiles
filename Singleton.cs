/*
 * File: Singleton.cs
 * ==================
 * 
 * Version		: 1.1
 * Author		: Kleber Lopes da Silva (@kleber_swf / www.linkedin.com/in/kleberswf)
 * E-Mail		: Not Available
 * Copyright		: Not Available
 * Company		: Not Available
 * Script Location	: Plugins/INICPlugins/Other
 * 
 * 
 * Created By		: Kleber Lopes da Silva
 * Created Date		: 2014.11.24
 * 
 * Last Modified by	: Ankur Ranpariya on 2014.11.28 (ankur30884@gmail.com / @PA_HeartBeat)
 * Last Modified	: 2014.11.28
 * 
 * Contributors 	:
 * Curtosey By		: Kleber Lopes da Silva (http://kleber-swf.com/singleton-monobehaviour-unity-projects/)
 * 
 * Purpose
 * ====================================================================================================================
 * 
 * 
 * 
 * 
 * ====================================================================================================================
 * LICENCE / WARRENTY
 * ==================
 * Copyright (c) 2014 <<Developer Name / Company name>> 
 * Please direct any bugs/comments/suggestions to <<support email id>>
 * 
 * This program is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General
 * Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option)
 * any later version.
 * 
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser General Public License for more
 * details.
 * 
 * 
 * Change Log
 * ====================================================================================================================
 * v1.0
 * ====
 * 1. Initial version by "Kleber Lopes da Silva"
 * 
 * v1.1
 * ====
 * 1.	Change DestroyImmediate to Destroy for removing Extra copies of singleton instance, DestroyImmediate wiil remove
 * 	copy imeediately and effect in same frame. It should cause null excption error if destroyed copy is referanced.
 * 	whether Destroy will remove instance end of the frame so it will not cause Null exception in same frame, and next
 * 	frame can identify used referance are removed or not.
 * 2.	Now it will throw Exception insted of logging error.
 * ====================================================================================================================
*/

using UnityEngine;
using System;

/// <summary>
/// <para version="1.1.0.0" />	 
/// <para author="Kleber Lopes da Silva, Ranpariya Ankur" />
/// <para support="" />
/// <para>
/// Description: 
/// </para>
/// </summary>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
	private static T _instance;

	public static T Me {
		get {
			if(_instance == null) {
				Type type = typeof(T);
				throw new System.NullReferenceException(type.ToString() + " not present in scene");
			}
			return _instance;
		}
		protected internal set {
			if(value == null) {
				if(_instance && _instance.gameObject) {
					Destroy(_instance.gameObject);
				}
				_instance = null;
			} else {
				_instance = value;
				var type = typeof(T);
				var attribute = Attribute.GetCustomAttribute(type, typeof(SignletonSetting)) as SignletonSetting;
				if(attribute != null && attribute.Persistent) {
					DontDestroyOnLoad(_instance.gameObject);
				}
			}
		}
	}
}