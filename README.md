# FastList<T> and CharArray
  A `FastList<T>` is a replacement for a `List<T>`, which performs a bit faster because it doesn't do as much extra work for convenience and/or safety.  The biggest risk is that a `FastList` can reduce its count without removing items from its internal array - so asking it for an index which is outside of the `count` range, but inside of the "array length" range, will silently fail (by returning a value instead of reporting "out of bounds").  For example, `myFastList.Clear()` will set `count` to zero, but will not perform any changes to the internal array.  `myFastList.HardClearCount()` or `myFastList.HardClearAll()` can be used instead, if you need to make sure that values/references are deleted from the internal array, as well.
  
  A `CharArray` inherits from `FastList<char>`, and is specifically intended to be used with TextMeshPro.  TMPro allows you to set the text content of a textbox by providing a `char[]` and a count - CharArray can provide it with these things, and it gives you some handy `Append()` functions for appending strings, integers, and float values.
  
  Example usage of a `CharArray`:
```
// initialize with capacity
CharArray chars = new CharArray(100);

void UpdateTextbox() {
    // // this function is equivalent to:
    //
    // string text = "Score: ";
    // text += GetScoreAsInt();
    // text += ", Time: ";
    // text += GetTimeAsFloat().ToString("0.00");
    // myTextMeshPro.SetText(text);
    //
    // // (except CharArray doesn't allocate garbage!)

    chars.Clear();
    chars.Append("Score: ");
    chars.Append(GetScoreAsInt());
    chars.Append(", Time: ");
    chars.Append(GetTimeAsFloat(), 2);
    
    myTextMeshPro.SetText(chars);
}
```
  Note that if you're profiling for GC allocations, you should do it in a build, instead of in the editor - the Unity editor allocates some garbage of its own while previewing your app in Play Mode.

  Generally, you should be able to feel out the usage of `FastList` and `CharArray` by checking the hints in their autocomplete-popups (they're similar to a `List` and a `StringBuilder`).  They're very simple, so it should be pretty easy to add your own extra features to them, as well.  If you don't want `FastList<T>` in your project, it should be straightforward to modify `CharArray` so it inherits (or contains) a `List<char>` instead - or you could inline all of the `FastList` logic directly into `CharArray`.
    
  Let me know if you add cool stuff that seems like it'd be useful to other folks!  So far, Sam Loeschen has contributed some helper functions to `FastList`:  `First()` and `Last()`, and `Shuffle()`.
