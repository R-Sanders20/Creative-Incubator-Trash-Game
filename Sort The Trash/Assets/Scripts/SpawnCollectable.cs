using TMPro;
using UnityEngine;
using System.Collections;

public class SpawnCollectable : MonoBehaviour
{
    public TextMeshProUGUI pollutionText; // Text for GUI
    private int pollutionLevel = 0;

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

    
    public CollectableInteraction CollectableInteractionScript; // Links to SpawnCollectable script


    void Start()
    {
        // Starts the spawning of the 2 collectable areas
        StartCoroutine(SpawnStarterRoomCollectablesCoroutine());
        StartCoroutine(SpawnBeachCollectablesCoroutine());
    }

    // Controls the GUI Pollution text
    private void Update()
    {
        pollutionText.text = "Pollution : " + pollutionLevel.ToString() + " %";
    }

    // Random Spawning calculation 
    public void SpawnCollectableFromArray(GameObject[] collectablesArray, Vector3 spawnArea, Vector3 spawnAreaSize)
    {
        // Random X and Z from the spawn area
        float x = Random.Range(spawnArea.x - spawnAreaSize.x / 2, spawnArea.x + spawnAreaSize.x / 2);
        float z = Random.Range(spawnArea.z - spawnAreaSize.z / 2, spawnArea.z + spawnAreaSize.z / 2);    

        Vector3 randomPosition = new Vector3(x, spawnArea.y, z); // Saves the random area with the new x and z and has a placeholder for the y axis 

        // Uses raycast hit to figure out the floor
        RaycastHit Floor;

        if (Physics.Raycast(randomPosition, Vector3.down, out Floor)) // Takes the xyz of the random position and looks down to figure out where the floor is
        {    
            randomPosition.y = Floor.point.y - 0.2f; // Updates random position y with the new y level (lowered by 0.2f because it was still kind of floating)
        }

        // Picks a random collectable from the array and spawns it at the random position with no rotations applied
        Instantiate(collectablesArray[Random.Range(0, collectablesArray.Length)], randomPosition, Quaternion.identity);

        // Update pollution level
        pollutionLevel += 1;
    }

    // Runs the spawn function with the starter room variables
    IEnumerator SpawnStarterRoomCollectablesCoroutine()
    {
    
    while (pollutionLevel < 100 && CollectableInteraction.starterArea == true)
        {
            SpawnCollectableFromArray(starterRoomCollectables, starterRoomAreaPos, starterRoomAreaSize);
            Debug.Log("Spawned collectable in Starter Room at timestamp : " + Time.time);
            yield return new WaitForSeconds(3);
        }

        Debug.Log("Pollution in Starter Room exceeded 100%");
    }

    // Runs the spawn function with the beach area variables
    IEnumerator SpawnBeachCollectablesCoroutine()
    {
        while (pollutionLevel < 100 && CollectableInteraction.beachArea == true)
        {
            SpawnCollectableFromArray(beachCollectables, beachAreaPos, beachAreaSize);
            Debug.Log("Spawned collectable in Beach Area at timestamp : " + Time.time);
            yield return new WaitForSeconds(5);
        }

        Debug.Log("Pollution in Beach Area exceeded 100%");
    }

    // Function for the other collectable script
    public void Deposited()
    {
        pollutionLevel -= 1;
    }

    // Visual zones for areas
    void OnDrawGizmos()
    {
        // Starter room area
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(starterRoomAreaPos, starterRoomAreaSize);

        // Beach area
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(beachAreaPos, beachAreaSize);
        
        // Beach area
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(parkAreaPos, parkAreaSize);
    }}


