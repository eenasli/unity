using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pawn : BasePiece
{
    public bool mIsFirstMove = true;

    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        //Base Setup
        base.Setup(newTeamColor, newSpriteColor, newPieceManager);

        //Reset
        mIsFirstMove = true;

        // Rook stuff
        mMovement = mColor == Color.white ? new Vector3Int(0, 1, 1) : new Vector3Int(0, -1, -1); //If it's white move upp, black move down
        GetComponent<Image>().sprite = Resources.Load<Sprite>("T_Pawn");
    }

    protected override void Move()
    {
        base.Move();
        mIsFirstMove = false; // Because it can only move twice
    }

    private bool MatchesState(int targetX, int targetY, CellState targetState)
    {
        CellState cellState = CellState.None;
        cellState = mCurrentCell.mBoard.ValidateCell(targetX, targetY, this);
        if(cellState == targetState)
        {
            mHighlightedCells.Add(mCurrentCell.mBoard.mAllCells[targetX, targetY]);
            return true;
        }
        return false;
    }

    protected override void CheckPathing()
    {
        // Target Position
        int CurrentX = mCurrentCell.mBoardPosition.x;
        int CurrentY = mCurrentCell.mBoardPosition.y;

        //Top left
        MatchesState(CurrentX - mMovement.z, CurrentY + mMovement.z, CellState.Enemy);

        //Forward
        if(MatchesState(CurrentX, CurrentY + mMovement.y, CellState.Free))
        {
            //If the first forward cell is free, and first move, check for next
            if (mIsFirstMove)
            {
                MatchesState(CurrentX, CurrentY + (mMovement.y *2) , CellState.Free);
            }
        }

        //Top right
        MatchesState(CurrentX + mMovement.z, CurrentY + mMovement.z, CellState.Enemy);
    }
}
