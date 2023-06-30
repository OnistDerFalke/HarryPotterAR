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
            EventBroadcaster.InvokeOnMarkLost(id);
        }
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        var id = GetComponent<VuMarkBehaviour>().InstanceId.StringValue;
        if (!currentTrackedObjects.Contains(id))
        {
            int modelIndex = availableIds.IndexOf(id);
            models[modelIndex].SetActive(true);
            currentTrackedObjects.Add(id);
            EventBroadcaster.InvokeOnMarkDetected(id);
        }
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        var vmb = GetComponent<VuMarkBehaviour>();
        if (vmb.InstanceId != null)
        {
            string id = vmb.InstanceId.StringValue;
            UntrackModel(id);
        }
    }

    protected override void HandleTargetStatusChanged(Status previousStatus, Status newStatus)
    {
        base.HandleTargetStatusChanged(previousStatus, newStatus);

        if(newStatus == Status.EXTENDED_TRACKED)
        {
            var vmb = GetComponent<VuMarkBehaviour>();
            if (vmb.InstanceId != null)
            {
                string id = vmb.InstanceId.StringValue;
                UntrackModel(id);
            }
        }
    }
}
