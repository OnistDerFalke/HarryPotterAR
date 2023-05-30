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
        base.OnTrackingFound();
        var id = GetComponent<VuMarkBehaviour>().InstanceId.StringValue;
        if (!currentTrackedObjects.Contains(id))
        {
            currentTrackedObjects.Add(id);
            Debug.Log($"Detected a character: {id}");
        }
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        var vmb = GetComponent<VuMarkBehaviour>();
        if (vmb.InstanceId != null)
        {
            currentTrackedObjects.Remove(vmb.InstanceId.StringValue);
            Debug.Log($"Lost tracking on character: {vmb.InstanceId.StringValue}");
        }
    }

    void Update()
    {
        foreach(var elem in currentTrackedObjects)
        {
            foreach (var model in models)
            {
                model.SetActive(false);
            }
                
            if(availableIds.IndexOf(elem)<models.Count)
            {
                models[availableIds.IndexOf(elem)].SetActive(true);
            }
        }
    }
}
