using UnityEngine;

// Faciliates pickup behaviour

public class Container : MonoBehaviour
{

    public GameObject Item = null;
    private Transform _location = null;

    public Transform Location { get
        {
            if (_location) return _location;
            return transform;
        }

        set => _location = value; }
}
