using TMPro;
using UnityEngine;

public class SpawnCollectable : MonoBehaviour
{
    public TextMeshProUGUI pollutionText;
    int pollutionLevel = 0;

    public GameObject collectable; 
    public Vector3 Area; 
    public Vector3 AreaSize; 

    void Start()
    {
        pollutionText.text = "Pollution : " + pollutionLevel.ToString() + " %";
        SpawnRandomCollectable();
    }

    public void SpawnRandomCollectable()
    {       
        // 13.63 1 5.66 Current test room area
        float x = Random.Range(Area.x - AreaSize.x / 2, Area.x + AreaSize.x / 2);
        float z = Random.Range(Area.z - AreaSize.z / 2, Area.z + AreaSize.z / 2);
        float y = Random.Range(Area.y - AreaSize.y / 2, Area.y + AreaSize.y / 2);
        Vector3 randomPosition = new Vector3(x, y, z);

        Instantiate(collectable, randomPosition, Quaternion.identity);

        pollutionLevel += 20;
        pollutionText.text = "Pollution : " + pollutionLevel.ToString() + " %";
    }

    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawWireCube(Area, AreaSize); 
    }
}
