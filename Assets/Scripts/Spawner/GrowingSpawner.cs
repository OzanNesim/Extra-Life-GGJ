using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingSpawner : MonoBehaviour
{

    public GameObject[] Prefabs; 

    public void Grow(float Time)
    {
        StartCoroutine(GrowInternal(Prefabs[Random.Range(0, Prefabs.Length - 1)], Time));
    }

    public void GrowThis(GameObject Prefab, float Time)
    {
        StartCoroutine(GrowInternal(Prefab, Time));
    }

    private IEnumerator GrowInternal(GameObject prefab,float Time)
    {

        var spawnee = Instantiate(prefab,transform);

        Rigidbody rigidbody = spawnee.GetComponent<Rigidbody>();

        if(rigidbody)
        {
            rigidbody.isKinematic = true;
        }

        spawnee.transform.localScale = Vector3.zero;
        spawnee.transform.DOScale(1f, Time);

        yield return new WaitForSeconds(Time);

        spawnee.transform.parent = null;

        spawnee.transform.DOShakeScale(0.5f);

        if (rigidbody)
        {
            rigidbody.isKinematic = false;
        }

    }

    private void Start()
    {
        Grow(5f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 1f);
    }

}
