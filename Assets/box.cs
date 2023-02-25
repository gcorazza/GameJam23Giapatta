using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    // Start is called before the first frame update

    public enum BounceState
    {
        No, Start, Bounce
    }
    
    public Boolean goleft, goright;
    
    public BounceState bounceState = BounceState.No;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        updateMovement();
        input();
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

        if (bounceState == BounceState.Start)
        {
            if (bounce() > 0 )
            {
                bounceState = BounceState.No;
            }
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
        var characterBounceScale = Globals.characterBounceScale;
        if (Input.GetKeyDown(KeyCode.W))
        {
            bounceState = BounceState.Start;
            // bounce();
            transform.localScale += new Vector3(characterBounceScale, characterBounceScale, characterBounceScale);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            transform.localScale -= new Vector3(characterBounceScale, characterBounceScale, characterBounceScale);
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

    public int bounce()
    {
        Debug.Log("bounce");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        var contactPoint2Ds = new List<ContactPoint2D>();
        rb.GetContacts(contactPoint2Ds);
        foreach (var cp in contactPoint2Ds)
        {
            Debug.Log(cp.collider.ToString() + " other: " + cp.otherCollider.ToString());
            Debug.Log(contactPoint2Ds.Count);
            Debug.Log(cp.otherRigidbody.velocity.ToString());

            var distwpcp = rb.worldCenterOfMass - cp.point;
            insertDebugPoint(cp.point.x, cp.point.y);
            Debug.Log(distwpcp.magnitude);
            
            var intersect_ = 1;
            rb.AddForce(cp.normal * Globals.bounceStrength * intersect_, ForceMode2D.Impulse);
        }

        return contactPoint2Ds.Count;
    }

    private static void insertDebugPoint(float x, float y)
    {
        var transform1 = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        transform1.Translate(x,y, 0);
        transform1.localScale -= new Vector3(0.8f, 0.8f, 0.8f);
    }
}
