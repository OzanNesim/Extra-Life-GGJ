using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class Pickable:MonoBehaviour
{

    public bool IsPickable = true;
    public bool OnCooldown = false;

    public Container Container = null;

    public ItemType Type;

    public float JumpPower = 1f;
    public float Speed = 1f;
    public float CooldownTime = 2f;



    private void OnTriggerEnter(Collider other)
    {

        var container = other.GetComponentInParent<Container>();
        
        if (IsPickable && container && !OnCooldown)
        {

            HandlePickup(container);

        }
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        
        var other = collision.collider;
        var container = other.GetComponentInParent<Container>();

        if (IsPickable && container && !OnCooldown)
        {
            HandlePickup(container);
        }
    }
    
    virtual protected void HandlePickup(Container container)
    {
        if (!container.Pick(this))
        {
            PutOnCooldown();
        }

    }
    
    public void PutOnCooldown()
    {

        StartCoroutine(CooldownCycle());
        /*OnCooldown = true;

        Invoke(nameof(ResetCooldown), CooldownTime);

        var rigidbody = GetComponentInParent<Rigidbody>();

        if (rigidbody)
        {
            rigidbody.AddForce(Vector3.up * 2f, ForceMode.VelocityChange);
            rigidbody.isKinematic = true;
        }

        gameObject.layer = LayerMask.NameToLayer("NoCol");

        foreach (Transform child in transform)
        {

            child.gameObject.layer = LayerMask.NameToLayer("NoCol");
        }*/

    }

    private IEnumerator CooldownCycle()
    {
        OnCooldown = true;

        var rigidbody = GetComponentInParent<Rigidbody>();

        if (rigidbody)
        {
            rigidbody.isKinematic = true;
        }
        /*
        gameObject.layer = LayerMask.NameToLayer("NoCol");

        foreach (Transform child in transform)
        {

            child.gameObject.layer = LayerMask.NameToLayer("NoCol");
        }*/

        yield return transform.DOLocalJump(transform.position,2f,1,CooldownTime).WaitForCompletion();

        if (rigidbody)
        {
            rigidbody.isKinematic = false;
        }
        /*
        gameObject.layer = LayerMask.NameToLayer("Default");

        foreach (Transform child in transform)
        {

            child.gameObject.layer = LayerMask.NameToLayer("Default");
        }
        */


        OnCooldown = false;
    }

    private void ResetCooldown()
    {
        /*
        var rigidbody = GetComponentInParent<Rigidbody>();



        OnCooldown = false;*/
    }

}

public enum ItemType
{
    Empty,Fruit,Pickaxe,Bucket
}
