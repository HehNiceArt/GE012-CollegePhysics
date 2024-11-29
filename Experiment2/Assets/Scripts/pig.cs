using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Callbacks;
using UnityEngine;

public class pig : MonoBehaviour
{
    [SerializeField] GameObject carrot;
    [SerializeField] TrajectoryCalc trajectoryCalc;
    [SerializeField] float speed;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trajectoryCalc.isPressed)
        {
            StartCoroutine(FollowPlayerCoroutine());
        }
    }
    IEnumerator FollowPlayerCoroutine()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            rb.velocity = new Vector2(speed, 0);
            yield return null;
        }
    }
}
