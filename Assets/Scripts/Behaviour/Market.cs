using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        var sellable = other.GetComponentInParent<Sellable>();

        if (sellable)
        {

            HandleSell(sellable);

        }

    }

    protected virtual void HandleSell(Sellable sellable)
    {

        Debug.Log("Purchase it!");
        StartCoroutine(SellSequence(sellable));
    }

    private IEnumerator SellSequence(Sellable sellable)
    {
        var target = sellable.transform;
        var value = sellable.Value;
        Destroy(sellable);


        if (target.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.isKinematic = true;
        }

        target.SetParent(transform);

        yield return target.DOLocalJump(Vector3.zero, 1f, 1, 1f).SetSpeedBased().WaitForCompletion();

        Debug.Log("Made Money:" + value);
    }

}
