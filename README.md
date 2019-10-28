# FastList<T> and CharArray
A FastList<T> is a replacement for a List<T>, which performs a bit faster because it doesn't do as much extra work for convenience and/or safety.  The biggest risk is that a FastList can reduce its count without removing items from its internal array - so asking it for an index which is outside of the "count" range, but inside of the "array length" range, will silently fail (by returning a value instead of reporting "out of bounds").
  
  A CharArray inherits from FastList<char>, and is specifically intended to be used with TextMeshPro.  TMPro allows you to set the text content of a textbox by providing a char[] and a count - CharArray can provide it with these things, and it gives you some handy `Append()` functions for appending text, integers, or float values.
  
  Generally, you should be able to feel out the usage of FastList and CharArray by checking the hints in their autocomplete-popups.
