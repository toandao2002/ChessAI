using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
  
    public override List<Direction> ShowDirCanMove(Board board)
    {
        List<Direction> DirCanMove2 = new List<Direction>();
        MoveStraight(board, DirCanMove2, 1);
        MoveCross(board, DirCanMove2, 1);
        DirCanMove3 = DirCanMove2;
        return DirCanMove2;
    }
}
