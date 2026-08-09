using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[DisallowMultipleComponent]
public sealed class StartingSpellSelectionUI : MonoBehaviour
{
    public const int ChoiceCount =
        StartingSpellSelectionController.StartingChoiceCount;

    [SerializeField]
    private GameObject startingSpellPanel;

    [SerializeField]
    private Button choiceButton1;

    [SerializeField]
    private Button choiceButton2;

    [SerializeField]
    private Button choiceButton3;

    [SerializeField]
    private Button choiceButton4;

    [SerializeField]
    private Button choiceButton5;

    [SerializeField]
    private Button choiceButton6;

    [SerializeField]
    private Button choiceButton7;

    [SerializeField]
    private Button choiceButton8;

    [SerializeField]
    private Text choiceLabel1;

    [SerializeField]
    private Text choiceLabel2;

    [SerializeField]
    private Text choiceLabel3;

    [SerializeField]
    private Text choiceLabel4;

    [SerializeField]
    private Text choiceLabel5;

    [SerializeField]
    private Text choiceLabel6;

    [SerializeField]
    private Text choiceLabel7;

    [SerializeField]
    private Text choiceLabel8;

    private Button[] choiceButtons;
    private Text[] choiceLabels;
    private UnityAction[] clickHandlers;
    private SkillDefinition[] startingChoices;
    private StartingSpellSelectionController controller;
    private bool isInitialized;
    private bool isShowing;
    private bool isChoiceLocked;

    public bool IsShowing => isShowing
        && startingSpellPanel != null
        && startingSpellPanel.activeSelf;

    private void Awake()
    {
        choiceButtons = new[]
        {
            choiceButton1,
            choiceButton2,
            choiceButton3,
            choiceButton4,
            choiceButton5,
            choiceButton6,
            choiceButton7,
            choiceButton8
        };

        choiceLabels = new[]
        {
            choiceLabel1,
            choiceLabel2,
            choiceLabel3,
            choiceLabel4,
            choiceLabel5,
            choiceLabel6,
            choiceLabel7,
            choiceLabel8
        };

        if (!ValidateReferences())
        {
            if (startingSpellPanel != null)
            {
                startingSpellPanel.SetActive(false);
            }

            enabled = false;
            return;
        }

        clickHandlers = new UnityAction[ChoiceCount];

        for (int index = 0; index < ChoiceCount; index++)
        {
            int choiceIndex = index;
            clickHandlers[index] = () => TrySelectChoice(choiceIndex);
            choiceButtons[index].onClick.AddListener(clickHandlers[index]);
        }

        HideChoices();
    }

    private void OnDestroy()
    {
        if (choiceButtons == null || clickHandlers == null)
        {
            return;
        }

        for (int index = 0; index < ChoiceCount; index++)
        {
            if (choiceButtons[index] != null
                && clickHandlers[index] != null)
            {
                choiceButtons[index].onClick.RemoveListener(
                    clickHandlers[index]);
            }
        }
    }

    public bool Initialize(
        StartingSpellSelectionController newController,
        IReadOnlyList<SkillDefinition> newStartingChoices)
    {
        if (!enabled
            || newController == null
            || !ValidateStartingChoices(newStartingChoices))
        {
            return false;
        }

        controller = newController;
        startingChoices = new SkillDefinition[ChoiceCount];

        for (int index = 0; index < ChoiceCount; index++)
        {
            startingChoices[index] = newStartingChoices[index];
        }

        ApplyChoiceLabels();
        isInitialized = true;
        return true;
    }

    public void ShowChoices()
    {
        if (!isInitialized || startingSpellPanel == null)
        {
            return;
        }

        ApplyChoiceLabels();
        isChoiceLocked = false;
        SetButtonsInteractable(true);
        startingSpellPanel.SetActive(true);
        isShowing = true;
    }

    public void HideChoices()
    {
        isShowing = false;
        isChoiceLocked = true;
        SetButtonsInteractable(false);

        if (startingSpellPanel != null)
        {
            startingSpellPanel.SetActive(false);
        }
    }

    private bool ValidateReferences()
    {
        if (startingSpellPanel == null
            || choiceButtons.Length != ChoiceCount
            || choiceLabels.Length != ChoiceCount)
        {
            Debug.LogError(
                "StartingSpellSelectionUI requires its Panel, eight Buttons, "
                + "and eight Text labels.",
                this);
            return false;
        }

        for (int index = 0; index < ChoiceCount; index++)
        {
            if (choiceButtons[index] == null || choiceLabels[index] == null)
            {
                Debug.LogError(
                    "StartingSpellSelectionUI requires its Panel, eight Buttons, "
                    + "and eight Text labels.",
                    this);
                return false;
            }

            for (int otherIndex = index + 1;
                otherIndex < ChoiceCount;
                otherIndex++)
            {
                if (choiceButtons[index] == choiceButtons[otherIndex])
                {
                    Debug.LogError(
                        "StartingSpellSelectionUI requires eight different "
                        + "Button references.",
                        this);
                    return false;
                }

                if (choiceLabels[index] == choiceLabels[otherIndex])
                {
                    Debug.LogError(
                        "StartingSpellSelectionUI requires eight different "
                        + "Text label references.",
                        this);
                    return false;
                }
            }
        }

        return true;
    }

    private bool ValidateStartingChoices(
        IReadOnlyList<SkillDefinition> choices)
    {
        if (choices == null || choices.Count != ChoiceCount)
        {
            Debug.LogError(
                $"StartingSpellSelectionUI requires exactly {ChoiceCount} choices.",
                this);
            return false;
        }

        for (int index = 0; index < ChoiceCount; index++)
        {
            SkillDefinition choice = choices[index];

            if (choice == null || choice.Type != SkillType.Active)
            {
                Debug.LogError(
                    "StartingSpellSelectionUI accepts only Active Skill Definitions.",
                    this);
                return false;
            }

            for (int otherIndex = index + 1;
                otherIndex < ChoiceCount;
                otherIndex++)
            {
                if (choices[otherIndex] != null
                    && string.Equals(
                        choice.Id,
                        choices[otherIndex].Id,
                        StringComparison.Ordinal))
                {
                    Debug.LogError(
                        "StartingSpellSelectionUI requires eight different choices.",
                        this);
                    return false;
                }
            }
        }

        return true;
    }

    private void ApplyChoiceLabels()
    {
        if (startingChoices == null)
        {
            return;
        }

        for (int index = 0; index < ChoiceCount; index++)
        {
            choiceLabels[index].text = startingChoices[index].DisplayName;
        }
    }

    private void SetButtonsInteractable(bool interactable)
    {
        if (choiceButtons == null)
        {
            return;
        }

        for (int index = 0; index < choiceButtons.Length; index++)
        {
            if (choiceButtons[index] != null)
            {
                choiceButtons[index].interactable = interactable;
            }
        }
    }

    private void TrySelectChoice(int choiceIndex)
    {
        if (!isInitialized
            || !IsShowing
            || isChoiceLocked
            || controller == null
            || startingChoices == null
            || choiceIndex < 0
            || choiceIndex >= ChoiceCount)
        {
            return;
        }

        isChoiceLocked = true;
        SetButtonsInteractable(false);

        if (controller.TrySelectStartingSpell(
                startingChoices[choiceIndex].Id))
        {
            return;
        }

        isChoiceLocked = false;
        SetButtonsInteractable(true);
    }
}
