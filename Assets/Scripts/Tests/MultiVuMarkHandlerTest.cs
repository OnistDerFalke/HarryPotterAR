using UnityEngine;
using Vuforia;
using System.Collections.Generic;

public class MultiVuMarkHandlerTest : DefaultObserverEventHandler
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

    private void Awake()
    {
        VuMarkBehaviour myVuMarkBehaviour = GetComponent<VuMarkBehaviour>();
        // turn off cloned objects
        foreach (GameObject model in models)
        {
            if (model.activeInHierarchy)
            {
                string id = availableIds[models.IndexOf(model)];
                if (!TestManager.CurrentTrackedObjects.ContainsKey(id))
                {
                    continue;
                }
                VuMarkBehaviour trackingInstance = TestManager.CurrentTrackedObjects[id];
                if (trackingInstance != myVuMarkBehaviour)
                {
                    model.SetActive(false);
                }
            }
        }
    }

    private void UntrackModel(string id)
    {
        if(TestManager.CurrentTrackedObjects.ContainsKey(id))
        {
            int modelIndex = availableIds.IndexOf(id);
            models[modelIndex].SetActive(false);
            TestManager.UpdateDetected(id, false);
        }
    }

    private void UntrackAll()
    {
        foreach(var obj in TestManager.CurrentTrackedObjects)
        {
            int modelIndex = availableIds.IndexOf(obj.Key);
            models[modelIndex].SetActive(false);
        }
    }

    private void TrackModel(string id, VuMarkBehaviour vmb)
    {
        if (!TestManager.CurrentTrackedObjects.ContainsKey(id))
        {
            int modelIndex = availableIds.IndexOf(id);
            models[modelIndex].SetActive(true);
            TestManager.UpdateDetected(id, true, vmb);
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
        if (TestManager.DetectingStarted)
        {
            Debug.Log("Hi");
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
        else
        {
            UntrackAll();
        }
    }
}
