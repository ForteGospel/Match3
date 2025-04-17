using UnityEngine;

[CreateAssetMenu(fileName = "floatScriptable", menuName = "Scriptable Objects/floatScriptable")]
public class floatScriptable : ScriptableObject
{
    public float value;

    private void OnEnable()
    {
        value = 0f;
    }
}
