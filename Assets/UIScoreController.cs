using UnityEngine;
using TMPro;
using DG;
using DG.Tweening;
public class UIScoreController : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] ScoreController scoreController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateScore()
    {
        text.text = "Score: " + scoreController.score;

        text.gameObject.transform.DOShakeScale(0.1f, 0.5f, 1, 45, true, ShakeRandomnessMode.Full);
    }
}
