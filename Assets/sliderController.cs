using UnityEngine;
using UnityEngine.UI;

public class sliderController : MonoBehaviour
{
    [SerializeField] floatScriptable value;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (GetComponent<Slider>().value != value.value)
            GetComponent<Slider>().value = Mathf.Lerp(GetComponent<Slider>().value, value.value, 0.1f);
    }
}
