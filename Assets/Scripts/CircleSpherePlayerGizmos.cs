using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpherePlayerGizmos : MonoBehaviour
{
    [SerializeField] float grappleRange = 55f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, grappleRange);
        Gizmos.color = Color.yellow;
    }
}
