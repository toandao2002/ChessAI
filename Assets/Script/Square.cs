using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Square : MonoBehaviour
{
    public Team beUsed;
    public Piece piece;
    public int x, y;
    public int tmp = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (piece!=null)
        piece.squareCur = this;
    }
    public void SetPiece(Piece piece)
    {
        this.piece = piece;
        piece.x = x; piece.y = y;
         
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chess"))
        {
            GetComponent<SpriteRenderer>().color = new Color32(0,0,0,255);
           
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
