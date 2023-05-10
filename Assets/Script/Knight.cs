using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override List<Direction> ShowDirCanMove(Board board)
    {
        List<Direction> DirCanMove2 = new List<Direction>();

        int j = 2;
        for (int i = 1; i <=2; i++)
        {
            if (Checkindext(x+i, y - j))
            {
                if (board.squares[x + i].rowSquares[y - j].beUsed != team)

                    DirCanMove2.Add(new Direction(x+i, y- j));

            }
            if (Checkindext(x + i, y + j))
            {

                if (board.squares[x + i].rowSquares[y + j].beUsed != team)
                    DirCanMove2.Add(new Direction(x+i, y + j));
            }
            if (Checkindext(x - i, y - j))
            {
                if (board.squares[x - i].rowSquares[y - j].beUsed != team)
                    DirCanMove2.Add(new Direction(x - i, y - j));
            }
            if (Checkindext(x - i, y + j))
            {
                if (board.squares[x-i].rowSquares[y+j].beUsed != team)
                    DirCanMove2.Add(new Direction(x - i, y + j));
               
                   
            }
            j--;
        }
        DirCanMove3 = DirCanMove2;
        return DirCanMove2;
    }
    public bool Checkindext(int x, int y)
    {

        if (x >= 8 || y >= 8 || x < 0 || y < 0) return false;
        return true;
    }

    // Update is called once per frame

}
