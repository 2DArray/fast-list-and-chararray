using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using TMPro;


/// <summary>
/// Similar to a string or a StringBuilder, made for appending strings/ints/floats to each other so the result can be displayed with a TextMeshPro.
/// <para />After appending your content, call myTextMeshPro.SetText(myCharArray)
/// <para />(Why?) Using System.String to represent dynamic values will always involve GC allocations.  StringBuilder addresses this, but appending numbers will involve GC allocations because it calls number.ToString(). The goal of CharArray is to hide these pain-points and allow GC-free textbox updating by default, as long as you provide an appropriate initial capacity.
/// </summary>
[System.Serializable]
public class CharArray : FastList<char> {
	
	string digits = "0123456789";

	/// <summary>
	/// Create a new CharArray, for dynamically-updated
	/// TextMeshPro content.  
	/// </summary>
	/// <param name="capacity">The default capacity.</param>
	/// <returns></returns> 
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public CharArray(int capacity=50) : base(capacity) {}

	/// <summary>
	/// Create a new CharArray from a string.
	/// </summary>
	/// <param name="fromString">The string to base this CharArray upon.</param>
	/// <param name="capacity">If you do not
	/// specify a capacity, the default is double the length
	/// of the input string.</param>
	/// <returns></returns> 
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public CharArray(string fromString, int capacity = -1) : base(capacity == -1 ? fromString.Length*2 : capacity ) {
		Append(fromString);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void ApplyToTextMeshPro(TMP_Text target) {
		target.SetText(this);
	}

	/// <summary>
	/// Add a string to the end of the CharArray.
	/// (Equivalent to myString+="other string")
	/// </summary>
	/// <param name="text">The string to append.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(string text) {
		for (int i=0; i<text.Length; i++) {
			Add(text[i]);
		}
	}

	/// <summary>
	/// Add a integer value to the end of the CharArray.
	/// (Equivalent to myString+=1000)
	/// </summary>
	/// <param name="value">The integer to append.</param>
	/// <param name="minLengthPadLeft">The minimum digit-count - extra
	/// digits will be left-padded.  Append(100, 6) will append "000100"</param>
	public void Append(int value, int minLengthPadLeft=0) {
		if (value<0) {
			Add('-');
			value = -value;
		}
		int numberStart = count;
		int digitCount = 1;
		Add(digits[value%10]);

		while (value>9) {
			digitCount++;
			value /= 10;
			Add(digits[value%10]);
		}
		for (int i=digitCount; i<minLengthPadLeft; i++) {
			Add('0');
			digitCount++;
		}

		int numberEnd = count-1;
		for (int i=0; i<digitCount/2; i++) {
			int index1 = numberStart+i;
			int index2 = numberEnd-i;
			char temp = this[index1];
			this[index1] = this[index2];
			this[index2] = temp;
		}
	}

	/// <summary>
	/// Add a 64-bit integer value to the end of the string.
	/// (Equivalent to myString+=(long)1000)
	/// </summary>
	/// <param name="value">The integer to append.</param>
	/// <param name="minLengthPadLeft">The minimum digit-count - extra
	/// digits will be left-padded.  Append(100, 6) will append "000100"</param>
	public void Append(long value, int minLengthPadLeft=0) {
		if (value<0) {
			Add('-');
			value = -value;
		}
		int numberStart = count;
		int digitCount = 1;
		Add(digits[(int)(value%10)]);

		while (value>9) {
			digitCount++;
			value /= 10;
			Add(digits[(int)(value%10)]);
		}
		for (int i=digitCount; i<minLengthPadLeft; i++) {
			Add('0');
			digitCount++;
		}

		int numberEnd = count-1;
		for (int i=0; i<digitCount/2; i++) {
			int index1 = numberStart+i;
			int index2 = numberEnd-i;
			char temp = this[index1];
			this[index1] = this[index2];
			this[index2] = temp;
		}
	}

	/// <summary>
	/// Add a float value to the end of the string.
	/// (Equivalent to myString+=2.5f)
	/// </summary>
	/// <param name="value">The float value to append.</param>
	/// <param name="digitsAfterDecimal">The number of digits after the decimal place.
	/// (Append(2f, 3) is equivalent to myString+="2.000")<param>
	public void Append(float value, int digitsAfterDecimal=1) {
		int intValue = (int)value;
		Append(intValue);

		if (digitsAfterDecimal>0) {
			Append('.');

			long scale = 1;
			for (int i=0;i<digitsAfterDecimal;i++) {
				scale *= 10;
			}

			double floatDecimalDigits = ((double)(value-(int)value))*scale;
			long decimalDigits = (long)(floatDecimalDigits);

			Append(decimalDigits, digitsAfterDecimal);
		}
	}

	/// <summary>
	/// Add a char to the end of the string.
	/// (Equivalent to myString+='0')
	/// </summary>
	/// <param name="c">The char to append.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Append(char c) {
		Add(c);
	}

	/// <summary>
	/// Returns the System.String value of this
	/// CharArray.  This allocates a new string.
	/// </summary>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override string ToString() {
		return new string(array);
	}

}

public static class TMProCharArrayExtension {
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetText(this TMP_Text textMeshPro, CharArray charArray) {
		textMeshPro.SetCharArray(charArray.GetArray(), 0, charArray.count);
	}
}