using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Board mBoard;
    public PieceManager mPieceManager;

    // Start the Game
    void Start()
    {
        //Create Board
        mBoard.Create();
        //Create pieces with PieceManager
        mPieceManager.Setup(mBoard);
    }

}
