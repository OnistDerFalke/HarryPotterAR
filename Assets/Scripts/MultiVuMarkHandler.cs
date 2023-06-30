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

    protected override void OnTrackingFound()
    {
        Debug.Log(gameObject.name);
        base.OnTrackingFound();
        var id = GetComponent<VuMarkBehaviour>().InstanceId.StringValue;
        if (!currentTrackedObjects.Contains(id))
        {
            Debug.Log($"ID: {id}");
            int modelIndex = availableIds.IndexOf(id);
            models[modelIndex].SetActive(true);
            currentTrackedObjects.Add(id);
            Debug.Log($"Detected a character: {id}");
            EventBroadcaster.InvokeOnMarkDetected(id);
        }
    }

    protected override void OnTrackingLost()
    {
        Debug.Log(gameObject.name);
        base.OnTrackingLost();
        var vmb = GetComponent<VuMarkBehaviour>();
        if (vmb.InstanceId != null)
        {
            string id = vmb.InstanceId.StringValue;
            int modelIndex = availableIds.IndexOf(id);
            models[modelIndex].SetActive(false);
            currentTrackedObjects.Remove(id);
            Debug.Log($"Lost tracking on marker: {id}");
            EventBroadcaster.InvokeOnMarkLost(id);
        }
    }
}
