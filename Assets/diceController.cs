using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static UnityEngine.Rendering.DebugUI;

public class diceController : MonoBehaviour
{
    [SerializeField] float currNumber;
    [SerializeField] floatScriptable diceValue;
    [SerializeField] Sprite[] images;
    Image image;
    public bool isPlaying = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        diceValue.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying) return;
        if (diceValue.value <= 0) image.enabled = false;
        if (currNumber != diceValue.value) setDiceImage();   
    }

    void setDiceImage()
    {
        if (diceValue.value <= 0) return;
        currNumber = diceValue.value;
        image.sprite = images[(int)currNumber - 1];
    }

    public IEnumerator playDice()
    {
        isPlaying = true;
        image.enabled = true;
        while (isPlaying)
        {
            image.sprite = images[Random.Range(0, images.Length)];
            yield return new WaitForSeconds(0.1f);
        }

        setDiceImage();
    }
}
