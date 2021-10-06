using UnityEngine;
using UnityEngine.EventSystems;

public class VRInputModule : BaseInputModule
{
    [SerializeField] private Pointer pointerL = default, pointerR = default;

    private PointerEventData data;
    private bool preStateL = false, preStateR = false;

    public override void ActivateModule()
    {
        data = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        if (pointerL && pointerL.Hit)
        {
            data.Reset();
            data.position = Camera.main.WorldToScreenPoint(pointerL.Target.point);
            eventSystem.RaycastAll(data, m_RaycastResultCache);

            data.button = PointerEventData.InputButton.Left;
            data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
            var hitObject = data.pointerCurrentRaycast.gameObject;
            //var oldPos = pointerData.position;
            //pointerData.position = position;
            //pointerData.delta = pointerData.position - pointerData.position;
            //pointerData.scrollDelta = Vector3.zero;

            if (!preStateL && pointerL.Clicked)
            {
                HandleClickEvents(data, hitObject);
                HandleDragEvents(data, hitObject);
            }

            HandlePointerExitAndEnter(data, hitObject);

            preStateL = pointerL.Clicked;
            m_RaycastResultCache.Clear();
        }


        if (pointerR || pointerR.Hit)
        {
            data.Reset();
            data.position = Camera.main.WorldToScreenPoint(pointerR.Target.point);
            eventSystem.RaycastAll(data, m_RaycastResultCache);

            data.button = PointerEventData.InputButton.Left;
            data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
            var hitObject = data.pointerCurrentRaycast.gameObject;
            //var oldPos = pointerData.position;
            //pointerData.position = position;
            //pointerData.delta = pointerData.position - pointerData.position;
            //pointerData.scrollDelta = Vector3.zero;

            if (!preStateR && pointerR.Clicked)
            {
                HandleClickEvents(data, hitObject);
                HandleDragEvents(data, hitObject);
            }

            HandlePointerExitAndEnter(data, hitObject);

            preStateR = pointerR.Clicked;
            m_RaycastResultCache.Clear();
        }
    }

    private void HandleClickEvents(PointerEventData currentPointerData, GameObject target)
    {
        ExecuteEvents.ExecuteHierarchy(target, GetBaseEventData(), ExecuteEvents.submitHandler);
    }

    private void HandleDragEvents(PointerEventData currentPointerData, GameObject target)
    {
        // drag start
        // drag

        currentPointerData.pointerPressRaycast = currentPointerData.pointerCurrentRaycast;
        var res = ExecuteEvents.ExecuteHierarchy(target, data, ExecuteEvents.dragHandler);
        // drag end
    }
}