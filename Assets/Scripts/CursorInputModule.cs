using UnityEngine;
using UnityEngine.EventSystems;

public class CursorInputModule : BaseInputModule
{
    [SerializeField] private Cursor[] cursors;

    private PointerEventData[] data;
    private bool[] preStates;

    public override void ActivateModule()
    {
        data = new PointerEventData[] { new PointerEventData(eventSystem), new PointerEventData(eventSystem) };
        preStates = new bool[cursors.Length];
    }

    public override void Process()
    {
        for (var i = 0; i < cursors.Length; i++)
        {
            var cursor = cursors[i];
            var preState = preStates[i];
            var datum = data[i];

            if (cursor)
            {
                datum.Reset();
                datum.position = Camera.main.WorldToScreenPoint(cursor.transform.position);
                eventSystem.RaycastAll(datum, m_RaycastResultCache);

                datum.button = PointerEventData.InputButton.Left;
                datum.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
                var hitObject = datum.pointerCurrentRaycast.gameObject;

                if (!preState && cursor.Clicked)
                {
                    HandleClickEvents(datum, hitObject);
                }


                HandlePointerExitAndEnter(datum, hitObject);

                preStates[i] = cursor.Clicked;
                m_RaycastResultCache.Clear();
            }

            data[i] = datum;
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