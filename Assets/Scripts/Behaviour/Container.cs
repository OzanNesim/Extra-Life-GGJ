using DG.Tweening;
using System.Collections;
using UnityEngine;

// Faciliates pickup behaviour

public class Container : MonoBehaviour
{

    public Pickable Item = null;
    public Transform Location = null;
    public bool OnCooldown = false;

    public bool Pick(Pickable PickableItem)
    {
        if (OnCooldown) return false;

        if (!Item)
        {
            //PickupSequence(PickableItem);
            StartCoroutine(PickupSequence(PickableItem));
            return true;
        }
        else
        {

            if (Item.Type != PickableItem.Type)
            {
                Item.transform.parent = null;
                Item.PutOnCooldown();
                Item.IsPickable = true;



                //StartCoroutine(PickupSequence(PickableItem));
                StartCoroutine(PickupSequence(PickableItem));

                return true;
            }

            return false;
        }
        
    }


    private IEnumerator PickupSequence(Pickable Item)
    {

        Item.IsPickable = false;

        /*
        Item.gameObject.layer = LayerMask.NameToLayer("No Collision");

        foreach (Transform child in Item.transform)
        {

            child.gameObject.layer = LayerMask.NameToLayer("No Collision");
        }
        */

        if (Item.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.isKinematic = true;
        }

        Item.transform.SetParent(Location.transform);
        this.Item = Item;

        PutOnCooldown();

        //Item.transform.DOLocalJump(Vector3.zero, 1, 1, 1).SetSpeedBased().WaitForCompletion();

       // yield return Item.transform.DOLocalJump(Vector3.zero, 1, 1, 1).SetSpeedBased().WaitForCompletion();

        var seq = Item.transform.DOLocalJump(Vector3.zero, 1, 1, 1).SetSpeedBased();
        yield return seq.Join(Item.transform.DOLocalRotate(Vector3.zero, 1)).WaitForCompletion();
        
    }

    public void PutOnCooldown()
    {
        OnCooldown = true;

        Invoke(nameof(ResetCooldown), 1f);

    }
    private void ResetCooldown()
    {
        OnCooldown = false;
    }
}
