using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   
    public override List<Direction> ShowDirCanMove(Board board)
    {
        List<Direction> DirCanMove2 = new List<Direction>();
        if (firtMove)
        {
            MoveStraight(board, DirCanMove2, 2);
            firtMove = false;
        }
            
        else
        {
                    MoveStraight(board, DirCanMove2, 1);
        }
        MoveCross(board, DirCanMove2, 1);
        DirCanMove3 = DirCanMove2;
        return DirCanMove2;
    }
    public override void MoveStraight(Board board, List<Direction> DirCanMove2, int numStep)
    {
        int num = 1;
        if (team == Team.AI)
            for (int i = x + 1; i < 8; i++)
        {
            if (num > numStep) break;
            num++;
            if (board.squares[i].rowSquares[y].beUsed == Team.None)
            {
                DirCanMove2.Add(new Direction(i, y));
            }
            else if (board.squares[i].rowSquares[y].beUsed != team)
            {
                //DirCanMove2.Add(new Direction(i, y));
                break;
            }
            else
            {
                break;
            }

        }
        num = 1;
        if (team == Team.Person)
            for (int i = x - 1; i >= 0; i--)
        {
            if (num > numStep) break;
            num++;
            if (board.squares[i].rowSquares[y].beUsed == Team.None)
            {
                DirCanMove2.Add(new Direction(i, y));
            }
            else if (board.squares[i].rowSquares[y].beUsed != team)
            {
               // DirCanMove2.Add(new Direction(i, y));
                break;
            }
            else
            {
                break;
            }


        }
         
    }
    public override void MoveCross(Board board, List<Direction> DirCanMove2, int numStep)
    {
        int num = 1;
        bool b1 = true, b2 = true;
        int cnt = 0;
        if (team == Team.AI)
            for (int i = x + 1; i < 8; i++)
            {
                cnt++;
                if (num > numStep) break;
                num++;
                int j = y + cnt;
                if (b1 && j < 8)
                {
                     if (board.squares[i].rowSquares[j].beUsed != team && board.squares[i].rowSquares[j].beUsed != Team.None)
                    {
                        DirCanMove2.Add(new Direction(i, j));
                        b1 = false;

                    }
                    else
                    {
                        b1 = false;
                    }
                }
                j = y - cnt;
                if (b2 && j >= 0)
                {

                   if (board.squares[i].rowSquares[j].beUsed != team && board.squares[i].rowSquares[j].beUsed != Team.None)
                    {
                        DirCanMove2.Add(new Direction(i, j));
                        b2 = false;
                    }
                    else
                    {
                        b2 = false;
                    }
                }

                if (!b1 && !b2) break;


            }
        b1 = true; b2 = true;
        cnt = 0;
        num = 1;
        if (team == Team.Person)
            for (int i = x - 1; i >= 0; i--)
            {
                cnt++;
                if (num > numStep) break;
                num++;
                int j = y + cnt;
                if (b1 && j < 8)
                {
                   if (board.squares[i].rowSquares[j].beUsed != team && board.squares[i].rowSquares[j].beUsed != Team.None)
                    {
                        DirCanMove2.Add(new Direction(i, j));
                        b1 = false;
                    }
                    else
                    {
                        b1 = false;
                    }
                }

                j = y - cnt;
                if (b2 && j >= 0)
                {
                     if (board.squares[i].rowSquares[j].beUsed != team && board.squares[i].rowSquares[j].beUsed != Team.None)
                    {
                        DirCanMove2.Add(new Direction(i, j));
                        b2 = false;
                    }
                    else
                    {
                        b2 = false;
                    }

                }
                if (!b1 && !b2) break;
            }

    }
}
