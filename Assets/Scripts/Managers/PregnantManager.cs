using System.Collections.Generic;
using UnityEngine;

public class PregnantManager : MonoBehaviour
{
    private GameObject pregnant;
    public GameObject[] pregnantPrefabs;
    public Transform pregnantSpawnPoint;
    public GameObject[] dadPrefabs;
    public Transform[] dadSpawnPoints;
    public Dictionary<int, bool> dadSpawnPointsDictionary = new Dictionary<int, bool>();
    public GameObject miniGame;
    private bool isSpawning = false;
    int index;
    [HideInInspector] public bool isPlayer2;
    public static PregnantManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i <= 5; i++)
        {
            dadSpawnPointsDictionary.Add(i, false);
        }
    }
    public void GiveBirthToBabyAndDestroyPregnant()
    {
        pregnant.GetComponent<PregnantController>().GiveBirth(index);
        miniGame.SetActive(false);
    }

    public void ActivateMiniGame(bool isPlayer2)
    {
        this.isPlayer2 = isPlayer2;
        miniGame.SetActive(true);
    }

    void Update()
    {
        pregnant = GameObject.FindGameObjectWithTag("Pregnant");
        GameObject[] babies = GameObject.FindGameObjectsWithTag("Baby");
        if (pregnant == null && !isSpawning && babies.Length < 6)
        {
            float time = Random.Range(4f, 7f);
            Invoke("SpawnDad", time);
            Invoke("SpawnAndActivatePregnant", time + 1f);
            isSpawning = true;
        }
    }

    void SpawnAndActivatePregnant()
    {
        Vector3 spawnPoint = pregnantSpawnPoint.position;
        GameObject pregnantPrefab = pregnantPrefabs[Random.Range(0, pregnantPrefabs.Length)];
        pregnant = Instantiate(pregnantPrefab, spawnPoint, Quaternion.identity);
        pregnant.GetComponent<PregnantController>().Activate();
        isSpawning = false;
    }

    void SpawnDad()
    {
        Vector3 spawnPoint;
        for (int i = 0; i < dadSpawnPoints.Length; i++)
        {
            if (!dadSpawnPointsDictionary[i])
            {
                spawnPoint = dadSpawnPoints[i].position;
                GameObject dadPrefab = dadPrefabs[Random.Range(0, dadPrefabs.Length)];
                GameObject dad = Instantiate(dadPrefab, spawnPoint, Quaternion.identity);
                dadSpawnPointsDictionary[i] = true;
                index = i;
                dad.GetComponent<DadController>().setDadIndex(i);
                return;
            }
        }
        return;
    }
}