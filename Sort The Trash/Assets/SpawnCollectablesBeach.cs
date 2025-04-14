using TMPro;
using UnityEngine;
using System.Collections;
using System.Xml.Linq;

public class SpawnCollectablesBeach : MonoBehaviour
{
    public TextMeshProUGUI pollutionText;
    int pollutionLevel = 0;

    public GameObject[] collectables;
    public Vector3 Area;
    public Vector3 AreaSize;
    public Vector3 AreaRotation;

    void Start()
    {
        // Start the coroutine to spawn collectables every 5 seconds
        StartCoroutine(SpawnCollectablesCoroutine());
    }

    private void Update()
    {
        pollutionText.text = "Pollution : " + pollutionLevel.ToString() + " %";
    }

    public void SpawnRandomCollectable()
    {
        
        float x = Random.Range(Area.x - AreaSize.x / 2, Area.x + AreaSize.x / 2);
        float z = Random.Range(Area.z - AreaSize.z / 2, Area.z + AreaSize.z / 2);
        float y = Random.Range(Area.y - AreaSize.y / 2, Area.y + AreaSize.y / 2);
        Vector3 randomPosition = new Vector3(x, y, z);

        Instantiate(collectables[Random.Range(0, collectables.Length)], randomPosition, Quaternion.identity);

        pollutionLevel += 1;
        pollutionText.text = "Pollution : " + pollutionLevel.ToString() + " %";
    }

    public void Deposited()
    {
        pollutionLevel -= 20;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Area, AreaSize);
    }

    IEnumerator SpawnCollectablesCoroutine()
    {
        while (pollutionLevel < 100)
        {
            SpawnRandomCollectable();
            Debug.Log("Spawned collectable at timestamp : " + Time.time);
            yield return new WaitForSeconds(3); // Wait for 5 seconds before spawning the next collectable
        }
        Debug.Log("Polloution exceeded 100%");
    }
}