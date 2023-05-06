using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayersSwipe : MonoBehaviour
    {
        [SerializeField] private Text PlayersNumberText;
        [SerializeField] private float MinDistanceForSwipe = 20f;
        [SerializeField] private int[] PlayersRange = new int[2];
    
        private Vector2 fingerDownPosition, fingerUpPosition;
        private int numberOfPlayers;

        void Start()
        {
            numberOfPlayers = PlayersRange[0];
            UpdateContext();
        }
        void Update()
        {
            SwipeCheck();
        }

        private void UpdateContext()
        {
            PlayersNumberText.text = numberOfPlayers.ToString();
        }

        private void SwipeCheck()
        {
            foreach (var touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        fingerDownPosition = touch.position;
                        fingerUpPosition = touch.position;
                        break;
                    case TouchPhase.Moved:
                        fingerUpPosition = touch.position;
                        break;
                    case TouchPhase.Ended:
                    {
                        fingerUpPosition = touch.position;

                        if (fingerDownPosition != fingerUpPosition)
                        {
                            var swipeDistance = (fingerDownPosition - fingerUpPosition).magnitude;
                            if (swipeDistance > MinDistanceForSwipe)
                            {
                                var swipeDirection = fingerUpPosition - fingerDownPosition;
                                if (swipeDirection.x < 0)
                                {
                                    //Left swipe handle
                                    numberOfPlayers++;
                                    if (numberOfPlayers > PlayersRange[1]) 
                                        numberOfPlayers = PlayersRange[1];
                                }
                                else
                                {
                                    //Right swipe handle
                                    numberOfPlayers--;
                                    if (numberOfPlayers < PlayersRange[0]) 
                                        numberOfPlayers = PlayersRange[0];
                                }
                                
                                UpdateContext();
                            }
                        }
                        break;
                    }
                }
            }
        }
    }
}



