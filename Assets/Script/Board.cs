using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    public List<RowSquare> squares;
    public List<Piece> PiecesAI;
    public List<Piece> PiecesPer;
    // Start is called before the first frame update
    
    public void CloneSquares(Board board)
    {
        board.squares = new List<RowSquare>();
        foreach (RowSquare i in squares)
        {
            RowSquare rq = Instantiate(i, Controller.instance.Grabage.transform);
            rq.rowSquares = new List<Square>();
            
            board.squares.Add(rq);
        }
    }
    public Square CheckCanMoveToPos(int x , int y, Team team )
    {
        if (!Checkindext(x, y)) return null;
        if (squares[x].rowSquares[y].beUsed  != team )
        {
            return squares[x].rowSquares[y];
        }
        else return null;
    }
    
    public void MovePiece (Piece piece, int xs, int ys , int xd, int yd , Team _team)
    {
       
        if (!Checkindext(xd, yd)) return ;
        squares[xs].rowSquares[ys].beUsed = Team.None ;
        piece.MoveAI(squares[xd].rowSquares[yd]);

        squares[xd].rowSquares[yd].beUsed = _team;
        if (squares[xd].rowSquares[yd].piece != null)
        {

            squares[xd].rowSquares[yd].piece.BeOut = true ;
            
        }
        squares[xd].rowSquares[yd].piece = squares[xs].rowSquares[ys].piece;
        squares[xd].rowSquares[yd].piece.squareCur = squares[xd].rowSquares[yd];
        squares[xs].rowSquares[ys].piece = null ;
       
        //squares[xd].rowSquares[yd].piece?.BeAttacked();
    }
    public bool Checkindext(int x , int y)
    {
        if (x >= 8 || y >= 8 || x < 0 || y < 0) return false;
        return true;
    }
    void Start()
    {
        
    }
    public int Evaluate(Team team)
    {
        int _score = 0;
        foreach (Piece i in PiecesAI)
        {
            if (i.team == team && !i.BeOut)
            {
                _score += i.score;
            }
             
        }
        foreach (Piece i in PiecesPer)
        {
            if (i.team == team&&!i.BeOut)
            {
                _score -= i.score;
            }

        }
        Debug.Log(_score);
        return _score;
    }
    public Square getSquare (int xs, int ys)
    {
        
        return squares[xs].rowSquares[ys];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
