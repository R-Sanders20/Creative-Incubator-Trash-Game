using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableInteraction : MonoBehaviour
{
    int blackCollectableStored = 0,blueCollectableStored = 0 ,greenCollectableStored = 0;
    public SpawnCollectable spawnCollectableScript; // Reference to SpawnCollectable script
    public bool collected;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Trigger with: " + other.tag);
        
        //Water

        if (other.CompareTag("Water"))
        {
                transform.position = new Vector3(50.12734f, -0.841f, 1.243f);
        }

        //Collectables

        if (other.CompareTag("Black Collectable") && collected == false)
        {
            blackCollectableStored++;
            Debug.Log("Collectables:" + blackCollectableStored);
            Destroy(other.gameObject);
            collected = true;
        }
        
        
        if (other.CompareTag("Blue Collectable") && collected == false)
        {
            blueCollectableStored++;
            Debug.Log("Collectables:" + blueCollectableStored);
            Destroy(other.gameObject);
            collected = true;
        }




        if (other.CompareTag("Green Collectable") && collected == false)
        {
            greenCollectableStored++;
            Debug.Log("Collectables:" + greenCollectableStored);
            Destroy(other.gameObject);
            collected = true;
        }

        

        //Bins 

        if (other.CompareTag("Black Bin"))
        {
            if (blackCollectableStored > 0)
            {
                blackCollectableStored--;
                Debug.Log("Black Collectables now at: " + blackCollectableStored);
                spawnCollectableScript.Deposited();
                collected = false;
            }
            else
            {
                Debug.Log("no trash");
            }
        }

        if (other.CompareTag("Blue Bin"))
        {
            if (blueCollectableStored > 0)
            {
                blueCollectableStored--;
                Debug.Log("Blue Collectables now at: " + blueCollectableStored);
                spawnCollectableScript.Deposited();
                collected = false;
            }
            else
            {
                Debug.Log("no trash");
            }
        }

        if (other.CompareTag("Green Bin"))
        {
            if (greenCollectableStored> 0)
            {
                greenCollectableStored--;
                Debug.Log("Green Collectables now at: " + greenCollectableStored);
                spawnCollectableScript.Deposited();
                collected = false;
            }
            else
            {
                Debug.Log("no trash");
            }
        }


    }
}
