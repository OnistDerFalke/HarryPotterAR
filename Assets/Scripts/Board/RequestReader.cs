using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestReader : MonoBehaviour
{
    private void ConsumeRequest(Request request)
    {
        if(request is BoardInitializedRequest)
        {
            Debug.Log("Board initialized");
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
