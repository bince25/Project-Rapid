
using UnityEngine;

public class ShieldGameManager : MonoBehaviour
{
    public static ShieldGameManager Instance { get; private set; }

    [SerializeField] private Canvas canvas;
    [SerializeField] private AttackSpawner attackSpawner;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void StartGame()
    {
        canvas.gameObject.SetActive(true);
        attackSpawner.Reset();
        attackSpawner.StartSpawning();
    }
}