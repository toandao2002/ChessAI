using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public Team beUsed;
    public Piece piece;
    public int x;
    public int y; 
    public int tmp = 0;
    private void OnEnable()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
        if (piece!=null)
        piece.squareCur = this;
    }
    public void SetPos(int _x , int _y)
    {
        x = _x; y = _y;
        if (piece != null)
        {
            piece.x = x; piece.y = y;
        }
    }
    public void SetPiece(Piece piece)
    {
       
        if (piece != null)
        {
            
            piece.squareCur = this;
            piece.x = x; piece.y = y;
            beUsed = piece.team;
        }
        else
        {
            beUsed = Team.None;
        }
        this.piece = piece;
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowCanMoveIn()
    {
        GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
    }
    public void PieceExit()
    {
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chess"))
        {
            GetComponent<SpriteRenderer>().color = new Color32(0,0,0,255);
            if (!collision.GetComponent<Piece>().findDirCanMove(x, y))
            {
                GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
            }
            if (collision.GetComponent<Piece>().isMovingDone)
            {

               /* if (collision.GetComponent<Piece>().squareCur.piece.findDirCanMove(x,y))
                {
                    GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
                }*/
                
                {
                    collision.GetComponent<Piece>().squareCur.piece = null;
                    collision.GetComponent<Piece>().squareCur.beUsed = Team.None;
                    if (piece != null) piece.BeAttacked();
                    SetPiece(collision.GetComponent<Piece>());
                }
             
            }
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chess"))
        {
            GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
    }

}
