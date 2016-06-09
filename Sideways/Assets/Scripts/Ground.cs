using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ground : MonoBehaviour
{
    public static Ground ground;
    public int worldHeight = 100;
    public int width = 10, height = 3;
    public float delta = 0.64f;
    public Sprite[] tiles;
    int leftTop = 0, middleTop = 1, rightTop = 2, under1 = 3, under2 = 4, under3 = 5;

    List<SpriteRenderer> srs;

    void Awake()
    {
        ground = this;
        GenerateGround();
    }

    public void GeneratePlatforms()
    {
        //Generate different height levels of platforms
        //Generate up to the worldHeight
    }

    public void GenerateGround()
    {
        int count = 0;
        srs = new List<SpriteRenderer>();

        //Generate the top row
        AddTile(Vector2.zero, leftTop, ref count, true);
        for (int i = 1; i < width - 1; i++)
            AddTile(new Vector2(i * delta, 0f), middleTop, ref count, true);
        AddTile(new Vector2((width - 1) * delta, 0f), rightTop, ref count, true);

        for (int k = 1; k <= height; k++)
        {
            //Generate height-1 more rows under
            for (int i = 0; i < width; i++)
            {
                AddTile(new Vector2(i * delta, -k * delta), GetRandomUnderIndex(), ref count);
            }
        }

        BoxCollider2D bc = gameObject.AddComponent<BoxCollider2D>();
        bc.size = new Vector2(width * delta, height);
        bc.offset = new Vector2((width * delta) / 2f, -0.64f);
        gameObject.layer = LayerMask.NameToLayer("Ground");
    }

    void AddTile(Vector2 position, int tileIndex, ref int count, bool topTile = false)
    {
        GameObject g = new GameObject("Tile" + count);
        g.transform.SetParent(transform);
        srs.Add(g.AddComponent<SpriteRenderer>());
        srs[count].sprite = tiles[tileIndex];
        srs[count].transform.position = (Vector2)transform.position + position;

        //if (topTile)
        //{
        //    srs[count].gameObject.AddComponent<BoxCollider2D>();
        //    srs[count].gameObject.layer = LayerMask.NameToLayer("Ground");
        //}

        count++;
    }

    int GetRandomUnderIndex()
    {
        switch (Random.Range(0, 12) % 4)
        {
            default:
            case 0: return under1;
            case 1: return under2;
            case 2: return under3;
        }
    }
    Sprite GetRandomUnder()
    {
        switch (Random.Range(0, 9) % 3)
        {
            default:
            case 0: return tiles[under1];
            case 1: return tiles[under2];
            case 2: return tiles[under3];
        }
    }
}
