using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour
{
    public float gravity = 9.81f;
    public float totalTime = 3.41f;
    public float initialVelocity = 0f;
    public float depth = 0;
    public GameObject target;
    Rigidbody2D rb;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PositionCalc(OwnDepthCalc());
    }

    private void Update()
    {

        //Rotation of the gameobject
        rb.angularVelocity = OwnDepthCalc();

        if (rb.transform.position.y <= target.transform.position.y)
        {
            rb.velocity = new Vector2(0, 0);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void PositionCalc(float d)
    {
        rb.position = new Vector2(target.transform.position.x, d);
    }

    float OwnDepthCalc()
    {
        //d = vi * t + 0.5 * a * t * t
        float d = initialVelocity * totalTime + 0.5f * (-gravity) * (totalTime * totalTime);

        depth = d;
        return depth;
    }
}
