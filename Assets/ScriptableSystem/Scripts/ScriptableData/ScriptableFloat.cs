using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableData/ScriptableFloat")]
public class ScriptableFloat : ScriptableObject
{
    public float Value;

    public float GetValue()
    {
        return Value;
    }
    public void UpdateValue(float _value)
    {
        Value = _value;
    }
}
