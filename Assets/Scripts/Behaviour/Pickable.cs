using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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
        OnCooldown = true;

        Invoke(nameof(ResetCooldown), CooldownTime);

        var rigidbody = GetComponentInParent<Rigidbody>();

        if (rigidbody)
        {
            rigidbody.AddForce(Vector3.up * 5f, ForceMode.VelocityChange);
            rigidbody.isKinematic = false;
        }
    }
    private void ResetCooldown()
    {
        OnCooldown = false;
    }

}

public enum ItemType
{
    Empty,Fruit,Pickaxe,Bucket
}
