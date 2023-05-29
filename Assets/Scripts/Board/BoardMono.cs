using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMono : MonoBehaviour
{
    [SerializeField] private CoordinatesConverter coordinatesConverter;
    [SerializeField] private BoardVisulalizer boardVisualiser;

    public Board Board { get;  set; }

    void Awake()
    {
       
    }

    void Update()
    {
        
    }
}
