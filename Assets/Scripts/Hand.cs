using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit;

public enum HandType
{
    Left,
    Right
};
public class Hand : MonoBehaviour
{
    public TextMeshProUGUI debugText = null;
    public float isTracked; 
    public HandType type = HandType.Left;
    public bool isHidden
    {
        get;
        private set;
    } = false;

    public InputAction trackedAction = null;
    //
    public bool m_isCurrentlyTracked = false;

    public List<SkinnedMeshRenderer> m_currentRenderers2 = new List<SkinnedMeshRenderer>();

    public Collider[] m_colliders = null;


    public List<Collider> colliderArray = new List<Collider>();
    public bool isCollisionEnabled { get; private set; } = false;


    public XRBaseInteractor interactor = null;

    private void Awake()
    {
        if(interactor == null)
        {
            interactor = GetComponentInParent<XRBaseInteractor>();
        }
    }

    [System.Obsolete]
    private void OnEnable()
    {
        interactor.onSelectEntered.AddListener(OnGrab);
        interactor.onSelectExited.AddListener(onRelease);
    }

    [System.Obsolete]
    private void OnDisable()
    {
        interactor.onSelectEntered.RemoveListener(OnGrab);
        interactor.onSelectExited.AddListener(onRelease);
    }



    void Start()
    {
        m_colliders = GetComponentsInChildren<Collider>();
        foreach(Collider collider in m_colliders)
        {
            if (!collider.isTrigger)
            {
                colliderArray.Add(collider);
            }
        }
        trackedAction.Enable();
        hide();
    }

    // Update is called once per frame
    void Update()
    {
        isTracked = trackedAction.ReadValue<float>();

        debugText.text = "isTracked: " + isTracked + "\nisEnabled: "+colliderArray[1].enabled;

        if(isTracked == 1.0f && !m_isCurrentlyTracked)
        {
            m_isCurrentlyTracked = true;
            show();
        }else if(isTracked == 0 && m_isCurrentlyTracked)
        {
            m_isCurrentlyTracked = false;
            hide();
        }
    }

    public void show()
    {

        foreach (SkinnedMeshRenderer renderer in m_currentRenderers2)
        {
            renderer.enabled = true;
        }
        isHidden = false;
        EnableCollisions(true);
    }

    public void hide()
    {
        
        m_currentRenderers2.Clear();
        SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        //MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach(SkinnedMeshRenderer renderer in renderers)
        {
            renderer.enabled = false;
            m_currentRenderers2.Add(renderer);
        }
        isHidden = true;
        EnableCollisions(false);
    }

    public void EnableCollisions(bool enabled)
    {
        if (isCollisionEnabled == enabled) return;

        isCollisionEnabled = enabled;
        foreach(Collider collider in colliderArray)
        {
            collider.enabled = isCollisionEnabled;
        }
    }

    void OnGrab(XRBaseInteractable grabbedObject)
    {
        Debug.Log("Grabbing stuff");
        HandControl ctrl = grabbedObject.GetComponent<HandControl>();
        Debug.Log(ctrl.name);
        if (ctrl != null)
        {
            if (ctrl.hideHand)
            {
                hide();
                Debug.Log("HideHand");
            }
        }
    }

    void onRelease(XRBaseInteractable releaseObject)
    {
        HandControl ctrl = releaseObject.GetComponent<HandControl>();
        if (ctrl != null)
        {
            if (ctrl.hideHand)
            {
                show();
            }
        }
    }
}
