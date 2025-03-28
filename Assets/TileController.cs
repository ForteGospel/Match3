using UnityEngine;

public class TileController : MonoBehaviour
{
    public int x, y;
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

    private gridManager gridManager;

    private void Start()
    {
        changeGemType();
        gridManager = FindObjectOfType<gridManager>();
    }

    void changeGemType()
    {
        GetComponent<SpriteRenderer>().sprite = gemSprite[((int)gemtype)];
    }

    private void OnMouseDown()
    {
        if (gridManager != null)
        {
            gridManager.SelectTile(this);
        }
    }
}

public enum TileType
{
    Red, Green, Blue, Yellow, Purple
}
