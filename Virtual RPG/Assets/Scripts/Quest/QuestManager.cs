using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

// BEGIN quest_manager
// Represents the player's current progress through a quest.
public class QuestStatus {

    // The underlying data object that describes the quest.
    public Quest questData;

    // The map of objective identifiers.
    public Dictionary<int, Quest.Status> objectiveStatuses;

    // The constructor. Pass a Quest to this to set it up.
    public QuestStatus(Quest questData)
    {
        // Store the quest info
        this.questData = questData;

        // Create the map of objective numbers to their status
        objectiveStatuses = new Dictionary<int, Quest.Status>();

        for (int i = 0; i < questData.objectives.Count; i += 1)
        {
            var objectiveData = questData.objectives[i];

            objectiveStatuses[i] = objectiveData.initalStatus;
        }
    }

    // Returns the state of the entire quest.
    // If all non-optional objectives are complete, the quest is complete.
    // If any non-optional objective is failed, the quest is failed.
    // Otherwise, the quest is not yet complete.
    public Quest.Status questStatus {
        get {
            
            for (int i = 0; i < questData.objectives.Count; i += 1) {
                
                var objectiveData = questData.objectives[i];

                // Optional objectives do not matter to the overall quest
                // status
                if (objectiveData.optional)
                    continue;

                var objectiveStatus = objectiveStatuses[i];

                // this is a mandatory objective
                if (objectiveStatus == Quest.Status.Failed)
                {
                    // if a mandatory objective is failed, the whole 
                    // quest is failed
                    return Quest.Status.Failed;
                }
                else if (objectiveStatus != Quest.Status.Complete)
                {
                    // if a mandatory objective is not yet complete,
                    // the whole quest is not yet complete
                    return Quest.Status.NotYetComplete;
                }
            }

            // All mandatory objectives are complete, so this quest is
            // complete
            return Quest.Status.Complete;

        }
    }

    // Returns a string containing the list of objectives, their statuses,
    // and the status of the quest.
    public override string ToString()
    {
        var stringBuilder = new System.Text.StringBuilder();

        stringBuilder.AppendFormat("Questname: {0}\n", questData.questName);

        for (int i = 0; i < questData.objectives.Count; i += 1)
        {
            // Get the objective and its status
            var objectiveData = questData.objectives[i];
            var objectiveStatus = objectiveStatuses[i];

            // Don't show hidden objectives that haven't been finished
            if (objectiveData.visible == false &&
                objectiveStatus == Quest.Status.NotYetComplete)
            {
                continue;
            }

            // If this objective is optional, display "(Optional)" after
            // its name
            if (objectiveData.optional)
            {
                stringBuilder.AppendFormat("Quest Description: {0} (Optional)\n",
                                           objectiveData.name);
            }
            else
            {
                stringBuilder.AppendFormat("Quest Description: {0}\n", 
                                           objectiveData.name);
            }

        }

        // Add a blank line followed by the quest status
        stringBuilder.AppendFormat(
            "Status: {0}", this.questStatus.ToString());

        return stringBuilder.ToString();
    }
}

// Manages a quest.
public class QuestManager : MonoBehaviour
{

    [SerializeField] List<Quest> quests = null;

    // Tracks the state of the current quests.
    List<QuestStatus> activeQuests;

    void Start ()
    {
        activeQuests = new List<QuestStatus>();
    }


    [YarnCommand("StartQuest")]
    public void StartQuestYarn(string questID)
    {
        foreach (Quest quest in quests)
        {
            if (quest.questId == Int32.Parse(questID))
            {
                activeQuests.Add(new QuestStatus(quest));
            }
        }
    }

    public void StartQuest(int questID)
    {
        foreach (Quest quest in quests)
        {
            if (quest.questId == questID)
            {
                activeQuests.Add(new QuestStatus(quest));
            }
        }
    }

    public void StartQuest(Quest quest)
    {
        activeQuests.Add(new QuestStatus(quest));
    }

    // Called by other objects to indicate that an objective has changed
    // status
    public void UpdateObjectiveStatus(Quest quest, int objectiveNumber, Quest.Status status)
    {
        foreach(QuestStatus questStatus in activeQuests)
        {
            if(questStatus.questData = quest)
            {
                questStatus.objectiveStatuses[objectiveNumber] = status;
            } 
        }  
    }

    public List<string> GetQuestsStatuses()
    {
        List<string> questsStatus = new List<string>();
        foreach (QuestStatus questStatus in activeQuests)
        {
            questsStatus.Add(questStatus.ToString());
        }

        return questsStatus;
    }


}
// END quest_manager