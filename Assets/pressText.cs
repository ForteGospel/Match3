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
        if (roulleteTurns.value >= 5f)
            text.text = "Press \n To Roll";
        else
            text.text = ((int)roulleteTurns.value) + "/5\n" + "to Roll";
    }
}
