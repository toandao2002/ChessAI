using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_MiniMax : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        
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
            
            foreach (Direction dir in i.DirCanMove)
            {
                Board board = Controller.instance.board;
                board.squares[0].rowSquares[0].tmp = 12;
                square = board.CheckCanMoveToPos(i.x + dir.x, i.y+ dir.y,Team.AI);
               
                if (square != null)
                {

                    int squareS =(int) board.getSquare(i.x, i.y).beUsed;
                    Debug.Log(i.x+" "+ i.y);
                    int  squareD = (int) board.getSquare(i.x + dir.x, i.y + dir.y).beUsed;
                    Piece pieceS = board.getSquare(i.x, i.y).piece; ;
                    Piece pieceD = board.getSquare(i.x + dir.x, i.y + dir.y).piece;
                    Square Ssquare = pieceS.squareCur;
                    Square DsquareS =null;
                    int xs = pieceS.x;
                    int ys = pieceS.y;
                    int xd =0, yd=0;
                    if (pieceD != null)
                    {
                          xd = pieceD.x;
                          yd = pieceD.y;
                           DsquareS = pieceD.squareCur;
                    }
                   
                    bool beOutT = false;
                    beOutT = board.getSquare(i.x + dir.x, i.y + dir.y).piece != null ? board.getSquare(i.x + dir.x, i.y + dir.y).piece : true;
                    
                    //pieceS.MoveAI(board.getSquare(i.x + dir.x, i.y + dir.y));
                    board.MovePiece(pieceS,i.x, i.y, i.x + dir.x, i.y + dir.y, Team.AI);
                    int tmp  = Mathf.Max(max, MiniMax(board, 4, Team.Person));
                     pieceS.x = xs ;
                    pieceS.y = ys;
                    if (pieceD != null)
                    {
                        pieceD.x = xd;
                        pieceD.y = yd;pieceD.squareCur = DsquareS;
                    }
                    pieceS.squareCur = Ssquare;
                    
                    board.getSquare(i.x, i.y).beUsed =(Team) squareS;
                    board.getSquare(i.x + dir.x, i.y + dir.y).beUsed = (Team) squareD;
                  
                    board.getSquare(i.x, i.y).piece = pieceS;
                    board.getSquare(i.x + dir.x, i.y + dir.y).piece = pieceD;
                    if (board.getSquare(i.x + dir.x, i.y + dir.y).piece)
                    {
                        board.getSquare(i.x + dir.x, i.y + dir.y).piece.BeOut = beOutT;

                    }
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
            int max = 0;
       
            foreach (Piece i in  board.PiecesAI)
            {
                
                foreach (Direction dir in i.DirCanMove)
                {
                    square = board.CheckCanMoveToPos(i.x + dir.x ,i.y+ dir.y, Team.AI);
                    if (square != null )
                    {

                        int squareS = (int)board.getSquare(i.x, i.y).beUsed;
                        int squareD = (int)board.getSquare(i.x+  dir.x, i.y + dir.y).beUsed;
                        Piece pieceS = board.getSquare(i.x, i.y).piece;
                        Piece pieceD= board.getSquare(i.x + dir.x, i.y + dir.y).piece;
                        if (pieceS == null)
                        {
                            Debug.Log(i.x + " " + i.y);
                            Debug.Log(i.squareCur.x + " " + i.squareCur.y);
                        }
                        Square Ssquare = pieceS.squareCur;
                       
                        Square DsquareS =null;
                        int xs = pieceS.x;
                        int ys = pieceS.y;
                        int xd = 0, yd = 0;
                        if (pieceD != null)
                        {
                            xd = pieceD.x;
                            yd = pieceD.y;
                            DsquareS = pieceD.squareCur;
                        }
                        bool beOutT = false;
                        beOutT = board.getSquare(i.x + dir.x, i.y + dir.y).piece!= null ? board.getSquare(i.x + dir.x, i.y + dir.y).piece.BeOut: true ;
                        //pieceS.MoveAI(board.getSquare(i.x + dir.x, i.y + dir.y));
                        board.MovePiece(pieceS,i.x, i.y, i.x + dir.x, i.y + dir.y, Team.AI);
                        max = Mathf.Max(max, MiniMax(board, d - 1, Team.Person ) );

                        pieceS.x = xs;
                        pieceS.y = ys;
                        if (pieceD != null)
                        {
                            pieceD.x = xd;
                            pieceD.y = yd;

                            pieceD.squareCur = DsquareS;
                        }
                        pieceS.squareCur = Ssquare;
                        
                        board.getSquare(i.x, i.y).beUsed =(Team) squareS;
                        board.getSquare(i.x + dir.x, i.y + dir.y).beUsed = (Team)squareD;
                        board.getSquare(i.x, i.y).piece = pieceS;
                        board.getSquare(i.x + dir.x, i.y + dir.y).piece= pieceD;

                
                        if (board.getSquare(i.x + dir.x, i.y + dir.y).piece!=null)
                        {
                           
                            board.getSquare(i.x + dir.x, i.y + dir.y).piece.BeOut = beOutT;
                         
                        }
                        
                    }
                    
                }
               
            }
             
            return max;
        }
        else
        {

            int min = 0;
           
            foreach (Piece i in  board.PiecesPer)
            {
                foreach (Direction dir in i.DirCanMove)
                {
                    square = board.CheckCanMoveToPos(i.x + dir.x, i.y + dir.y, Team.Person);
                    if (square != null)
                    {

                        int squareS = (int)board.getSquare(i.x, i.y).beUsed;
                        int   squareD = (int)board.getSquare(i.x + dir.x, i.y + dir.y).beUsed;
                        Piece pieceS = board.getSquare(i.x, i.y).piece;
                        Piece pieceD = board.getSquare(i.x + dir.x, i.y + dir.y).piece;
                        Square Ssquare = pieceS.squareCur;
                        Square DsquareS = null;
                        int xs = pieceS.x;
                        int ys = pieceS.y;
                        int xd = 0, yd = 0;
                        if (pieceD != null)
                        {
                            xd = pieceD.x;
                            yd = pieceD.y;
                            DsquareS = pieceD.squareCur;
                        }
                        bool beOutT = false;
                        beOutT = board.getSquare(i.x + dir.x, i.y + dir.y).piece != null ? board.getSquare(i.x + dir.x, i.y + dir.y).piece : true;
                        //pieceS.MoveAI(board.getSquare(i.x + dir.x, i.y + dir.y));
                        board.MovePiece(pieceS,i.x, i.y, i.x + dir.x, i.y + dir.y, Team.Person);
                        min = Mathf.Min(min, MiniMax(board,d - 1, Team.AI));

                        pieceS.x = xs;
                        pieceS.y = ys;
                        if (pieceD != null)
                        {
                            pieceD.x = xd;
                            pieceD.y = yd;
                            pieceD.squareCur = DsquareS;
                        }
                        pieceS.squareCur = Ssquare;
                       
                        board.getSquare(i.x, i.y).beUsed = (Team)squareS;
                        board.getSquare(i.x + dir.x, i.y + dir.y).beUsed =(Team) squareD;
                        board.getSquare(i.x, i.y).piece = pieceS;
                        board.getSquare(i.x + dir.x, i.y + dir.y).piece = pieceD;
                        if (board.getSquare(i.x + dir.x, i.y + dir.y).piece)
                        {
                            board.getSquare(i.x + dir.x, i.y + dir.y).piece.BeOut = beOutT;

                        }
                       
                    }

                }
               
            }
           
            return min;
        }

    }
}
