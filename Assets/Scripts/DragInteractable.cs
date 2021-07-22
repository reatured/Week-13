using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[Serializable]
public class DragEvent : UnityEvent<float> { }

public class DragInteractable : XRBaseInteractable
{
    public Transform startDragPosition = null;
    public Transform endDragPosition = null;

    public float dragPercent = 0.0f;

    protected XRBaseInteractor m_interactor = null;

    public UnityEvent onDragStart = new UnityEvent();
    public UnityEvent onDragEnd = new UnityEvent();
    public DragEvent onDragUpdate = new DragEvent();

    Coroutine m_drag = null;

    void StartDrag()
    {
        Debug.Log("StartDrag");
        if(m_drag != null)
        {   
            StopCoroutine(m_drag);
        }
        m_drag = StartCoroutine(CalculateDrag());
        onDragStart?.Invoke();
        
    }

    void EndDrag()
    {
        Debug.Log("StartDrag");
        if (m_drag != null)
        {
            StopCoroutine(m_drag);
            m_drag = null;
            onDragEnd?.Invoke();
        }

        

    }

    public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value) 
    {
        Debug.Log("StartDrag");
        Vector3 AB = b - a;
        Vector3 AV = value - a;
        return Mathf.Clamp01(Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB));
    }

    IEnumerator CalculateDrag()
    {
        while(m_interactor != null)
        {
            Debug.Log("Calculation");
            Vector3 line = startDragPosition.localPosition - endDragPosition.localPosition;

            Vector3 InteractorLocalPosition = startDragPosition.parent.InverseTransformPoint(m_interactor.transform.position);


            Vector3 projectedPoint = Vector3.Project(InteractorLocalPosition, line.normalized);

            dragPercent = InverseLerp(startDragPosition.localPosition, endDragPosition.localPosition, projectedPoint);


            onDragUpdate?.Invoke(dragPercent);

            yield return null;
        }
    }

    [Obsolete]
    protected override void OnSelectEntering(XRBaseInteractor interactor)
    {   
        m_interactor = interactor;
        StartDrag();
        base.OnSelectEntering(interactor);
    }

    [Obsolete]
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        EndDrag();
        m_interactor = null;
        base.OnSelectExited(interactor);

    }





}
