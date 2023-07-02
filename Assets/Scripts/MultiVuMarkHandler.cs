using UnityEngine;
using Vuforia;
using System.Collections.Generic;
using Assets.Scripts;

public class MultiVuMarkHandler : DefaultObserverEventHandler
{
    [SerializeField] public List<string> availableIds = new List<string>();
    [SerializeField] public List<GameObject> models = new List<GameObject>();

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
        if(GameManager.CurrentTrackedObjects.ContainsKey(id))
        {
            int modelIndex = availableIds.IndexOf(id);
            models[modelIndex].SetActive(false);
            GameManager.CurrentTrackedObjects.Remove(id);
            EventBroadcaster.InvokeOnMarkerLost(id);
        }
    }

    private void TrackModel(string id, VuMarkBehaviour vmb)
    {
        if (!GameManager.CurrentTrackedObjects.ContainsKey(id))
        {
            Debug.Log($"Element {id} has VuMarkBehaviour {vmb} on position {vmb.transform.position}");

            int modelIndex = availableIds.IndexOf(id);
            models[modelIndex].SetActive(true);
            GameManager.CurrentTrackedObjects.Add(id, vmb);
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

        Debug.Log($"VuMarkBehaviour {vmb} position : {vmb.transform.position}");

        switch (newStatus)
        {
            case Status.NO_POSE:
                UntrackModel(id);
                break;
            case Status.LIMITED:
                UntrackModel(id);
                break;
            case Status.TRACKED:
                TrackModel(id, vmb);
                break;
            case Status.EXTENDED_TRACKED:
                UntrackModel(id);
                break;
        }
    }
}
