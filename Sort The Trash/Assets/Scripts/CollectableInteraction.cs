using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class CollectableInteraction : MonoBehaviour
{
    int blackCollectableStored = 0;
    int blueCollectableStored = 0;
    int greenCollectableStored = 0;
    public static bool starterArea = true;
    public static bool beachArea = false;
    public SpawnCollectable spawnCollectableScript; // Links to SpawnCollectable script
    public bool collected;
    public GameObject currentObject;
    public Transform PreviewAnchor;
    public int targetLayer = 6;
    public AudioSource pickUp, binObject;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Trigger with: " + other.tag);

        // Water
        if (other.CompareTag("Water"))
        {
            transform.position = new Vector3(50.12734f, -0.841f, 1.243f);
        }

        // Collectables
        if (other.CompareTag("Black Collectable") && !collected)
        {
            collected = true;
            GameObject prefabToSpawn = other.gameObject;

            blackCollectableStored++;

            GameObject clone = Instantiate(prefabToSpawn, PreviewAnchor.position, Quaternion.identity);
            objectScale(clone, 1f, targetLayer);

            Destroy(other.gameObject);
            currentObject = clone;
            Destroy(clone.GetComponent<Rigidbody>());

            Debug.Log("Collectables: " + blackCollectableStored + "Type: " + currentObject.name);
            pickUp.Play();
        }

        if (other.CompareTag("Blue Collectable") && !collected)
        {
            collected = true;
            GameObject prefabToSpawn = other.gameObject;

            blueCollectableStored++;

            GameObject clone = Instantiate(prefabToSpawn, PreviewAnchor.position, Quaternion.identity);
            objectScale(clone, 1f, targetLayer);

            Destroy(other.gameObject);

            currentObject = clone;

            Debug.Log("Collectables: " + blueCollectableStored + "Type: " + currentObject.name);
            pickUp.Play();
        }

        if (other.CompareTag("Green Collectable") && !collected)
        {
            collected = true;
            GameObject prefabToSpawn = other.gameObject;

            greenCollectableStored++;
            GameObject clone = Instantiate(prefabToSpawn, PreviewAnchor.position, Quaternion.identity);
            objectScale(clone, 1f, targetLayer);

            Destroy(other.gameObject);

            currentObject = clone;

            Debug.Log("Collectables: " + greenCollectableStored + "Type: " + currentObject.name);
            pickUp.Play();

        }

        // Bins
        if (other.CompareTag("Black Bin"))
        {
            if (blackCollectableStored > 0)
            {
                GameObject toDestroy = currentObject;
                currentObject = null;
                Destroy(toDestroy);
                collected = false;

                blackCollectableStored--;
                Debug.Log("Black Collectables now at: " + blackCollectableStored);
                spawnCollectableScript.Deposited();
                binObject.Play();
            }
            else
            {
                Debug.Log("No trash");
            }
        }

        if (other.CompareTag("Blue Bin"))
        {
            if (blueCollectableStored > 0)
            {
                GameObject toDestroy = currentObject;
                currentObject = null;
                Destroy(toDestroy);
                collected = false;

                blueCollectableStored--;
                Debug.Log("Blue Collectables now at: " + blueCollectableStored);
                spawnCollectableScript.Deposited();
                binObject.Play();
            }
            else
            {
                Debug.Log("No trash");
            }
        }

        if (other.CompareTag("Green Bin"))
        {
            if (greenCollectableStored > 0)
            {
                GameObject toDestroy = currentObject;
                currentObject = null;
                Destroy(toDestroy);

                collected = false;
                greenCollectableStored--;
                Debug.Log("Green Collectables now at: " + greenCollectableStored);
                spawnCollectableScript.Deposited();
                binObject.Play();
            }
            else
            {
                Debug.Log("No trash");
            }
        }

        if (other.CompareTag("starterArea"))
        {
            starterArea = true;
            Debug.Log("Starter Area: " + starterArea);
        }

        if (other.CompareTag("beachArea"))
        {
            beachArea = true;
            Debug.Log("beach Area: " + beachArea);

        }
    }
    void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("starterArea"))
        {
            starterArea = false;
            Debug.Log("Starter Area: " + starterArea);

        }

        if (other.CompareTag("beachArea"))
        {
            beachArea = false;
            Debug.Log("beach Area: " + beachArea);
        }
    }

    void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }

    void objectScale(GameObject obj, float targetSize, int layer)
    {
        obj.transform.SetParent(PreviewAnchor, false);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return;

        Bounds combinedBounds = renderers[0].bounds;
        foreach (Renderer r in renderers)
        {
            combinedBounds.Encapsulate(r.bounds);
        }

        float maxDimension = Mathf.Max(combinedBounds.size.x, combinedBounds.size.y, combinedBounds.size.z);
        if (maxDimension == 0) return;

        float scaleFactor = targetSize / maxDimension;
        obj.transform.localScale *= scaleFactor;

        SetLayerRecursively(obj, layer);
    }

    private void Update()
    {
        if (currentObject != null)
        {
            PreviewAnchor.Rotate(0f, 15f * Time.deltaTime, 15f * Time.deltaTime);
        }
    }
}