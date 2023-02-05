using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public Portal Pair;
    public bool OnCooldown;

    public float CooldownTime = 1.0f;

    private void OnTriggerEnter(Collider other)
    {

        if (OnCooldown) return;
      
        if(other.gameObject.TryGetComponent(out CameraControl control))
        {

            //var delta = Pair.transform.eulerAngles.y - transform.eulerAngles.y;
            var delta = Pair.transform.eulerAngles.y - control.Heading;


            if (delta > 180)
            {
                delta = 360 - delta;
            }

            Pair.PutOnCooldown();
            control.transform.DOMove(Pair.transform.position, 0.5f);
            DOTween.To(() => control.Heading, x => control.Heading = x, control.Heading + delta, 0.5f);
            //DOTween.To(() => control.Heading, x => control.Heading = x, Pair.transform.eulerAngles.y, 0.5f);
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

    
