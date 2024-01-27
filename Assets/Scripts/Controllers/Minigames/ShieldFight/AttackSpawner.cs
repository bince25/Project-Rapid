using System.Collections.Generic;
using UnityEngine;

public class AttackSpawner : MonoBehaviour
{
    public GameObject attackPrefab;
    public float spawnRate = 1f;
    private float nextSpawnTime = 0f;
    private int spawnCount = 0;
    private int hitCount = 0;

    [SerializeField]
    private bool spawning = false;

    private int currentAttackCount = 0;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private int MAXIMUM_HIT_COUNT = 3;
    [SerializeField]
    private int MAXIMUM_SPAWN_COUNT = 15;
    private int MAXIMUM_SIMULTANEOUS_ATTACKS = 3;

    [SerializeField] private List<Transform> spawnPositions;

    [SerializeField] private List<Vector3> moveDirections;

    void Update()
    {
        if (spawning)
        {
            if (Time.time >= nextSpawnTime && spawnCount < MAXIMUM_SPAWN_COUNT && currentAttackCount < MAXIMUM_SIMULTANEOUS_ATTACKS)
            {
                int randomIndex = Random.Range(0, spawnPositions.Count);

                GameObject attackInstance = Instantiate(attackPrefab, spawnPositions[randomIndex].position, Quaternion.identity, transform);
                nextSpawnTime = Time.time + 1f / spawnRate;

                if (attackInstance.TryGetComponent(out AttackController attackController))
                {
                    attackController.SetMoveDirection(moveDirections[randomIndex]);
                }
                spawnCount++;
                currentAttackCount++;
            }
            else if (spawnCount >= MAXIMUM_SPAWN_COUNT)
            {
                spawning = false;
                canvas.gameObject.SetActive(false);
            }
        }
    }

    public void StartSpawning()
    {
        spawning = true;
        nextSpawnTime = Time.time + 1f / spawnRate;
    }

    public void Hit()
    {
        hitCount++;
        if (hitCount >= MAXIMUM_HIT_COUNT)
        {
            canvas.gameObject.SetActive(false);
        }
    }

    public void AttackDestroyed()
    {
        currentAttackCount--;
    }

    public void Reset()
    {
        hitCount = 0;
        spawnCount = 0;
        currentAttackCount = 0;
    }
}
