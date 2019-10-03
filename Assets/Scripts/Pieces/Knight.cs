using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knight : BasePiece
{

    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        //Base Setup
        base.Setup(newTeamColor, newSpriteColor, newPieceManager);

        // Knight stuff
        GetComponent<Image>().sprite = Resources.Load<Sprite>("T_Knight");

    }

    private void CreateCellPath(int filpper)
    {
        //Target position
        int currentX = mCurrentCell.mBoardPosition.x; //Get the actual position from the board
        Console.WriteLine("currentX =", currentX);
        int currentY = mCurrentCell.mBoardPosition.y;
        Console.WriteLine("currentY =", currentY);

        //LEFT
        MatchesState(currentX - 2, currentY + (1 * filpper));
        //UPPER LEFT
        MatchesState(currentX - 1, currentY + (2 * filpper));

        //RIGHT
        MatchesState(currentX + 2, currentY + (1 * filpper));
        //UPPER RIGHT
        MatchesState(currentX + 1, currentY + (2 * filpper));
    }

    protected override void CheckPathing()
    {
        //Draw top half
        CreateCellPath(1);

        //Draw down half
        CreateCellPath(-1);
    }

    private void MatchesState(int targetX, int targetY)
    {
        CellState cellState = CellState.None;
        cellState = mCurrentCell.mBoard.ValidateCell(targetX, targetY, this);
        if (cellState != CellState.Friendly && cellState != CellState.OutOfBounds)
        {
            //If free or enemy, eat the cell
            mHighlightedCells.Add(mCurrentCell.mBoard.mAllCells[targetX, targetY]);
        }
    }
}
