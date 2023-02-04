using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Pickable:MonoBehaviour
{

    public bool IsPickable = true;
    public UnityEvent PickedUp;
    public Container Container = null;

    public float JumpPower = 1f;
    public float Speed = 1f;

    private void OnTriggerEnter(Collider other)
    {

        var container = other.GetComponentInParent<Container>();
        
        if (IsPickable && container)
        {

            HandlePickup(container);

        }
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        
        var other = collision.collider;
        var container = other.GetComponentInParent<Container>();

        if (IsPickable && container)
        {
            HandlePickup(container);
        }
    }
    
    virtual protected void HandlePickup(Container container)
    {
        StartCoroutine(PickupSequence(container));
    }
    
    private IEnumerator PickupSequence(Container container)
    {
        Container = container;
        IsPickable = false;
        PickedUp.Invoke();

        gameObject.layer = LayerMask.NameToLayer("No Collision");

        //var children = GetComponentsInChildren<Transform>(includeInactive: true);

        foreach (Transform child in transform)
        {
   
            child.gameObject.layer = LayerMask.NameToLayer("No Collision");
        }


        if (TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.isKinematic = true;
        }

        transform.SetParent(null);

        transform.SetParent(container.transform);
        container.Item = gameObject;

        yield return transform.DOLocalJump(Vector3.zero, JumpPower, 1, Speed).SetSpeedBased().WaitForCompletion();

        Destroy(this);
        
    }

}
