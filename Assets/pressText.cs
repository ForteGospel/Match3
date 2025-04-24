using UnityEngine;
using TMPro;

public class pressText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] floatScriptable roulleteTurns;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (roulleteTurns.value <= 0f)
            text.text = "Press \n To Roll";
        else
            text.text = "";
    }
}
