using UnityEngine;

public class PregnantManager : MonoBehaviour
{
    private GameObject pregnant;
    public GameObject pregnantPrefab;
    public Transform pregnantSpawnPoint;
    public GameObject miniGame;
    private bool isSpawning = false;
    public bool isPlayer2;
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
        if (pregnant == null && !isSpawning)
        {
            float time = Random.Range(4f, 7f);
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