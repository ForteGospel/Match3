using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreController", menuName = "Scriptable Objects/ScoreController")]
public class ScoreController : ScriptableObject
{
    public int score;

    public List<GameEventListener> listeners = new List<GameEventListener> ();

    public void Raise()
    {
        for (int  i = 0; i > listeners.Count; i++)
            listeners[i].OnEventRaised ();
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains (listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains (listener))
            listeners.Remove (listener);
    }

    private void OnEnable()
    {
        score = 0;
    }
}
