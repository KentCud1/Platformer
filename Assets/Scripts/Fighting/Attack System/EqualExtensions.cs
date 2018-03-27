using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EqualExtensions {

    public static bool IsEqual(this string[] array1, string[] array2) {
        if (array1.Length != array2.Length) {
            return false;
        }

        for (int i = 0; i < array1.Length; i++) {
            if (array1[i] != array2[i]) {
                return false;
            }
        }

        return true;
    }

    public static bool IsEqual(this List<string> list1, List<string> list2) {
        if (list1.Count != list2.Count) {
            return false;
        }

        for (int i = 0; i < list1.Count; i++) {
            if (list1[i] != list2[i]) {
                return false;
            }
        }

        return true;
    }

    public static bool IsEqual(this string[] array, List<string> list) {
        if (array.Length != list.Count) {
            return false;
        }

        for (int i = 0; i < array.Length; i++) {
            if (array[i] != list[i]) {
                return false;
            }
        }

        return true;
    }

    public static bool IsEqual(this List<string> list, string[] array) {
        if (array.Length != list.Count) {
            return false;
        }

        for (int i = 0; i < array.Length; i++) {
            if (array[i] != list[i]) {
                return false;
            }
        }

        return true;
    }
}
