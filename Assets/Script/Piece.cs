using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    public int x, y;
    public bool Bechosed;
    public bool BeOut;
    public Board board;
    public Square squareCur;
    public void SetPos()
    {
       gameObject.transform.position = squareCur.transform.position;
    }
    public bool Move(Square square) {

        x = square.x;
        y = square.y;
        if (squareCur != null)
        {
            squareCur.beUsed = Team.None;
            squareCur.piece = null;
        }
        squareCur = square;
        if (square.beUsed == Team.None)
        {
            square.beUsed = team;

            square.SetPiece(this);
            return true;
        }
        else if (square.beUsed != team)
        {

            square.beUsed = team;
            Debug.Log(square.x + " " + square.y);
            square.piece.BeAttacked();
            square.SetPiece(this);
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool MoveAI(Square square)
    {
        if (squareCur != null)
        {
            squareCur.beUsed = Team.None;
        }
        squareCur = square;
        if (square.beUsed == Team.None)
        {
            
            square.SetPiece(this);
            return true;
        }
        else if (square.beUsed != team)
        {
          
             
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
    public void ShowDirCanMove(Board board)
    {
        DirCanMove = new List<Direction>();
        MoveStraight(board);
        MoveCross(board);
    }
    public void MoveStraight(Board board)
    {
        for (int i = x+1; i < 8; i++)
        {
            
            DirCanMove.Add(new Direction(i, y));
        }
        for (int i = 0  ; i < x-1; i++)
        {
            DirCanMove.Add(new Direction(i, y));
        }
        for (int i = y + 1; i < 8; i++)
        {
            DirCanMove.Add(new Direction(x, i));
        }
        for (int i = 0; i < y - 1; i++)
        {
            DirCanMove.Add(new Direction(x, i));

        }
    }
    public void MoveCross(Board board)
    {
        int cnt = 0;
        for (int i = x + 1; i < 8; i++)
        {
            cnt++;
            int j = y + cnt;
            DirCanMove.Add(new Direction(i, j));
            j = y - cnt;
            DirCanMove.Add(new Direction(i, j));
        }
        cnt = 0;
        for (int i = 0; i < x - 1; i++)
        {
            cnt++;
            int j = y + cnt;
            DirCanMove.Add(new Direction(i, j));
            j = y - cnt;
            DirCanMove.Add(new Direction(i, j));
        }
       
    }
    public void  Attacked(Piece piece)
    {

    }
    public void BeAttacked()
    {
        BeOut = false;
        Debug.Log(team+"Be Attacked" );
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        
    }
    private void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        gameObject.transform.position = worldPos;
    }
     
}
