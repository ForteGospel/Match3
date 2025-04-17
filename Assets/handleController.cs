using DG.Tweening;
using UnityEngine;

public class handleController : MonoBehaviour
{
    [SerializeField] slotMachineController Machine;
    [SerializeField] floatScriptable value;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (value.value < 1f) return;

        value.value = 0f;
        Machine.startMachine();
        transform.DOLocalRotate(new Vector3(0,0, 8), 0.5f, RotateMode.Fast).SetLoops(2,LoopType.Yoyo);
    }
}
