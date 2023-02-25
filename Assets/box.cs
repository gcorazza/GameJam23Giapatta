using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        var contactPoint2Ds = new List<ContactPoint2D>();
        rb.GetContacts(contactPoint2Ds);
        foreach (var cp in contactPoint2Ds)
        {
            Debug.Log(cp.collider.ToString() + " other: " +cp.otherCollider.ToString());
            // otherColliderAttachedRigidbody.AddForceAtPosition(, otherColliderAttachedRigidbody.worldCenterOfMass);
            Debug.Log(cp.normal.ToString());
            Debug.Log(cp.otherRigidbody.velocity.ToString());

            // cp.otherRigidbody.AddForce(cp.normal , ForceMode2D.Impulse);
        }
    }
}
