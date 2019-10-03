using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bishop : BasePiece
{
    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        //Base Setup
        base.Setup(newTeamColor, newSpriteColor, newPieceManager);

        // Rook stuff
        mMovement = new Vector3Int(0, 0, 7); //Can move Horizonytal and vertical 
        GetComponent<Image>().sprite = Resources.Load<Sprite>("T_Bishop");

    }
}
