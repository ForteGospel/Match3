using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField] TileType gemtype;
    public TileType GemType
    {
        get { return gemtype; }
        set
        {
            gemtype = value;
            changeGemType();
        }
    }

    [SerializeField] Sprite[] gemSprite;

    private void Start()
    {
        changeGemType();
    }

    void changeGemType()
    {
        GetComponent<SpriteRenderer>().sprite = gemSprite[((int)gemtype)];
    }
}

public enum TileType
{
    Red, Green, Blue, Yellow, Purple
}
