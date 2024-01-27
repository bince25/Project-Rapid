
using UnityEngine;

public class ShieldGameManager : MonoBehaviour
{
    public static ShieldGameManager Instance { get; private set; }

    [SerializeField] private Canvas canvas;
    [SerializeField] private AttackSpawner attackSpawner;
    [SerializeField] private ShieldController shieldController;
    private GameObject heldBaby;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void StartGame(GameObject heldBaby)
    {
        this.heldBaby = heldBaby;
        heldBaby.transform.parent.GetComponent<PlayerController>().SetCanMove(false);
        if (heldBaby.transform.parent.GetComponent<PlayerController>().isPlayer2)
        {
            shieldController.isPlayer2 = true;
        }
        else
        {
            shieldController.isPlayer2 = false;
        }
        canvas.gameObject.SetActive(true);
        attackSpawner.Reset();
        attackSpawner.StartSpawning();
    }

    public void EndGame()
    {
        attackSpawner.Reset();
        canvas.gameObject.SetActive(false);
        heldBaby.GetComponent<BabyController>().StopCrying();
        heldBaby.transform.parent.GetComponent<PlayerController>().SetCanMove(true);
    }
}