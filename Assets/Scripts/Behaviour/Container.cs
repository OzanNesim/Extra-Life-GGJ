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
            PickupSequence(PickableItem);
            return true;
        }
        else
        {

            if (Item.Type != PickableItem.Type)
            {
                Item.transform.parent = null;
                Item.PutOnCooldown();
                Item.IsPickable = true;

                Item.gameObject.layer = LayerMask.NameToLayer("Default");

                foreach (Transform child in Item.transform)
                {

                    child.gameObject.layer = LayerMask.NameToLayer("Default");
                }

                Item = null;

                PickupSequence(PickableItem);

                return true;
            }

            return false;
        }
        
    }


    private void PickupSequence(Pickable Item)
    {

        Item.IsPickable = false;
        Item.gameObject.layer = LayerMask.NameToLayer("No Collision");

        foreach (Transform child in Item.transform)
        {

            child.gameObject.layer = LayerMask.NameToLayer("No Collision");
        }


        if (Item.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.isKinematic = true;
        }

        Item.transform.SetParent(Location.transform);
        this.Item = Item;

        Item.transform.DOLocalJump(Vector3.zero, 1, 1, 1).SetSpeedBased().WaitForCompletion();
        PutOnCooldown();
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
