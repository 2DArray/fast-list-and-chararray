using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayExtensions {
	/// <summary>
	/// Returns a random item from the array.
	/// </summary>
	/// <param name="array"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T RandomItem<T>(this T[] array) {
		return array[Random.Range(0,array.Length)];
	}

	/// <summary>
	/// Converts an array into a FastList.  This does not
	/// make a new copy of the array - the new FastList
	/// will use the existing array internally.
	/// </summary>
	/// /// <param name="fromArray"></param>
	public static FastList<T> ToFastList<T>(this T[] array) {
		return new FastList<T>(array);
	}
}
