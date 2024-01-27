using UnityEngine;

public class PregnantManager : MonoBehaviour
{
    private GameObject pregnant;
    public GameObject pregnantPrefab;
    public Transform pregnantSpawnPoint;
    public GameObject dadPrefab;
    public Transform[] dadSpawnPoints;
    public GameObject miniGame;
    private bool isSpawning = false;
    [HideInInspector] public bool isPlayer2;
    public static PregnantManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void GiveBirthToBabyAndDestroyPregnant()
    {
        pregnant.GetComponent<PregnantController>().GiveBirth();
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
            Invoke("SpawnAndActivatePregnant", time);
            Invoke("SpawnDad", time + 1f);
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

    void SpawnDad()
    {
        GameObject[] dads = GameObject.FindGameObjectsWithTag("Dad");
        Vector3 spawnPoint;
        if (dads.Length > 0)
        {
            spawnPoint = dadSpawnPoints[dads.Length].position;
        }
        else
        {
            spawnPoint = dadSpawnPoints[0].position;
        }
        GameObject dad = Instantiate(dadPrefab, spawnPoint, Quaternion.identity);
    }
}