using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornersHider : MonoBehaviour
{
    [SerializeField] private GameObject toHide;
    [SerializeField] private CoordinatesConverter converter;

    void Update()
    {
        if(converter.IsTrackingBoard() && !toHide.activeInHierarchy)
        {
            toHide.SetActive(true);
        }
        else if(!converter.IsTrackingBoard() && toHide.activeInHierarchy)
        {
            toHide.SetActive(false);
        }
    }
}
