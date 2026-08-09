using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public sealed class LevelUpChoiceUI : MonoBehaviour
{
    public const int ChoiceCount = 3;

    private const string ChoiceLabelA = "Prototype Choice A";
    private const string ChoiceLabelB = "Prototype Choice B";
    private const string ChoiceLabelC = "Prototype Choice C";

    [SerializeField]
    private GameObject levelUpPanel;

    [SerializeField]
    private Button choiceButton1;

    [SerializeField]
    private Button choiceButton2;

    [SerializeField]
    private Button choiceButton3;

    [SerializeField]
    private Text choiceLabel1;

    [SerializeField]
    private Text choiceLabel2;

    [SerializeField]
    private Text choiceLabel3;

    private LevelUpController levelUpController;
    private bool isInitialized;
    private bool isShowing;
    private bool isChoiceLocked;

    public bool IsShowing => isShowing
        && levelUpPanel != null
        && levelUpPanel.activeSelf;

    private void Awake()
    {
        if (!ValidateReferences())
        {
            if (levelUpPanel != null)
            {
                levelUpPanel.SetActive(false);
            }

            enabled = false;
            return;
        }

        choiceButton1.onClick.AddListener(SelectChoice1);
        choiceButton2.onClick.AddListener(SelectChoice2);
        choiceButton3.onClick.AddListener(SelectChoice3);
        ApplyPlaceholderLabels();
        HideChoices();
    }

    private void OnDestroy()
    {
        if (choiceButton1 != null)
        {
            choiceButton1.onClick.RemoveListener(SelectChoice1);
        }

        if (choiceButton2 != null)
        {
            choiceButton2.onClick.RemoveListener(SelectChoice2);
        }

        if (choiceButton3 != null)
        {
            choiceButton3.onClick.RemoveListener(SelectChoice3);
        }
    }

    public bool Initialize(LevelUpController newLevelUpController)
    {
        if (!enabled || newLevelUpController == null)
        {
            return false;
        }

        levelUpController = newLevelUpController;
        isInitialized = true;
        return true;
    }

    public void ShowChoices()
    {
        if (!isInitialized || levelUpPanel == null)
        {
            return;
        }

        ApplyPlaceholderLabels();
        isChoiceLocked = false;
        SetButtonsInteractable(true);
        levelUpPanel.SetActive(true);
        isShowing = true;
    }

    public void HideChoices()
    {
        isShowing = false;
        isChoiceLocked = true;
        SetButtonsInteractable(false);

        if (levelUpPanel != null)
        {
            levelUpPanel.SetActive(false);
        }
    }

    private bool ValidateReferences()
    {
        if (levelUpPanel == null
            || choiceButton1 == null
            || choiceButton2 == null
            || choiceButton3 == null
            || choiceLabel1 == null
            || choiceLabel2 == null
            || choiceLabel3 == null)
        {
            Debug.LogError(
                "LevelUpChoiceUI requires its Panel, three Buttons, and three Text labels.",
                this);
            return false;
        }

        if (choiceButton1 == choiceButton2
            || choiceButton1 == choiceButton3
            || choiceButton2 == choiceButton3)
        {
            Debug.LogError(
                "LevelUpChoiceUI requires three different Button references.",
                this);
            return false;
        }

        if (choiceLabel1 == choiceLabel2
            || choiceLabel1 == choiceLabel3
            || choiceLabel2 == choiceLabel3)
        {
            Debug.LogError(
                "LevelUpChoiceUI requires three different Text label references.",
                this);
            return false;
        }

        return true;
    }

    private void ApplyPlaceholderLabels()
    {
        if (choiceLabel1 != null)
        {
            choiceLabel1.text = ChoiceLabelA;
        }

        if (choiceLabel2 != null)
        {
            choiceLabel2.text = ChoiceLabelB;
        }

        if (choiceLabel3 != null)
        {
            choiceLabel3.text = ChoiceLabelC;
        }
    }

    private void SetButtonsInteractable(bool interactable)
    {
        if (choiceButton1 != null)
        {
            choiceButton1.interactable = interactable;
        }

        if (choiceButton2 != null)
        {
            choiceButton2.interactable = interactable;
        }

        if (choiceButton3 != null)
        {
            choiceButton3.interactable = interactable;
        }
    }

    private void SelectChoice1()
    {
        TrySelectChoice(0);
    }

    private void SelectChoice2()
    {
        TrySelectChoice(1);
    }

    private void SelectChoice3()
    {
        TrySelectChoice(2);
    }

    private void TrySelectChoice(int choiceIndex)
    {
        if (!isInitialized
            || !IsShowing
            || isChoiceLocked
            || levelUpController == null)
        {
            return;
        }

        isChoiceLocked = true;
        SetButtonsInteractable(false);
        levelUpController.SelectChoice(choiceIndex);
    }
}
