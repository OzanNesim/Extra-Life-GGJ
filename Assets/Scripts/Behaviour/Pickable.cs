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
        
        if (IsPickable && other.TryGetComponent(out Container container))
        {
            Debug.Log("Picked Up");
            
            StartCoroutine(PickupSequence( container));
        }
        
    }

    private IEnumerator PickupSequence(Container container)
    {
        Container = container;
        IsPickable = false;
        PickedUp.Invoke();
        

        transform.SetParent(null);

        transform.SetParent(container.transform);
        container.Item = gameObject;

        yield return transform.DOLocalJump(Vector3.zero, JumpPower, 1, Speed).SetSpeedBased().WaitForCompletion();

    }

}
