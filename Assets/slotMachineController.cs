using System.Collections;
using UnityEngine;

public class slotMachineController : MonoBehaviour
{
    [SerializeField] Row[] rows;
    [SerializeField] ParticleSystem[] particles;

    private bool resultsChecked = false;
    AudioSource source;

    [SerializeField] ScoreController scoreController;
    int score = 100000;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
        {
            resultsChecked = false;
        }

        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked)
        {
            checkResults();
            resultsChecked = true;
            source.Stop();
        }
    }

    public void startMachine()
    {
        foreach (Row row in rows)
        {
            row.StartRotating();
            source.Play();
        }
    }

    void checkResults()
    {
        if (rows[0].stoppedSlot == rows[1].stoppedSlot && rows[0].stoppedSlot == rows[2].stoppedSlot)
        {
            StartCoroutine(handleParticles());
            switch (rows[0].stoppedSlot)
            {
                case "diamond":
                    scoreController.score += score * 10;
                    break;
                case "crown":
                    scoreController.score += score * 2;
                    break;
                case "mellon":
                    scoreController.score += score * 3;
                    break;
                case "bar":
                    scoreController.score += score * 4;
                    break;
                case "seven":
                    scoreController.score += score * 7;
                    break;
                case "cherry":
                    scoreController.score += score * 5;
                    break;
                case "lemon":
                    scoreController.score += score * 6;
                    break;
            }
        }
    }

    IEnumerator handleParticles()
    {
        foreach (ParticleSystem particle in particles)
            particle.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        foreach (ParticleSystem particle in particles)
            particle.gameObject.SetActive(false);
    }
}
