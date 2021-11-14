using UnityEngine;
using UnityEngine.EventSystems;

public class CursorInputModule : BaseInputModule
{
    [SerializeField] private Cursor[] cursors;

    private PointerEventData data;
    private bool[] preStates;

    public override void ActivateModule()
    {
        data = new PointerEventData(eventSystem);
        preStates = new bool[cursors.Length];
    }

    public override void Process()
    {
        for (var i = 0; i < cursors.Length; i++)
        {
            var cursor = cursors[i];
            var preState = preStates[i];

            if (cursor)
            {
                data.Reset();
                data.position = Camera.main.WorldToScreenPoint(cursor.transform.position);
                eventSystem.RaycastAll(data, m_RaycastResultCache);

                data.button = PointerEventData.InputButton.Left;
                data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
                var hitObject = data.pointerCurrentRaycast.gameObject;

                if (!preState && cursor.Clicked)
                {
                    HandleClickEvents(data, hitObject);
                }

                HandlePointerExitAndEnter(data, hitObject);

                preStates[i] = cursor.Clicked;
                m_RaycastResultCache.Clear();
            }
        }
    }

    private void HandleClickEvents(PointerEventData currentPointerData, GameObject target)
    {
        ExecuteEvents.ExecuteHierarchy(target, GetBaseEventData(), ExecuteEvents.submitHandler);
    }

    public override bool ShouldActivateModule()
    {
        var res = false;
        foreach (var cursor in cursors)
        {
            if (cursor.gameObject.activeInHierarchy)
            {
                res = true;
                break;
            }
        }

        return res;
    }
}