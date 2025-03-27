using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class gridManager : MonoBehaviour
{
    [SerializeField] int x;
    [SerializeField] int y;

    [SerializeField] TileController tile;
    TileController[,] grid;
    List<TileController> tilesToDestroy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initializeGrid();
        CheckAllForMatch();
        List<TileController> tilesToDestroy = new List<TileController>();
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
                int gemType = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TileType)).Length);
                grid[i, j] = Instantiate(tile, new Vector3(i, j, 0), Quaternion.identity, transform);
                grid[i, j].GetComponent<TileController>().GemType = (TileType)Enum.ToObject(typeof(TileType), gemType);
            }
        }       
    }

    public void CheckAllForMatch()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Debug.Log(i + " " + j);
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

                if (possibleGems.Count >= 3) 
                {
                    foreach (TileController gem in possibleGems)
                        gem.GetComponent<SpriteRenderer>().color = Color.gray;
                }

                // Horizontal check
                //if (i < x - 2)
                //{
                //    TileController right1 = grid[i + 1, j];
                //    TileController right2 = grid[i + 2, j];

                //    if (current != null && right1 != null && right2 != null &&
                //        current.GemType == right1.GemType && current.GemType == right2.GemType)
                //    {
                //        Debug.Log($"Match found at: ({i},{j}) horizontal");
                //        // You could add to a list of matched tiles here
                //        current.GetComponent<SpriteRenderer>().color = Color.gray;
                //        right1.GetComponent<SpriteRenderer>().color = Color.gray;
                //        right2.GetComponent<SpriteRenderer>().color = Color.gray;
                //    }
                //}

                //// Vertical check
                //if (j < y - 2)
                //{
                //    TileController down1 = grid[i, j + 1];
                //    TileController down2 = grid[i, j + 2];

                //    if (current != null && down1 != null && down2 != null &&
                //        current.GemType == down1.GemType && current.GemType == down2.GemType)
                //    {
                //        Debug.Log($"Match found at: ({i},{j}) vertical");
                //        current.GetComponent<SpriteRenderer>().color = Color.gray;
                //        down1.GetComponent<SpriteRenderer>().color = Color.gray;
                //        down2.GetComponent<SpriteRenderer>().color = Color.gray;
                //    }
                //}
            }
        }
    }

    void checkTileForMatch ()
    {

    }
}
