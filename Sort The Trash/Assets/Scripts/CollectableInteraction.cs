using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableInteraction : MonoBehaviour
{
    int blackCollectableStored = 0;
    public SpawnCollectable spawnCollectableScript; // Reference to SpawnCollectable script
    public bool collected;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Trigger with: " + other.tag);
        if (other.CompareTag("Black Collectable") && collected == false)
        {
            blackCollectableStored++;
            Debug.Log("Collectables:" + blackCollectableStored);
            Destroy(other.gameObject);
            collected = true;
        }

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
    }
}
