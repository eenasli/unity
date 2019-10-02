using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CellState
{
    None,
    Friendly,
    Enemy,
    Free,
    OutOfBounds
}


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

    //Validate state for each targetCell
    public CellState ValidateCell(int targetX, int targetY, BasePiece checkingPiece)
    {
        //Bounds check
        if (targetX < 0 || targetX > 7)
            return CellState.OutOfBounds;
        
        if (targetY < 0 || targetY > 7)
            return CellState.OutOfBounds;

        //Get Cell
        Cell targetCell = mAllCells[targetX, targetY];

        //If Cell hase a piesce
        if(targetCell.mCurrentPiece != null)
        {
            //If friendly
            if (checkingPiece.mColor == targetCell.mCurrentPiece.mColor)
                return CellState.Friendly;
           
            // If Enemy
           if(checkingPiece.mColor != targetCell.mCurrentPiece.mColor)
                return CellState.Enemy;
        }
            return CellState.Free;
    }
}
