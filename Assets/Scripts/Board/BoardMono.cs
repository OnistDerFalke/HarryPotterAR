using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMono : MonoBehaviour
{
    public CoordinatesConverter coordinatesConverter;
    public BoardVisulalizer boardVisualiser;
    public MultiVuMarkHandler charactersHandler;

    public Board Board { get;  set; }


    public Field GetOccupiedField(Vector3 pos)
    {
        Vector2 boardSpacePos = coordinatesConverter.WorldToBoard(pos);
        foreach(Field f in Board.Fields)
        {
            if(f.Figure.ContainsPosition(boardSpacePos))
            {
                // assuming that fields don't overlap
                return f;
            }
        }
        return null;
    }

    void Awake()
    {
       
    }

    void Update()
    {
        foreach(string character in charactersHandler.CurrentTrackedObjects)
        {
            int index = charactersHandler.availableIds.IndexOf(character);
            if(index != -1)
            {
                Vector3 characterPos = charactersHandler.models[index].transform.position;
                Field f = GetOccupiedField(characterPos);
                Game.Player player = GameManager.Players.Find((e) => e.Character == Game.Player.CharacterFromString(character));
                if (f != null && player != null)
                {
                    if (player.LastFieldId != f.Index)
                    {
                        player.ChangeField(f.Index);
                    }
                }
            }
        }
    }
}
