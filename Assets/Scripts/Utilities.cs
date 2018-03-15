using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {

	public static void LerpTransform(this Transform from, Vector3 toPosition, Quaternion toRotation, float speed) {
        from.position = Vector3.Lerp(from.position, toPosition, speed);
        from.rotation = Quaternion.Lerp(from.rotation, toRotation, speed);
    }

    public static T CloneData<T>(this T obj) where T : class {
        if (obj == null) return null;
        System.Reflection.MethodInfo instance = obj.GetType().GetMethod("MemberwiseClone",
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        if (instance != null)
            return (T)instance.Invoke(obj, null);
        else
            return null;
    }
}
