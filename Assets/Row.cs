using System.Collections;
using UnityEngine;

public class Row : MonoBehaviour
{
    private int randomValue;
    private float timeInterval;
    public bool rowStopped;
    public string stoppedSlot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rowStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRotating()
    {
        stoppedSlot = "";
        StartCoroutine(rotate());
    }

    private IEnumerator rotate()
    {
        rowStopped = false;

        timeInterval = 0.025f;

        for (int i = 0; i < 44; i++)
        {
            if (transform.position.y <= -2.65)
                transform.position = new Vector2(transform.position.x, 4.35f);

            transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);

            yield return new WaitForSeconds(timeInterval);
        }

        randomValue = Random.Range(60, 100);

        switch (randomValue % 4)
        {
            case 1:
                randomValue += 3;
                break;
            case 2:
                randomValue += 2;
                break;
            case 3:
                randomValue += 1;
                break;
        }

        for (int i = 0; i < randomValue; i++)
        {
            if (transform.position.y <= -2.65)
                transform.position = new Vector2(transform.position.x, 4.35f);


            transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);

            if (i > Mathf.RoundToInt(randomValue * 0.25f))
                timeInterval = 0.05f;
            if (i > Mathf.RoundToInt(randomValue * 0.5f))
                timeInterval = 0.1f;
            if (i > Mathf.RoundToInt(randomValue * 0.75f))
                timeInterval = 0.15f;
            if (i > Mathf.RoundToInt(randomValue * 0.95f))
                timeInterval = 0.2f;

            yield return new WaitForSeconds(timeInterval);
        }

        if (transform.position.y == -2.65f)
            stoppedSlot = "diamond";
        else if (transform.position.y == -1.65f)
            stoppedSlot = "crown";
        else if(transform.position.y == -0.65f)
            stoppedSlot = "mellon";
        else if(transform.position.y == 0.35)
            stoppedSlot = "bar";
        else if(transform.position.y == 1.35f)
            stoppedSlot = "seven";
        else if(transform.position.y == 2.35f)
            stoppedSlot = "cherry";
        else if(transform.position.y == 3.35f)
            stoppedSlot = "lemon";
        else if(transform.position.y == 4.35f)
            stoppedSlot = "diamond";

        rowStopped = true;
    }
}
