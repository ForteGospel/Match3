using UnityEngine;
using DG;
using DG.Tweening;

public class roulleteController : MonoBehaviour
{
    public Vector2 RotatePower;
    public float StopPower;

    private Rigidbody2D rbody;
    int inRotate;

    public TileType roulleteType;

    AudioSource audioSource;

    [SerializeField] floatScriptable roulleteTurns;

    [SerializeField] ParticleSystem particle;
    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    float t;
    private void Update()
    {

        if (rbody.angularVelocity > 0)
        {
            rbody.angularVelocity -= StopPower * Time.deltaTime;

            audioSource.pitch = rbody.angularVelocity / 1200;

            rbody.angularVelocity = Mathf.Clamp(rbody.angularVelocity, 0, 1440);
        }

        if (rbody.angularVelocity == 0 && inRotate == 1)
        {
            t += 1 * Time.deltaTime;
            if (t >= 0.5f)
            {
                GetReward();

                inRotate = 0;
                audioSource.Stop();
                t = 0;
            }
        }

        if (roulleteTurns.value >= 5f)
        {
            if (particle.isStopped)
                particle.Play();
        }
        else
            particle.Stop();
    }


    public void Rotete()
    {
        if (inRotate == 0 && roulleteTurns.value >= 5f)
        {
            roulleteTurns.value = 0f;
            audioSource.pitch = 1;
            audioSource.Play();
            rbody.AddTorque(Random.Range(RotatePower.x, RotatePower.y));
            inRotate = 1;
        }
    }



    public void GetReward()
    {
        float rot = transform.eulerAngles.z;

        Debug.Log(rot);

        if (rot > 0 && rot <= 71)
        {
            transform.DOLocalRotate(new Vector3(0, 0, 35), 0.2f, RotateMode.Fast);
            //GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 35);
            roulleteType = TileType.Red;
        }
        else if (rot > 71 && rot <= 144)
        {
            transform.DOLocalRotate(new Vector3(0, 0, 110), 0.2f, RotateMode.Fast);
            //GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 110);
            roulleteType = TileType.Yellow;
        }
        else if (rot > 144 && rot <= 216)
        {
            transform.DOLocalRotate(new Vector3(0, 0, 180), 0.2f, RotateMode.Fast);
            //GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 180);
            roulleteType = TileType.Green;
        }
        else if (rot > 216 && rot <= 288)
        {
            transform.DOLocalRotate(new Vector3(0, 0, 250), 0.2f, RotateMode.Fast);
            //GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 250);
            roulleteType = TileType.Purple;
        }
        else if (rot > 288 && rot <= 360)
        {
            transform.DOLocalRotate(new Vector3(0, 0, 325), 0.2f, RotateMode.Fast);
            //GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 325);
            roulleteType = TileType.Blue;
        }
    }
}
