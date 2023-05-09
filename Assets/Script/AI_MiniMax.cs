using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_MiniMax : MonoBehaviour
{

    public Piece piece1;
    public Piece Piece2;
    // Start is called before the first frame update
    void Start()
    {
        Piece2 = piece1;
        piece1 = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MovePiece()
    {
        int max = int.MinValue;
        
        for (int i = 0 ; i < Controller.instance.Grabage.transform.childCount; i++){
            Destroy(Controller.instance.Grabage.transform.GetChild(i).gameObject);
        }
        Square square = null;
        Square squareTarget = null;
        Piece pieceTarget = null;
        int cntt = 0;
        foreach (Piece i in Controller.instance.board.PiecesAI)
        {
            if (i.BeOut) continue;
            List<Direction> DirCanMove2= i.ShowDirCanMove(Controller.instance.board);
            
            foreach (Direction dir in  DirCanMove2)
            {
                Board board = Controller.instance.board;
               
                square = board.CheckCanMoveToPos( dir.x, dir.y,Team.AI);
               
                if (square != null)
                {

                    int xs = i.x;
                    int ys = i.y;
                    int xd = square.x;
                    int yd = square.y;
                    int squareS = (int)board.getSquare(xs, ys).beUsed;
                    int squareD = (int)square.beUsed;
                    Piece pieceD = square.piece;
 
                    //pieceS.MoveAI(board.getSquare(i.x + dir.x, i.y + dir.y));
                    i.MoveAI(square);
                    int tmp  =  MiniMax(board, 3, Team.Person);

                    i.BeOut = false;
                    if (pieceD != null)
                    {
                        pieceD.BeOut = false;
                    }
                    board.getSquare(xs, ys).SetPiece(i);

                    square.SetPiece(pieceD);
 
                     Debug.Log(max + " " + tmp);
                    if (max < tmp)
                    {
                        max = tmp;
                        squareTarget = square;
                        pieceTarget = i;
                    }
                   
                }

            }

        }
        Debug.Log("Done");
        pieceTarget.Move(squareTarget);
        Debug.Log(squareTarget.beUsed);
        pieceTarget.SetPos();

    }
    public int MiniMax(Board board, int d, Team team )
    {
        if (d <= 0) return board.Evaluate(team);
        Square square;
        if (team == Team.AI)
        {
            int max = int.MinValue;
       
            foreach (Piece i in  board.PiecesAI)
            {
                if (i.BeOut) continue;
                List<Direction> DirCanMove2 = i.ShowDirCanMove( board);
                foreach (Direction dir in DirCanMove2)
                {
                    square = board.CheckCanMoveToPos(dir.x , dir.y, Team.AI);
                    if (square != null )
                    {
                        int xs = i.x;
                        int ys = i.y;
                        int xd = square.x;
                        int yd = square.y;
                        int squareS = (int)board.getSquare(xs, ys).beUsed;
                        if (i.squareCur != board.getSquare(xs, ys))
                        {
                            Square tmp = board.getSquare(xs, ys);
                            Debug.Log("d");
                        }
                        int squareD = (int)board.getSquare(xd, yd).beUsed;
                        Piece pieceD= board.getSquare(xd, yd).piece;
                  
                        //pieceS.MoveAI(board.getSquare(i.x + dir.x, i.y + dir.y));
                        i.MoveAI(square);
                        max = Mathf.Max(max, MiniMax(board, d - 1, Team.Person ) );

                        i.BeOut = false;
                        if (pieceD != null)
                        {
                            pieceD.BeOut = false;
                        }
                        board.getSquare(xs, ys).SetPiece(i);
                        square.SetPiece(pieceD);

 
                    }
                    
                }
               
            }
             
            return max;
        }
        else
        {

            int min = int.MaxValue;
           
            foreach (Piece i in  board.PiecesPer)
            {
                if (i.BeOut) continue;
                List<Direction> DirCanMove2 = i.ShowDirCanMove(board);
                foreach (Direction dir in DirCanMove2)
                {
                    square = board.CheckCanMoveToPos(dir.x, dir.y, Team.Person);
                    if (square != null)
                    {
                        int xs = i.x;
                        int ys = i.y;
                        int xd = square.x;
                        int yd = square.y;
                        int squareS = (int)board.getSquare(xs, ys).beUsed;
                        int squareD = (int)board.getSquare(xd, yd).beUsed;
                        Piece pieceD = board.getSquare(xd, yd).piece;

                        //pieceS.MoveAI(board.getSquare(i.x + dir.x, i.y + dir.y));
                        i.MoveAI(square);
              
                        min = Mathf.Min(min, MiniMax(board,d - 1, Team.AI));
                        i.BeOut = false;
                        if (pieceD != null)
                        {
                            pieceD.BeOut = false;
                        }
                        board.getSquare(xs, ys).SetPiece(i);
                        square.SetPiece(pieceD);
 

                    }

                }
               
            }
           
            return min;
        }
        return 0;
    }
}
