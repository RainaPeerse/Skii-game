using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCheck : MonoBehaviour
{
    public enum Direction { Left, Right };
    public Direction passingDirection;
    public Material passedFlagMat, failedFlagMat;
    public RaceTimer raceTimer; // Assign this in the Unity Editor with the RaceTimer component

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (passingDirection == Direction.Left)
            {
                if (other.transform.position.x < transform.position.x)
                {
                    PassSuccessful();
                }
                else
                {
                    PassUnsuccessful();
                }
            }
            else if (passingDirection == Direction.Right)
            {
                if (other.transform.position.x > transform.position.x)
                {
                    PassSuccessful();
                }
                else
                {
                    PassUnsuccessful();
                }
            }
        }
    }

    private void PassSuccessful()
    {
        GetComponent<Renderer>().material = passedFlagMat;
    }

    private void PassUnsuccessful()
    {
        GetComponent<Renderer>().material = failedFlagMat;
        // Increment the timer by one second
        if (raceTimer != null)
        {
            raceTimer.IncrementTime();
        }
    }
}