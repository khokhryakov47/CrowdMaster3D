using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions
{
    public static Vector3 RotateAroundY(this Vector3 vector, float eulerAngle)
    {
        float newX = vector.x * Mathf.Cos(Mathf.Deg2Rad * eulerAngle) - vector.z * Mathf.Sin(Mathf.Deg2Rad * eulerAngle);
        float newZ = vector.z * Mathf.Cos(Mathf.Deg2Rad * eulerAngle) + vector.x * Mathf.Sin(Mathf.Deg2Rad * eulerAngle);
        return new Vector3(newX, 0, newZ);
    }

    public static Vector3 ToPlane(this Vector3 vector)
    {
        vector.y = 0;
        return vector;
    }

    public static bool TryGetActiveComponent<T>(this GameObject gameObject, out T targetComponent) where T : class
    {
        targetComponent = null;

        T[] findedComponents = gameObject.GetComponents<T>();
        if (findedComponents == null)
            return false;

        foreach (T component in findedComponents)
        {
            if ((component as MonoBehaviour).enabled == true)
                targetComponent = component;
        }

        return targetComponent != null;
    }
}
