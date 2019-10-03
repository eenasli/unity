﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class King : BasePiece
{
    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        //Base Setup
        base.Setup(newTeamColor, newSpriteColor, newPieceManager);

        // Rook stuff
        //mMovement = new Vector3Int(7, 7, 7); //She can move in all directions
        GetComponent<Image>().sprite = Resources.Load<Sprite>("T_King");
    }

}
