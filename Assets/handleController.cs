using DG.Tweening;
using UnityEngine;

public class handleController : MonoBehaviour
{
    [SerializeField] slotMachineController Machine;
    [SerializeField] floatScriptable value;
    [SerializeField] ParticleSystem particle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (value.value >= 1f)
        {
            Debug.Log("greater than 1");
            if (particle.isStopped)
                particle.Play();
        }
        else
            particle.Stop();
    }

    private void OnMouseDown()
    {
        if (value.value < 1f) return;

        value.value = 0f;
        Machine.startMachine();
        transform.DOLocalRotate(new Vector3(0,0, 8), 0.5f, RotateMode.Fast).SetLoops(2,LoopType.Yoyo);
    }
}
