using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInitializer : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Let it go");
        GameManager.Setup();
    }
}
