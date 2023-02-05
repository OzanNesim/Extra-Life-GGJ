using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour
{

    public Transform Reference;

    private void OnTriggerEnter(Collider other)
    {
        var Pickaxe = other.GetComponentInParent<Pickaxe>();

        if (Pickaxe)
        {

           HandleMine(Pickaxe);

        }

    }

    protected virtual void HandleMine(Pickaxe pickaxe)
    {

        StartCoroutine(PickSequence(pickaxe));
    }

    private IEnumerator PickSequence(Pickaxe pickaxe)
    {
        var target = pickaxe.transform;

        if (pickaxe.TryGetComponent(out Container container))
        {
            if (container.Item == pickaxe.gameObject)
            {
                container.Item = null;
            }
        }

        Destroy(pickaxe);


        if (target.TryGetComponent(out Rigidbody rigidbody))
        {
            Debug.Log("Turned to kinetic");
            rigidbody.isKinematic = true;
        }

        if (TryGetComponent(out rigidbody))
        {
            Debug.Log("Turned to kinetic");
            rigidbody.isKinematic = true;
        }

        target.SetParent(Reference.transform);

        var sequence = DOTween.Sequence();

        sequence.Append(target.DOLocalMove(Vector3.zero, 0.66f).SetEase(Ease.InBounce));
        sequence.Join(target.DOLocalRotate(Vector3.zero, 0.66f).SetEase(Ease.InBounce));

        yield return sequence.WaitForCompletion();

        for (int i = 0; i < 4; i++)
        {
            yield return target.DOLocalRotate(new Vector3(0f, 0f, 70f), 0.3f).WaitForCompletion();
            yield return target.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.1f).WaitForCompletion();
            yield return new WaitForSeconds(0.15f);
        }
        Destroy(target.gameObject);
        Destroy(gameObject);
        //yield return target.DOMove(Vector3.zero, 0.4f).SetEase(Ease.InExpo);

        // yield return target.DOLocalJump(Vector3.zero, 1f, 1, 1f).SetSpeedBased().WaitForCompletion();

        //Debug.Log("Made Money:" + value);
        //Destroy(target.gameObject);


    }
}
