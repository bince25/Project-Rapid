using UnityEngine;

public class PregnantManager : MonoBehaviour
{
    private GameObject pregnant;
    public GameObject pregnantPrefab;
    public Transform pregnantSpawnPoint; 
    private bool isSpawning = false;
    void Update()
    {
        pregnant = GameObject.FindGameObjectWithTag("Pregnant");
        if (pregnant == null && !isSpawning)
        {
            float time = Random.Range(5f, 15f);
            Invoke("SpawnAndActivatePregnant", time);
            isSpawning = true;
        }
    }

    void SpawnAndActivatePregnant()
    {
        Vector3 spawnPoint = pregnantSpawnPoint.position;
        pregnant = Instantiate(pregnantPrefab, spawnPoint, Quaternion.identity);
        pregnant.GetComponent<PregnantController>().Activate();
        isSpawning = false;
    }
}