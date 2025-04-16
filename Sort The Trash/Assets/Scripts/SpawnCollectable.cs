using TMPro;
using UnityEngine;
using System.Collections;

public class SpawnCollectable : MonoBehaviour
{
    public TextMeshProUGUI pollutionText; // Fixed naming consistency with your initial declaration
    private int pollutionLevel = 0; // Restored consistent naming

    // Collectable Arrays
    public GameObject[] starterRoomCollectables; // Consistent camelCase naming
    public GameObject[] beachCollectables;

    // Collectable Spawn Area + Area Size
    public Vector3 starterRoomArea;
    public Vector3 starterRoomAreaSize;

 
    public Vector3 beachArea;
    public Vector3 beachAreaSize;

    void Start()
    {
        // Starts the spawning of the 2 collectable areas
        StartCoroutine(SpawnStarterRoomCollectablesCoroutine());
        StartCoroutine(SpawnBeachCollectablesCoroutine());
    }

    //controls the GUI Polloution text
    private void Update()
    {
        pollutionText.text = "Pollution : " + pollutionLevel.ToString() + " %"; 
    }


    //Random Spawning calculation 
    public void SpawnCollectableFromArray(GameObject[] collectablesArray, Vector3 spawnArea, Vector3 spawnAreaSize)
    {
        float x = Random.Range(spawnArea.x - spawnAreaSize.x / 2, spawnArea.x + spawnAreaSize.x / 2);
        float z = Random.Range(spawnArea.z - spawnAreaSize.z / 2, spawnArea.z + spawnAreaSize.z / 2);
        float y = Random.Range(spawnArea.y - spawnAreaSize.y / 2, spawnArea.y + spawnAreaSize.y / 2);
        Vector3 randomPosition = new Vector3(x, y, z);

        Instantiate(collectablesArray[Random.Range(0, collectablesArray.Length)], randomPosition, Quaternion.identity);

        //updates pollution 
        pollutionLevel += 1;
    }


    //runs the spawn function with the starter room variables
    IEnumerator SpawnStarterRoomCollectablesCoroutine()
    {
        while (pollutionLevel < 100)
        {
            SpawnCollectableFromArray(starterRoomCollectables, starterRoomArea, starterRoomAreaSize);
            Debug.Log("Spawned collectable in Starter Room at timestamp : " + Time.time);
            yield return new WaitForSeconds(3);
        }
        Debug.Log("Pollution in Starter Room exceeded 100%");
    }

    //runs the spawn function with the beach area variables
    IEnumerator SpawnBeachCollectablesCoroutine()
    {
        while (pollutionLevel < 100)
        {
            SpawnCollectableFromArray(beachCollectables, beachArea, beachAreaSize);
            Debug.Log("Spawned collectable in Beach Area at timestamp : " + Time.time);
            yield return new WaitForSeconds(5); 
        }
        Debug.Log("Pollution in Beach Area exceeded 100%");
    }

    //Function for the other collectable script
    public void Deposited()
    {
        pollutionLevel -= 1; 
    }


    //Gizmo so i can physically see the area's
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(starterRoomArea, starterRoomAreaSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(beachArea, beachAreaSize);
    }
}