using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public ScoreController scoreController;

    public UnityEvent response;

    private void OnEnable()
    {
        scoreController.RegisterListener(this);
    }

    private void OnDisable()
    {
        scoreController.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        response.Invoke();
    }
}
