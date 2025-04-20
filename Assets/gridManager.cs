using DG.Tweening;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Events;
using Unity.VisualScripting;


public class gridManager : MonoBehaviour
{
    [SerializeField] int x;
    [SerializeField] int y;

    public GameObject comboTextPrefab; // assign in inspector

    [SerializeField] TileController tile;
    TileController[,] grid;
    List<TileController> tilesToDestroy;

    TileController selectedTile = null;
    bool isSwapping = false;

    int score = 0;
    int comboMultiplier = 1;

    [SerializeField] ScoreController scoreController;

    [SerializeField] UnityEvent scoreEvent;

    [SerializeField] roulleteController roullete;

    [SerializeField] floatScriptable slidder;

    [SerializeField] floatScriptable roulleteTurns;

    AudioSource source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
        initializeGrid();
        StartCoroutine(MatchLoop());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void initializeGrid()
    {
        grid = new TileController[x, y];

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                grid[i, j] = instantiateGem(i, j);
            }
        }
    }

    void AddScore(int amount)
    {
        scoreController.score += amount * comboMultiplier;
        Debug.Log($"+{amount * comboMultiplier} points! Combo x{comboMultiplier}");

        scoreEvent.Invoke();
    }

    public HashSet<TileController> CheckAllForMatch()
    {
        HashSet<TileController> matchedTiles = new HashSet<TileController>();

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                HashSet<TileController> possibleGems = crossCheck(i, j);

                if (possibleGems.Count >= 3)
                {
                    foreach (TileController gem in possibleGems)
                    {
                        gem.GetComponent<SpriteRenderer>().color = Color.gray;
                        matchedTiles.Add(gem);
                    }
                }
            }
        }

        return matchedTiles;
    }

    HashSet<TileController> crossCheck(int i, int j)
    {
        TileController current = grid[i, j];
        HashSet<TileController> possibleGems = new HashSet<TileController>();
        possibleGems.Add(current);

        //left Check
        if (i != 0 && current.GemType == grid[i - 1, j].GemType)
            possibleGems.Add(grid[i - 1, j]);
        //right check
        if (i != x - 1 && current.GemType == grid[i + 1, j].GemType)
            possibleGems.Add(grid[i + 1, j]);
        //down Check
        if (j != 0 && current.GemType == grid[i, j - 1].GemType)
            possibleGems.Add(grid[i, j - 1]);
        //up Check
        if (j != y - 1 && current.GemType == grid[i, j + 1].GemType)
            possibleGems.Add(grid[i, j + 1]);

        return possibleGems;
    }

    bool testCrossCheck(TileType type, int i, int j)
    {
        HashSet<TileController> possibleGems = new HashSet<TileController>();

        //left Check
        if (i != 0 && type == grid[i - 1, j].GemType)
            possibleGems.Add(grid[i - 1, j]);
        //right check
        if (i != x - 1 && type == grid[i + 1, j].GemType)
            possibleGems.Add(grid[i + 1, j]);
        //down Check
        if (j != 0 && type == grid[i, j - 1].GemType)
            possibleGems.Add(grid[i, j - 1]);
        //up Check
        if (j != y - 1 && type == grid[i, j + 1].GemType)
            possibleGems.Add(grid[i, j + 1]);
        
        return possibleGems.Count >= 2;
    }

    void ClearMatches(HashSet<TileController> matches)
    {
        if (matches.Count >= 5)
        {
            ShakeScreen();
        }
        foreach (var tile in matches)
        {
            float scale = UnityEngine.Random.Range(1.15f, 1.3f);
            tile.transform.DOScale(scale, 0.1f)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.OutBack);
        }

        // Wait a moment before destroying (optional)
        StartCoroutine(DestroyAfterDelay(matches));
    }

    IEnumerator DestroyAfterDelay(HashSet<TileController> matches)
    {
        yield return new WaitForSeconds(0.15f);
        source.pitch = UnityEngine.Random.Range(0.5f, 1f);
        source.Play();

        foreach (var tile in matches)
        {
            grid[tile.x, tile.y] = null;
            if (tile.GemType == roullete.roulleteType)
            {
                slidder.value += 0.02f;
                tile.transform.DOJump(new Vector3(-5, -0.2f, 0), 2, 1, 0.5f).OnComplete(() => Destroy(tile.gameObject));
            }
            else
                Destroy(tile.gameObject);
        }
    }

    void DropTiles()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 1; j < y; j++)
            {
                if (grid[i, j] == null)
                {
                    for (int k = j + 1; k < y; k++)
                    {
                        if (grid[i, k] != null)
                        {
                            grid[i, j] = grid[i, k];
                            grid[i, k] = null;

                            grid[i, j].x = i;
                            grid[i, j].y = j;
                            //grid[i, j].transform.position = new Vector3(i, j, 0);
                            grid[i, j].transform.DOMove(new Vector3(i, j, 0), UnityEngine.Random.Range(0.5f,0.2f)).SetEase(Ease.OutQuad);
                            break;
                        }
                    }
                }
            }
        }
    }

    void RefillBoard()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if (grid[i, j] == null)
                {
                    grid[i, j] = instantiateGem(i, j);
                }
            }
        }
    }

    TileController instantiateGem(int i, int j)
    {
        int gemType = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TileType)).Length);
        TileController gem = Instantiate(tile, new Vector3(i, j + 2f, 0), Quaternion.identity, transform);
        gem.GetComponent<TileController>().GemType = (TileType)Enum.ToObject(typeof(TileType), gemType);
        gem.x = i;
        gem.y = j;

        gem.transform.DOMove(new Vector3(i, j, 0), 0.3f).SetEase(Ease.OutBounce);

        return gem;
    }

    IEnumerator MatchLoop()
    {
        yield return new WaitForSeconds(0.3f);

        comboMultiplier = 1;

        while (true)
        {
            var matches = CheckAllForMatch();

            if (matches.Count == 0)
                break;

            // 🔥 Add score before clearing
            AddScore(matches.Count * 100);

            // 🧃 Combo feedback here if you want
            TriggerComboEffects(comboMultiplier);

            ClearMatches(matches);
            yield return new WaitForSeconds(0.2f);

            DropTiles();
            yield return new WaitForSeconds(0.2f);

            RefillBoard();
            yield return new WaitForSeconds(0.2f);

            comboMultiplier++; // chain continues!
        }

        comboMultiplier = 1;
    }

    void TriggerComboEffects(int combo)
    {
        //if (combo <= 1) return;

        //GameObject popup = Instantiate(comboTextPrefab, new Vector3(x / 2f, y + 0.5f, 0), Quaternion.identity);
        //TMPro.TextMeshPro text = popup.GetComponent<TMPro.TextMeshPro>();
        //text.text = $"Combo x{combo}!";

        //popup.transform.DOScale(1.5f, 0.2f).SetLoops(2, LoopType.Yoyo);
        //popup.transform.DOMoveY(popup.transform.position.y + 1.5f, 0.8f).SetEase(Ease.OutCubic);
        ////text.DOFade(0, 0.8f).OnComplete(() => Destroy(popup));
    }

    public void SelectTile(TileController tile)
    {
        if (isSwapping) return;

        if (selectedTile == null)
        {
            selectedTile = tile;
            HighlightTile(tile, true);
        }
        else
        {
            if (AreSwappable(selectedTile, tile) && testCrossCheck(selectedTile.GemType, tile.x, tile.y))
            {
                StartCoroutine(ShiftTiles(selectedTile, tile));
                roulleteTurns.value += 1f;
            }
            else
            {
                HighlightTile(selectedTile, false);
                selectedTile = tile;
                HighlightTile(tile, true);
            }
        }
    }

    void HighlightTile(TileController tile, bool highlight)
    {
        tile.GetComponent<SpriteRenderer>().color = highlight ? Color.cyan : Color.white;
    }

    bool AreAdjacent(TileController a, TileController b)
    {
        return (Mathf.Abs(a.x - b.x) == 1 && a.y == b.y) || (Mathf.Abs(a.y - b.y) == 1 && a.x == b.x);
    }

    bool AreSwappable(TileController a, TileController b)
    {
        return a.x == b.x || a.y == b.y;
    }

    IEnumerator SwapTiles(TileController a, TileController b)
    {
        isSwapping = true;
        HighlightTile(a, false);
        HighlightTile(b, false);

        // Swap in grid
        grid[a.x, a.y] = b;
        grid[b.x, b.y] = a;

        // Swap positions
        int tempX = a.x, tempY = a.y;

        a.x = b.x; a.y = b.y;
        b.x = tempX; b.y = tempY;

        // Swap visuals
        Vector3 aPos = a.transform.position;
        a.transform.position = b.transform.position;
        b.transform.position = aPos;

        yield return new WaitForSeconds(0.25f);

        var matches = CheckAllForMatch();
        if (matches.Count > 0)
        {
            StartCoroutine(MatchLoop());
        }
        else
        {
            // No match? Swap back
            grid[a.x, a.y] = b;
            grid[b.x, b.y] = a;

            tempX = a.x; tempY = a.y;
            a.x = b.x; a.y = b.y;
            b.x = tempX; b.y = tempY;

            a.transform.position = b.transform.position;
            b.transform.position = aPos;
        }

        selectedTile = null;
        isSwapping = false;
    }

    IEnumerator ShiftTiles(TileController from, TileController to)
    {
        isSwapping = true;

        int fx = from.x;
        int fy = from.y;
        int tx = to.x;
        int ty = to.y;

        if (fx == tx) // Vertical shift
        {
            int startY = Mathf.Min(fy, ty);
            int endY = Mathf.Max(fy, ty);
            List<TileController> column = new List<TileController>();

            // Collect column slice
            for (int y = startY; y <= endY; y++)
            {
                column.Add(grid[fx, y]);
            }

            // Rearrange the list
            column.Remove(from);
            int insertIndex = ty > fy ? column.Count : 0;
            column.Insert(insertIndex, from);

            // Reassign all tiles in column slice
            for (int i = 0; i < column.Count; i++)
            {
                TileController tile = column[i];
                int newY = startY + i;
                tile.y = newY;
                //tile.transform.position = new Vector3(fx, newY, 0);
                tile.transform.DOMove(new Vector3(fx, newY, 0), 0.2f).SetEase(Ease.OutQuad);
                grid[fx, newY] = tile;
            }
        }
        else if (fy == ty) // Horizontal shift
        {
            int startX = Mathf.Min(fx, tx);
            int endX = Mathf.Max(fx, tx);
            List<TileController> row = new List<TileController>();

            // Collect row slice
            for (int x = startX; x <= endX; x++)
            {
                row.Add(grid[x, fy]);
            }

            // Rearrange the list
            row.Remove(from);
            int insertIndex = tx > fx ? row.Count : 0;
            row.Insert(insertIndex, from);

            // Reassign all tiles in row slice
            for (int i = 0; i < row.Count; i++)
            {
                TileController tile = row[i];
                int newX = startX + i;
                tile.x = newX;
                //tile.transform.position = new Vector3(newX, fy, 0);
                tile.transform.DOMove(new Vector3(newX, fy, 0), 0.2f).SetEase(Ease.OutQuad);
                grid[newX, fy] = tile;
            }
        }

        yield return new WaitForSeconds(0.2f);

        var matches = CheckAllForMatch();
        if (matches.Count > 0)
        {
            StartCoroutine(MatchLoop());

        }

        selectedTile = null;
        isSwapping = false;
    }

    void ShakeScreen()
    {
        Camera.main.transform.DOShakePosition(
            duration: 0.2f,
            strength: 0.2f,
            vibrato: 20,
            randomness: 90f
        );
    }
}
