using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.Progress;

public class CollectableInteraction : MonoBehaviour
{
    bool blackCollectableStored = false;
    bool blueCollectableStored = false;
    bool greenCollectableStored = false;
    public bool collected;

    public static bool starterArea = true;
    public static bool beachArea = false;
    public static bool parkArea = false;

    //wrong bin attempts
    private int playerTries = 0;

   
    public SpawnCollectable spawnCollectableScript;

    public GameObject currentObject;
    public Transform PreviewAnchor;
    public int targetLayer = 6;

    public AudioSource pickUp, binObject;
    public TextMeshProUGUI objectName;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Trigger with: " + other.tag);

        // Water 
        if (other.CompareTag("Water"))
        {
            transform.position = new Vector3(50.12734f, -0.841f, 1.243f);
        }

        // Collectables 
        if (collected == false)
        {
            if (other.CompareTag("Black Collectable"))
            {
                Collect(other, ref blackCollectableStored);
            }
            else if (other.CompareTag("Blue Collectable"))
            {
                Collect(other, ref blueCollectableStored);
            }
            else if (other.CompareTag("Green Collectable"))
            {
                Collect(other, ref greenCollectableStored);
            }
        }

        // Bins
        if (other.CompareTag("Black Bin"))
        {
            Deposit(ref blackCollectableStored);
        }
        else if (other.CompareTag("Blue Bin"))
        {
            Deposit(ref blueCollectableStored);
        }
        else if (other.CompareTag("Green Bin"))
        {
            Deposit(ref greenCollectableStored);
        }

        // Area triggers
        if (other.CompareTag("starterArea"))
        {
            starterArea = true;
            Debug.Log("Starter Area: " + starterArea);
        }
        if (other.CompareTag("beachArea"))
        {
            beachArea = true;
            Debug.Log("Beach Area: " + beachArea);
        }
        if (other.CompareTag("parkArea"))
        {
            parkArea = true;
            Debug.Log("Park Area: " + parkArea);
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
            Debug.Log("Beach Area: " + beachArea);
        }
        if (other.CompareTag("parkArea"))
        {
            parkArea = false;
            Debug.Log("Park Area: " + parkArea);
        }
    }

    void Collect(Collider other, ref bool collectableStored)
    {
        if (!collectableStored)
        {
            collected = true;
            collectableStored = true;

            GameObject prefabToSpawn = other.gameObject;
            GameObject clone = Instantiate(prefabToSpawn, PreviewAnchor.position, Quaternion.identity);
            objectScale(clone, 1f, targetLayer);
            Destroy(other.gameObject);

            currentObject = clone;
            Destroy(clone.GetComponent<Rigidbody>());

            Debug.Log("Collectable picked up: " + currentObject.name);
            pickUp.Play();
            objectName.text = CleanObjectName(currentObject.name);
        }
    }

    void Deposit(ref bool collectableStored)
    {
        if (collectableStored)
        {
            // Correct deposit
            Destroy(currentObject);
            currentObject = null;
            objectName.text = "";
            collected = false;

            collectableStored = false;
            Debug.Log("Deposit successful. Stored flag reset.");
            spawnCollectableScript.Deposited();
            binObject.Play();

            // Resets Player Attempt Counter
            playerTries = 0;
        }
        else if (collected)
        {
            // Wrong bin while holding something
            playerTries++;
            Debug.Log("Wrong bin!: " + playerTries);

            if (playerTries > 2)
            {
                ResetCollectables();

                spawnCollectableScript.Penalty();
               
            }
        }
        else
        {
            // Nothing held
            Debug.Log("No trash to deposit.");
        }
    }

    void ResetCollectables()
    {
        blackCollectableStored = false;
        blueCollectableStored = false;
        greenCollectableStored = false;
        collected = false;
        playerTries = 0;

        if (currentObject != null)
            Destroy(currentObject);

        currentObject = null;
        objectName.text = "";   
    }

    void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
            SetLayerRecursively(child.gameObject, layer);
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
            combinedBounds.Encapsulate(r.bounds);

        float maxDimension = Mathf.Max(combinedBounds.size.x, combinedBounds.size.y, combinedBounds.size.z);
        if (maxDimension == 0) return;

        float scaleFactor = targetSize / maxDimension;
        obj.transform.localScale *= scaleFactor;
        SetLayerRecursively(obj, layer);
    }

    private void Update()
    {
        if (currentObject != null)
            PreviewAnchor.Rotate(0f, 15f * Time.deltaTime, 15f * Time.deltaTime);
    }

    string CleanObjectName(string name)
    {
        string[] stringsToRemove = { "Variant", "(Clone)", "Red", "Blue", "Green", "2", "1", "Squashed" };
        foreach (string str in stringsToRemove)
            name = name.Replace(str, "");
        return name.Trim();
    }
}