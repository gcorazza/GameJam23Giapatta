using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    // Start is called before the first frame update

    private Boolean goleft, goright;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        input();
        updateMovement();
    }

    private void  updateMovement()
    {
        
        var rigidbody = GetComponent<Rigidbody2D>();
        var velocityX = rigidbody.velocity.x;
        
        var maxRunningSpeed = Globals.maxRunningSpeed;
        
        if (goleft)
        {
            var goleftV = velocityX>0
                ? maxRunningSpeed*2
                : Math.Max(maxRunningSpeed - Math.Abs(velocityX), 0);
            rigidbody.AddForce(new Vector2(-goleftV,0));
        }
        
        if (goright)
        {
            var gorightV = velocityX > 0
                ? Math.Max(maxRunningSpeed - velocityX, 0)
                : maxRunningSpeed * 2;
            rigidbody.AddForce(new Vector2(gorightV,0));
        }

        if (!goright && ! goleft && hasContact())
        {
            rigidbody.AddForce(new Vector2(-velocityX/6,0), ForceMode2D.Impulse);
        }

    }

    private Boolean hasContact()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        var contactPoint2Ds = new List<ContactPoint2D>();
        rb.GetContacts(contactPoint2Ds);
        return contactPoint2Ds.Count != 0;
    }

    private void input()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            bounce();
            setRadius(Globals.characterRadiusBounced);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            setRadius(Globals.characterRadius);
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.goleft = true;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            this.goleft = false;
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.goright = true;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            this.goright = false;
        }
        
        
    }

    void setRadius(float r)
    {
        GetComponent<CircleCollider2D>().radius = r;
    }

    public void bounce()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        var contactPoint2Ds = new List<ContactPoint2D>();
        rb.GetContacts(contactPoint2Ds);
        foreach (var cp in contactPoint2Ds)
        {
            Debug.Log(cp.collider.ToString() + " other: " + cp.otherCollider.ToString());
            // otherColliderAttachedRigidbody.AddForceAtPosition(, otherColliderAttachedRigidbody.worldCenterOfMass);
            Debug.Log(contactPoint2Ds.Count);
            Debug.Log(cp.otherRigidbody.velocity.ToString());
            
            rb.AddForce(cp.normal * Globals.bounceStrength, ForceMode2D.Impulse);
        }
    }
}
