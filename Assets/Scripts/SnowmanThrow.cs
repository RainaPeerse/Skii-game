using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanThrow : MonoBehaviour
{
    public float throwDistance;
    public int throwSpeed;
    private bool justThrown = false;
    private int snowballCount = 0;
    private int maxSnowballs = 10; // Maximum number of snowballs the snowman can throw

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
{
    GameObject target = GameObject.Find("Player");

    // Check if the target object is null before proceeding
    if (target != null)
    {
        float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

        if (distanceToTarget < throwDistance && !justThrown && snowballCount < maxSnowballs)
        {
            justThrown = true;
            GameObject tempSnowBall = SnowballPool.SharedInstance.GetPooledSnowball();
            if (tempSnowBall != null)
            {
                tempSnowBall.SetActive(true);
                tempSnowBall.transform.position = transform.position;
                tempSnowBall.transform.rotation = transform.rotation;
                Rigidbody tempRb = tempSnowBall.GetComponent<Rigidbody>();
                Vector3 targetDirection = Vector3.Normalize(target.transform.position - transform.position);

                // Add a small throw angle
                targetDirection += new Vector3(0, 0.33f, 0);
                tempRb.AddForce(targetDirection * throwSpeed);
                Invoke("ThrowOver", 0.1f);
                snowballCount++; // Increment the snowball count
            }
        }
    }
    else
    {
        Debug.LogWarning("Player object not found in the scene.");
    }
}

    void ThrowOver()
    {
        justThrown = false;
    }
}