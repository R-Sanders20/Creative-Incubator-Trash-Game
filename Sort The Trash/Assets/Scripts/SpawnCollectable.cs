using UnityEngine;

public class SpawnCollectable : MonoBehaviour
{
    public GameObject collectable; 
    public Vector3 Area; 
    public Vector3 AreaSize; 

    void Start()
    {
        SpawnRandomCollectable();
    }

    public void SpawnRandomCollectable()
    {       
        // 13.63 1 5.66 Current test room area
        float x = Random.Range(Area.x - AreaSize.x / 2, Area.x + AreaSize.x / 2);
        float z = Random.Range(Area.z - AreaSize.z / 2, Area.z + AreaSize.z / 2);
        Vector3 randomPosition = new Vector3(x, z);

        Instantiate(collectable, randomPosition, Quaternion.identity);
    }

    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawWireCube(Area, AreaSize); 
    }
}
