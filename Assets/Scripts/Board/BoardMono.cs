using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMono : MonoBehaviour
{
    [SerializeField] private int id;
    private Board board;

    void Awake()
    {
       
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            board = GameManager.BoardManager.Boards[id];
            Debug.Log($"{board.Fields.Count} fields");
        }
    }
}
