using System;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    [HideInInspector]
    public bool mIsKingAlive = true;

    public GameObject piecePrefab;

    private List<BasePiece> mWhitePieces = null;
    private List<BasePiece> mBlackPieces = null;

    private string[] mPieceOrder = new string[16]
    {
        "P","P","P","P","P","P","P","P",
        "R", "KN", "B", "K", "Q","B", "KN", "R"
    };

    private Dictionary<string, Type> mPieceLibrary = new Dictionary<string, Type>()
    {
        {"P", typeof(Pawn)},
        {"R", typeof (Rook)},
        {"KN" ,typeof(Knight)},
        {"B" ,typeof(Bishop)},
        {"K", typeof(King)},
        {"Q", typeof(Queen)}
    };

    public void Setup(Board board)
    {
        //Create white piece
        mWhitePieces = CreatePieces(Color.white, new Color32(80, 124, 159, 255), board);
        // Create Black piece
        mBlackPieces = CreatePieces(Color.black, new Color32(210, 95, 64, 255), board);

        // Place pieces on board
        PlacePieces(1, 0, mWhitePieces, board);
        PlacePieces(6, 7, mBlackPieces, board);

        //White goes first
        //Switch sides

    }

    private List<BasePiece> CreatePieces(Color teamColor, Color32 spriteColor, Board board)
    {
        List<BasePiece> newPieces = new List<BasePiece>();

        for (int i = 0; i < mPieceOrder.Length; i++)
        {
            //Create new object
            GameObject newPieceObject = Instantiate(piecePrefab);
            newPieceObject.transform.SetParent(transform);

            // Set scale and position
            newPieceObject.transform.localScale = new Vector3(1, 1, 1);
            newPieceObject.transform.localRotation = Quaternion.identity;

            //Get the type, apply to new object
            string key = mPieceOrder[i];
            Type pieceType = mPieceLibrary[key];

            //Store piece
            BasePiece newPiece = (BasePiece)newPieceObject.AddComponent(pieceType);
            newPieces.Add(newPiece);

            //Setup piece
            newPiece.Setup(teamColor, spriteColor, this);
        }
        return newPieces;
    }

    private void PlacePieces(int pawnRow, int royaltyRow, List<BasePiece> pieces, Board board)
    {
        for (int i = 0; i < 8; i++)
        {
            //Place Pawns
            pieces[i].Place(board.mAllCells[i, pawnRow]);
            //Place Royalty
            pieces[i + 8].Place(board.mAllCells[i, royaltyRow]);
        }
    }

    private void SetIntractive(List<BasePiece> allPiece, bool value)
    {
        foreach (BasePiece piece in allPiece)
            piece.enabled = value;
    }

    public void switchSides(Color color)
    {
        if (!mIsKingAlive)
        {
            //Reset Pieces
            ResetPiece();

            //King has risen from the dead
            mIsKingAlive = true;

            //Change color to black, so white can go first again
            color = Color.black;
        }

        bool isBlackTurn = color == Color.white ? true : false;

        //Set interactivity
        SetIntractive(mWhitePieces, !isBlackTurn);
        SetIntractive(mBlackPieces, isBlackTurn);
    }

    public void ResetPiece()
    {
        foreach (BasePiece piece in mWhitePieces)
            piece.Reset();

        foreach (BasePiece piece in mWhitePieces)
            piece.Reset();
    }
}

