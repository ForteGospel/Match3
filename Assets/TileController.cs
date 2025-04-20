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
    [SerializeField] ParticleSystem particle;
    [SerializeField] Color[] colors;

    private gridManager gridManager;
    private roulleteController roulleteController;

    private void Start()
    {
        changeGemType();
        changeParticleColor();
        gridManager = FindObjectOfType<gridManager>();
        roulleteController = FindFirstObjectByType<roulleteController>();

    }

    private void Update()
    {
        enableParticle();
    }

    void enableParticle()
    {
         if (roulleteController.roulleteType != GemType)
            particle.Stop();
        else
        {
            if (particle.isStopped)
                particle.Play();
        }
            
    }

    void changeParticleColor()
    {
        switch (GemType)
        {
            case TileType.Red:
                particle.startColor = colors[0];
                break;
            case TileType.Green:
                particle.startColor = colors[1];
                break;
            case TileType.Blue:
                particle.startColor = colors[2];
                break;
            case TileType.Purple:
                particle.startColor = colors[4];
                break;
            case TileType.Yellow:
                particle.startColor = colors[3];
                break;
        }
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
