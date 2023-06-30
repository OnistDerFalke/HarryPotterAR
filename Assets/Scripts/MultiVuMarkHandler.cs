using UnityEngine;
using Vuforia;
using System.Collections.Generic;

public class MultiVuMarkHandler : DefaultObserverEventHandler
{
    private List<string> currentTrackedObjects = new List<string>();
    [SerializeField] public List<string> availableIds = new List<string>();
    [SerializeField] public List<GameObject> models = new List<GameObject>();

    public List<string> CurrentTrackedObjects { get => currentTrackedObjects; }

    public GameObject FindModelById(string id)
    {
        if (!availableIds.Contains(id)) return null;

        int index = availableIds.FindIndex((i) => i == id);
        if(index < models.Count)
        {
            return models[index];
        }
        return null;
    }

    private void UntrackModel(string id)
    {
        if(currentTrackedObjects.Contains(id))
        {
            int modelIndex = availableIds.IndexOf(id);
            models[modelIndex].SetActive(false);
            currentTrackedObjects.Remove(id);
            EventBroadcaster.InvokeOnMarkerLost(id);
        }
    }

    private void TrackModel(string id)
    {
        if (!currentTrackedObjects.Contains(id))
        {
            int modelIndex = availableIds.IndexOf(id);
            models[modelIndex].SetActive(true);
            currentTrackedObjects.Add(id);
            EventBroadcaster.InvokeOnMarkerDetected(id);
        }
    }

    protected override void HandleTargetStatusChanged(Status previousStatus, Status newStatus)
    {
        base.HandleTargetStatusChanged(previousStatus, newStatus);

        var vmb = GetComponent<VuMarkBehaviour>();
        if (vmb.InstanceId == null)
        {
            Debug.LogWarning("For some reason there is no vu mark behaviour on target status change");
            return;
        }
        string id = vmb.InstanceId.StringValue;

        switch (newStatus)
        {
            case Status.NO_POSE:
                UntrackModel(id);
                break;
            case Status.LIMITED:
                UntrackModel(id);
                break;
            case Status.TRACKED:
                TrackModel(id);
                break;
            case Status.EXTENDED_TRACKED:
                UntrackModel(id);
                break;
        }
    }
}
