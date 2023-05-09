using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
[System.Serializable]
public class Direction
{
    public int x;
    public int y;
    public Direction (int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
public enum Team
{
    None,
    AI,
    Person
}
public enum NameChess
{
    Pawn,
    Knight,
    Rooks,
    King,
    Queen
}
public class Piece : MonoBehaviour 
{
    public Team team;
 
    public int score;
    public List <Direction> DirCanMove= new List<Direction>();
    public List <Direction> DirCanMove3= new List<Direction>();
    
    public int x;
    public int y; 
    public bool Bechosed;
    public bool BeOut;
    public Board board;
    public bool isMovingDone;
    private void OnEnable()
    {
        if (team == Team.Person) return;
        
    }
    public Square squareCur { get; set; }
    public float duration = 1;
    public void SetPos()
    {
       //gameObject.transform.position = squareCur.transform.position;
        transform.DOMove(squareCur.transform.position, duration).From(transform.position);
    }
    public bool Move(Square square) {

        if (square.beUsed == Team.None)
        {
            if (squareCur != null)
            {
                squareCur.beUsed = Team.None;
                squareCur.piece = null;
            }
            squareCur = square;
            square.beUsed = team;
            square.SetPiece(this);
            return true;
        }
        else if (square.beUsed != team)
        {
            if (squareCur != null)
            {
                squareCur.beUsed = Team.None;
                squareCur.piece = null;
            }
            squareCur = square;
            square.beUsed = team;
            square.piece.BeOut = true;
            square.piece.gameObject.SetActive(false);
            square.SetPiece(this);
            return true;
        }
        else
        {
            return false;
        }
    }
   
    public void ShowSquresCanMove()
    {
        foreach (Direction dir in DirCanMove3)
        {
            Controller.instance.board.squares[dir.x].rowSquares[dir.y].ShowCanMoveIn();
        }
    }
    public void HideShowSquresCanMove()
    {
        foreach (Direction dir in DirCanMove3)
        {
            Controller.instance.board.squares[dir.x].rowSquares[dir.y].PieceExit();
        }
    }
    public bool MoveAI(Square square)
    {
      
        if (square.beUsed == Team.None)
        {
            if (squareCur != null)
            {
                squareCur.beUsed = Team.None;
                squareCur.piece = null;
            }
            squareCur = square;
            square.beUsed = team;
            square.SetPiece(this);
            return true;
        }
        else if (square.beUsed != team)
        {
            if (squareCur != null)
            {
                squareCur.beUsed = Team.None;
                squareCur.piece = null;
            }
            squareCur = square;
            square.beUsed = team;
            square.piece.BeOut = true;
            square.SetPiece(this);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void test()
    {
        ShowDirCanMove(Controller.instance.board);
    }
    public List<Direction> ShowDirCanMove(Board board)
    {
        List<Direction> DirCanMove2 = new List<Direction>();
        MoveStraight(board, DirCanMove2,2);
        MoveCross(board, DirCanMove2);
        DirCanMove3 = DirCanMove2;
        return DirCanMove2;
    }
    public void MoveStraight(Board board, List<Direction> DirCanMove2, int numStep)
    {
        for (int i = x+1; i < 8; i++)
        {
            if (board.squares[i].rowSquares[y].beUsed == Team.None)
            {
                DirCanMove2.Add(new Direction(i, y));
            }
            else if (board.squares[i].rowSquares[y].beUsed != team)
            {
                DirCanMove2.Add(new Direction(i, y));
                break;
            }
            else
            {
                break;
            }

        }
        for (int i = x - 1; i >= 0; i--)
        {
            if (board.squares[i].rowSquares[y].beUsed == Team.None)
            {
                DirCanMove2.Add(new Direction(i, y));
            }
            else if (board.squares[i].rowSquares[y].beUsed != team)
            {
                DirCanMove2.Add(new Direction(i, y));
                break;
            }
            else
            {
                break;
            }


        }
        for (int i = y + 1; i < 8; i++)
        {
            if (board.squares[x].rowSquares[i].beUsed == Team.None)
            {
                DirCanMove2.Add(new Direction(x, i));
            }
            else if (board.squares[x].rowSquares[i].beUsed != team)
            {
                DirCanMove2.Add(new Direction(x, i));
                break;
            }
            else
            {
                break;
            }
            //   DirCanMove.Add(new Direction(x, i));
        }
        for (int i = y - 1; i >=0 ; i--)
        {
            if (board.squares[x].rowSquares[i].beUsed != Team.None)
            {
                DirCanMove2.Add(new Direction(x, i));
            }
            else if (board.squares[x].rowSquares[i].beUsed != team)
            {
                DirCanMove2.Add(new Direction(x, i));
                break;
            }
            else
            {
                break;
            }

        }
    }
    public void MoveCross(Board board, List<Direction> DirCanMove2)
    {
        bool b1= true, b2= true;
        int cnt = 0;
        for (int i = x + 1; i < 8 ; i++)
        {
            cnt++;
            int j = y + cnt;
            if (b1&& j<8)
            {
                if (board.squares[i].rowSquares[j].beUsed == Team.None)
                {
                    DirCanMove2.Add(new Direction(i, j));
                }
                else if (board.squares[i].rowSquares[j].beUsed != team )
                {
                    DirCanMove2.Add(new Direction(i, j));
                    b1 = false;

                }
                else
                {
                    break;
                }
            }
            j = y - cnt;
            if (b2 &&j >=0)
            {
               
                if (board.squares[i].rowSquares[j].beUsed == Team.None)
                {
                    DirCanMove2.Add(new Direction(i, j));
                }
                else if (board.squares[i].rowSquares[j].beUsed != team)
                {
                    DirCanMove2.Add(new Direction(i, j));
                    b2 = false;
                }
                else
                {
                    break;
                }
            }

            if (!b1 && !b2) break;
            

        }
        b1 = true; b2 = true;
        cnt = 0;
        for (int i = x - 1; i >= 0; i--)
        {
            cnt++;

            int j = y + cnt;
            if (b1&& j<8)
            {
                if (board.squares[i].rowSquares[j].beUsed == Team.None)
                {
                    DirCanMove2.Add(new Direction(i, j));
                }
                else if (board.squares[i].rowSquares[j].beUsed != team)
                {
                    DirCanMove2.Add(new Direction(i, j));
                    b2 = false;
                }
                else
                {
                    break;
                }
            }
         
            j = y - cnt;
            if (b2&& j >=0)
            {
                if (board.squares[i].rowSquares[j].beUsed == Team.None)
                {
                    DirCanMove2.Add(new Direction(i, j));
                }
                else if (board.squares[i].rowSquares[j].beUsed != team)
                {
                    DirCanMove2.Add(new Direction(i, j));
                    b2 = false;
                }
                else
                {
                    break;
                }

            }
            if (!b1 && !b2) break;
        }
       
    }
    public void  Attacked(Piece piece)
    {

    }
    public void BeAttacked()
    {
        BeOut = true;

        gameObject.SetActive(false);
      
        Debug.Log(team+"Be Attacked" );
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        isMovingDone = false;
        ShowDirCanMove(Controller.instance.board);
        ShowSquresCanMove();
    }
    public bool findDirCanMove(int x, int y)
    {
        foreach (Direction i in DirCanMove3)
        {
            if (i.x == x && i.y == y) return true;
        }
        return false;
    }
    private void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        gameObject.transform.position = worldPos;
    }
    private void OnMouseUp()
    {
        isMovingDone = true;
        // ShowDirCanMove(Controller.instance.board);
        HideShowSquresCanMove();
    }

}
