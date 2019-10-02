using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public GameObject cellPrefab;
    public Cell[,] mAllCells = new Cell[8, 8];

    public void Create()
    {
        for (int y=0; y<8; y++)
        {
            for (int x=0; x<8; x++)
            {
                GameObject newCell = Instantiate(cellPrefab, transform);

                RectTransform rectTransform = newCell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2((x*100)+50,(y*100)+50);

                mAllCells[x,y] = newCell.GetComponent<Cell>();
                mAllCells[x,y].Setup(new Vector2Int(x,y), this);

            }
        }

        //Color
        for (int x = 0; x < 8; x += 2)
        {
            for (int y = 0; y < 8; y++)
            {
                //Offset for every other line
                int offset = (y % 2 != 0) ? 0 : 1;
                int finalX = x + offset;

                mAllCells[finalX, y].GetComponent<Image>().color = new Color(230, 220, 187, 255);


            }
        }

    }
}
