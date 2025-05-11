using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class SpawnCollectable : MonoBehaviour
{
    //UI Stuff
    public TextMeshProUGUI pollutionText;
    public TextMeshProUGUI scoreText;

    private int pollutionLevel = 0;
    public int score = 0;
    public int ScoreFinal;

    // Collectable Arrays
    public GameObject[] starterRoomCollectables;
    public GameObject[] beachCollectables;
    public GameObject[] parkCollectables;

    // Collectable Spawn Area + Area Size
    public Vector3 starterRoomAreaPos;
    public Vector3 starterRoomAreaSize;

    public Vector3 beachAreaPos;
    public Vector3 beachAreaSize;

    public Vector3 parkAreaPos;
    public Vector3 parkAreaSize;
    

    //Collectable Script for the player area checker
    public CollectableInteraction CollectableInteraction; // Links to CollectableInteraction script

    // coroutine's initialised as a null
    private Coroutine starterRoomCoroutine = null;
    private Coroutine beachCoroutine = null;
    private Coroutine parkCoroutine = null;

    public AudioSource spawnSound;


    void Update()
    {
        // Starts the collectable Scripts When the player enters the Area
        
        if (CollectableInteraction.starterArea && starterRoomCoroutine == null)
        { 
            starterRoomCoroutine = StartCoroutine(SpawnStarterRoomCollectablesCoroutine());
        }

        if (CollectableInteraction.beachArea && beachCoroutine == null)
        {
            beachCoroutine = StartCoroutine(SpawnBeachCollectablesCoroutine());
        }

        //Stops the Collectable scripts when the player is not in the area

        if (!CollectableInteraction.starterArea && starterRoomCoroutine != null)
        {
            StopCoroutine(starterRoomCoroutine);
            starterRoomCoroutine = null;
        }

        if (!CollectableInteraction.beachArea && beachCoroutine != null)
        {
            StopCoroutine(beachCoroutine);
            beachCoroutine = null;
        }

        // Update GUI Pollution text
        pollutionText.text = "Pollution : " + pollutionLevel.ToString() + " %";
        scoreText.text = "Score : " + score;
        ScoreFinal = score;
    }

    // Random Spawning calculation
    public void SpawnCollectableFromArray(GameObject[] collectablesArray, Vector3 spawnArea, Vector3 spawnAreaSize)
    {
        float x = Random.Range(spawnArea.x - spawnAreaSize.x / 2, spawnArea.x + spawnAreaSize.x / 2);
        float z = Random.Range(spawnArea.z - spawnAreaSize.z / 2, spawnArea.z + spawnAreaSize.z / 2);

        Vector3 randomPosition = new Vector3(x, spawnArea.y + 0.12f, z); // Placeholder for the y-axis

        // Uses raycast hit to determine floor
        if (Physics.Raycast(randomPosition, Vector3.down, out RaycastHit floor))
        {
            randomPosition.y = floor.point.y;
        }

        // Pick a random collectable from the array and spawn it
        Instantiate(collectablesArray[Random.Range(0, collectablesArray.Length)], randomPosition, Quaternion.identity);

        // Update pollution level
        pollutionLevel += 1;
    }

    // Starter Room Collectables Coroutine
    IEnumerator SpawnStarterRoomCollectablesCoroutine()
    {
        while (pollutionLevel < 100)
        {
            SpawnCollectableFromArray(starterRoomCollectables, starterRoomAreaPos, starterRoomAreaSize);
            Debug.Log("Spawned collectable in Starter Room at timestamp: " + Time.time);
            yield return new WaitForSeconds(3);
            spawnSound.Play();
        }
        Debug.Log("Pollution in Starter Room exceeded 100%");
        starterRoomCoroutine = null; // Reset reference when coroutine ends
        SceneManager.LoadSceneAsync(2);
    }

    // Beach Collectables Coroutine
    IEnumerator SpawnBeachCollectablesCoroutine()
    {
        while (pollutionLevel < 100)
        {
            SpawnCollectableFromArray(beachCollectables, beachAreaPos, beachAreaSize);
            Debug.Log("Spawned collectable in Beach Area at timestamp: " + Time.time);
            yield return new WaitForSeconds(5);
            spawnSound.Play();
        }
        Debug.Log("Pollution in Beach Area exceeded 100%");
        beachCoroutine = null; // Reset reference when coroutine ends
        SceneManager.LoadSceneAsync(2);
    }

    // Function for the other collectable script
    public void Deposited()
    {
        pollutionLevel -= 1;
        score++;
    }

    // Visual zones for areas
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(starterRoomAreaPos, starterRoomAreaSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(beachAreaPos, beachAreaSize);

        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(parkAreaPos, parkAreaSize);
    }
}