using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager Instance { get; private set; }
    public GameObject choicePanel;
    public Button[] options;
    public Image[] optionImages;
    private int currentSelection = 0;
    private GameObject dadInConversation;
    private bool isPlayer2;
    private GameObject playerInConversation;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Activate(GameObject dad, GameObject player)
    {
        if (player.GetComponent<PlayerController>().isPlayer2)
        {
            isPlayer2 = true;
        }
        else
        {
            isPlayer2 = false;
        }
        playerInConversation = player;
        dadInConversation = dad;
        player.GetComponent<PlayerController>().SetCanMove(false);
        choicePanel.SetActive(true); // Show the choice panel
        currentSelection = 0; // Reset the current selection
        UpdateCursorVisual(); // Update the cursor visual
    }

    void Update()
    {
        if (choicePanel.activeSelf)
        {
            HandleInput();
        }

    }

    void HandleInput()
    {
        switch (isPlayer2)
        {
            case true:
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    MoveCursor(-1); // Move up in the options
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    MoveCursor(1); // Move down in the options
                }
                if (Input.GetKeyDown(KeyCode.RightControl))
                {
                    SelectCurrentOption();
                }
                break;
            case false:
                if (Input.GetKeyDown(KeyCode.W))
                {
                    MoveCursor(-1); // Move up in the options
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    MoveCursor(1); // Move down in the options
                }
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    SelectCurrentOption();
                }
                break;
        }
    }

    private void MoveCursor(int direction)
    {
        // Update current selection based on direction
        currentSelection = Mathf.Clamp(currentSelection + direction, 0, options.Length - 1);

        // Update visual cursor here (e.g., highlight the current button)
        UpdateCursorVisual();
    }

    private void UpdateCursorVisual()
    {
        for (int i = 0; i < options.Length; i++)
        {
            var colors = options[i].colors;
            colors.normalColor = (i == currentSelection) ? Color.green : Color.white; // Highlight color for selected
            options[i].colors = colors;
            optionImages[i].color = (i == currentSelection) ? Color.green : Color.white; // Change color for selected
        }
    }

    private void SelectCurrentOption()
    {
        Debug.Log("Option " + (currentSelection + 1) + " selected.");

        if (currentSelection == 0)
        {
            Debug.Log("Kill Selected.");
            AudioManager.Instance.PlaySFX(SFX.Laser);
            Destroy(dadInConversation);
        }
        else if (currentSelection == 1)
        {
            Debug.Log("Option 2 selected.");
            dadInConversation.GetComponent<DadController>().notification.SetActive(false);
            GameManager.Instance.TimerManager.AddTime(15);
            GameManager.Instance.DecreaseSatisfactionLevel(15);
        }
        playerInConversation.GetComponent<PlayerController>().SetCanMove(true);
        choicePanel.SetActive(false); // Hide the choice panel

        // Add more code here to handle the selected option
    }
}
