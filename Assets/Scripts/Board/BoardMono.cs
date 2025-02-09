using Assets.Scripts;
using Game;
using System.Collections.Generic;
using UnityEngine;

public class BoardMono : MonoBehaviour
{
    public CoordinatesConverter coordinatesConverter;
    public BoardVisulalizer boardVisualiser;
    public MultiVuMarkHandler charactersHandler;
    public int id;

    public Board Board { get;  set; }
    public bool IsTracked => isTracked;
    public List<string> CurrentTrackedBoardMarks => currentTrackedBoardMarks;

    public bool isTracked = false;
    private List<string> currentTrackedBoardMarks = new();
    private List<Character> managedCharacters = new();


    public Field GetOccupiedField(Vector3 pos)
    {
        Vector2 boardSpacePos = coordinatesConverter.WorldToBoard(pos);

        foreach (Field f in Board.Fields)
        {
            if (f.Figure.ContainsPosition(boardSpacePos))
            {
                // assuming that fields don't overlap
                return f;
            }
        }
        return null;
    }

    private void OnMarkerDetected(string id)
    {
        if (coordinatesConverter.BoardMarkIds.Contains(id))
        {
            if (currentTrackedBoardMarks.Count == 0)
                EventBroadcaster.InvokeOnBoardDetected(Board.Id);
            currentTrackedBoardMarks.Add(id);
        }
    }

    private void OnMarkerLost(string id)
    {
        if (currentTrackedBoardMarks.Contains(id))
        {
            currentTrackedBoardMarks.Remove(id);
            if (currentTrackedBoardMarks.Count == 0)
                EventBroadcaster.InvokeOnBoardLost(Board.Id);
        }
    }

    private void OnBoardDetected(int id)
    {
        if(id == Board.Id)
        {
            isTracked = true;
            coordinatesConverter.enabled = true;
            boardVisualiser.enabled = true;
            boardVisualiser.ShowVisuals();
        }
    }

    private void OnBoardLost(int id)
    {
        if (id == Board.Id)
        {
            isTracked = false;
            coordinatesConverter.enabled = false;
            boardVisualiser.HideVisuals();
            boardVisualiser.enabled = false;
        }
    }

    private void Awake()
    {
        Board = GameManager.BoardManager.Boards[id];
    }

    private void OnEnable()
    {
        EventBroadcaster.OnMarkDetected += OnMarkerDetected;
        EventBroadcaster.OnMarkLost += OnMarkerLost;
        EventBroadcaster.OnBoardDetected += OnBoardDetected;
        EventBroadcaster.OnBoardLost += OnBoardLost;
    }

    private void OnDisable()
    {
        EventBroadcaster.OnMarkDetected -= OnMarkerDetected;
        EventBroadcaster.OnMarkLost -= OnMarkerLost;
        EventBroadcaster.OnBoardDetected -= OnBoardDetected;
        EventBroadcaster.OnBoardLost -= OnBoardLost;
    }

    private void PlacePlayers()
    {
        managedCharacters.Clear();
        foreach (var vumark in GameManager.CurrentTrackedObjects)
        {
            Character character = Player.CharacterFromString(vumark.Key);
            if (character != Character.None && !managedCharacters.Contains(character))
            {
                managedCharacters.Add(character);
                Vector3 characterPos = charactersHandler.models[charactersHandler.availableIds.IndexOf(vumark.Key)].transform.position;
                Vector3 offset = charactersHandler.transform.position - vumark.Value.transform.position;


                Field f = GetOccupiedField(characterPos + offset);
                Player player = GameManager.Players.Find((e) => e.Character == character);
                if (f != null && player != null)
                {
                    if (!player.IsDuringMove && player.LastFieldId != f.Index)
                    {
                        player.LastFieldId = f.Index;
                    }
                }
            }
        }
    }

    void Update()
    {
        if (isTracked)
        {
            PlacePlayers();
        }
    }
}
