using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestReader : MonoBehaviour
{
    [SerializeField] private List<BoardMono> boards;

    private void ConsumeRequest(Request request)
    {
        if(request is HighlightFieldRequest)
        {
            HighlightFieldRequest highlightRequest = request as HighlightFieldRequest;
            boards[highlightRequest.field.BoardId].boardVisualiser.HighlightField(highlightRequest.field);
        }
        else if(request is UnhighlightFieldRequest)
        {
            UnhighlightFieldRequest unhighlightRequest = request as UnhighlightFieldRequest;
            boards[unhighlightRequest.field.BoardId].boardVisualiser.UnhighlightField(unhighlightRequest.field);
        }
        else if(request is BoardInitializedRequest)
        {
            for(int i = 0; i < boards.Count; i++)
            {
                boards[i].Board = GameManager.BoardManager.Boards[i];
            }
            GameManager.BoardManager.HighlightAllFields(); // testing
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
