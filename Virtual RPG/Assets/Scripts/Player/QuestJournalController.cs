using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestJournalController : MonoBehaviour
{

    [SerializeField]
    private QuestManager questManager;

    [SerializeField]
    private GameObject questJournalUIPanel;

    [SerializeField]
    private TextMeshProUGUI questJournalText;

    private bool isQuestJournalOpen;

    public bool IsQuestJournalOpen { get => isQuestJournalOpen; set => isQuestJournalOpen = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteractWithQuestJournal()
    {
        if(!questJournalUIPanel.activeSelf)
        {
            var stringBuilder = new System.Text.StringBuilder();


            List<string> questsStatuses = questManager.GetQuestsStatuses();
            foreach (string questStatus in questsStatuses)
            {
                stringBuilder.AppendFormat("{0}\n", questStatus);
            }

            questJournalText.text = stringBuilder.ToString();
            questJournalUIPanel.SetActive(true);
            IsQuestJournalOpen = true;
        }
        else
        {
            questJournalUIPanel.SetActive(false);
            IsQuestJournalOpen = false;
        }
    }
}
