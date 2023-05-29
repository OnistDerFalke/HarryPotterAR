using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestReader : MonoBehaviour
{
    [SerializeField] private BoardMono firstBoard;
    [SerializeField] private BoardMono secondBoard;
    [SerializeField] private BoardMono thirdBoard;

    private void ConsumeRequest(Request request)
    {
        if(request is BoardInitializedRequest)
        {
            firstBoard.Board = GameManager.BoardManager.Boards[0];
            secondBoard.Board = GameManager.BoardManager.Boards[1];
            thirdBoard.Board = GameManager.BoardManager.Boards[2];
            Debug.Log($"third board size: {thirdBoard.Board.Size}");
        }
    }

    void Update()
    {
        if(RequestBroker.requests.Count > 0)
        {
            Request r = RequestBroker.requests[0];
            ConsumeRequest(r);
            RequestBroker.requests.RemoveAt(0);
        }
    }
}
