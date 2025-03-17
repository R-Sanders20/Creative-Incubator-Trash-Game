using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableInteraction : MonoBehaviour
{
    int blackCollectableStored = 0; 

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Trigger with: " + other.tag);       
        if (other.CompareTag("Black Collectable"))
        {
            blackCollectableStored++;
            Debug.Log("Collectables:" + blackCollectableStored);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Black Bin"))
        {
            if (blackCollectableStored > 0) {
                blackCollectableStored --;
                Debug.Log("collectable deposited into bin");
            }
            else
            {
                Debug.Log("no black waste found");
            }
        }
    }
}
