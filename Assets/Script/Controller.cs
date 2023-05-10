using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller instance;

    public Board board;
    public GameObject Grabage;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        for  (int i = 0; i < board.squares.Count;i ++)
        {
            for (int j = 0; j < board.squares[i].rowSquares.Count; j ++)
            {
                /*board.squares[i].rowSquares[j].x = i;
                board.squares[i].rowSquares[j].y = j;*/
                board.squares[i].rowSquares[j].SetPos(i, j);
            }
        }
    }
}
