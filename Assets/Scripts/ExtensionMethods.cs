using System;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static T MinBy<T>(this IEnumerable<T> collection, Func<T, float> evaluateValue)
    {
        var min = default(T);
        var minVal = Mathf.Infinity;

        foreach (var element in collection)
        {
            var value = evaluateValue(element);
            if (value < minVal)
            {
                minVal = value;
                min = element;
            }
        }

        return min;
    }
}