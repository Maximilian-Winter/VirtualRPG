using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CombatState { NOT_IN_COMBAT, COMBAT_INITIATED, COMBAT_IN_TURN, COMBAT_TURN_FINISHED, COMBAT_ROUND_FINISHED, COMBAT_FINISHED}

public delegate void OnTurnEnter();
public delegate void OnTurnExit();
public delegate void OnRoundExit();
public delegate void OnCombatExit();
public delegate bool IsTurnFinished();

public class CombatSystem : MonoBehaviour
{
    public class Combatant : IComparable<Combatant>
    {
        public Combatant(string name, float turnOrderIndex, OnTurnEnter onTurnEnter, OnTurnExit onTurnExit, OnRoundExit onRoundExit, OnCombatExit onCombatExit, IsTurnFinished isTurnFinished)
        {
            this.Name = name;
            this.TurnOrderIndex = turnOrderIndex;
            this.OnTurnEnter = onTurnEnter;
            this.OnTurnExit = onTurnExit;
            this.OnRoundExit = onRoundExit;
            this.OnCombatExit = onCombatExit;
            this.IsTurnFinished = isTurnFinished;
        }
        string name;
        float turnOrderIndex;
        OnTurnEnter onTurnEnter;
        OnTurnExit onTurnExit;
        OnRoundExit onRoundExit;
        OnCombatExit onCombatExit;
        IsTurnFinished isTurnFinished;

        public string Name { get => name; set => name = value; }
        public float TurnOrderIndex { get => turnOrderIndex; set => turnOrderIndex = value; }
        public OnTurnEnter OnTurnEnter { get => onTurnEnter; set => onTurnEnter = value; }
        public OnTurnExit OnTurnExit { get => onTurnExit; set => onTurnExit = value; }
        public OnRoundExit OnRoundExit { get => onRoundExit; set => onRoundExit = value; }
        public OnCombatExit OnCombatExit { get => onCombatExit; set => onCombatExit = value; }
        public IsTurnFinished IsTurnFinished { get => isTurnFinished; set => isTurnFinished = value; }

        public int CompareTo(Combatant other)
        {
            return turnOrderIndex.CompareTo(other.turnOrderIndex);
        }
    }

    [SerializeField]
    private ApplicationStates applicationStates;

    private List<Combatant> combatants;
    private Combatant currentCombatant;
    CombatState combatState;
    // Start is called before the first frame update
    void Start()
    {
        combatState = CombatState.NOT_IN_COMBAT;
        applicationStates.isInCombatMode = false;
        combatants = new List<Combatant>();
    }

    // Update is called once per frame
    void Update()
    {
        if (combatState == CombatState.COMBAT_INITIATED)
        {
            combatants.Sort();
            combatants.Reverse();
            currentCombatant = combatants.FirstOrDefault(x => !x.IsTurnFinished());

            if(currentCombatant == null)
            {
                combatState = CombatState.COMBAT_ROUND_FINISHED; 
            }
            else
            {
                currentCombatant.OnTurnEnter();
                combatState = CombatState.COMBAT_IN_TURN;
            }
        }
        else if (combatState == CombatState.COMBAT_IN_TURN)
        {
            if(currentCombatant.IsTurnFinished())
            {
                combatState = CombatState.COMBAT_TURN_FINISHED;
                if(combatants.Count == 1)
                {
                    ExitCombatMode();
                }
            }
        }
        else if (combatState == CombatState.COMBAT_TURN_FINISHED)
        {
            currentCombatant.OnTurnExit();
            currentCombatant = combatants.FirstOrDefault(x => !x.IsTurnFinished());
            if(currentCombatant == null)
            {
                combatState = CombatState.COMBAT_ROUND_FINISHED; 
            }
            else
            {
                currentCombatant.OnTurnEnter();
                combatState = CombatState.COMBAT_IN_TURN;  
            }
        }
        else if (combatState == CombatState.COMBAT_ROUND_FINISHED)
        {
            if(combatants != null)
            {
                combatants.Sort();
                combatants.Reverse();
                foreach (Combatant combi in combatants)
                {
                    combi.OnRoundExit();
                }
                currentCombatant = combatants.FirstOrDefault(x => !x.IsTurnFinished());
                if (currentCombatant != null)
                {
                    currentCombatant.OnTurnEnter();
                    combatState = CombatState.COMBAT_IN_TURN;
                }
                else
                {
                    ExitCombatMode();
                }

            }
            else
            {
                ExitCombatMode();
            }

        }
    }

    public void AddCombatant(string name, float turnOrderIndex, OnTurnEnter onTurnEnter, OnTurnExit onTurnExit, OnRoundExit onRoundExit, OnCombatExit onCombatExit, IsTurnFinished isTurnFinished)
    {
        combatants.Add(new Combatant(name, turnOrderIndex, onTurnEnter, onTurnExit, onRoundExit, onCombatExit, isTurnFinished));
        if(!applicationStates.isInCombatMode)
        {
            StartCoroutine(EnterCombatMode());
        }
    }

    public void RemoveCombatant(string name)
    {
        Combatant combi = combatants.FirstOrDefault(x => x.Name == name);
        if(combi != null)
        {
            combatants.Remove(combi);
        }
    }

    public bool CanExitCombat()
    {
        if(combatants.Count == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator EnterCombatMode()
    {
        applicationStates.isInCombatMode = true;
        yield return new WaitForSeconds(0.5f);
        combatState = CombatState.COMBAT_INITIATED;
    }

    public void ExitCombatMode()
    {
        applicationStates.isInCombatMode = false;
        combatState = CombatState.NOT_IN_COMBAT;
        foreach (Combatant combi in combatants)
        {
            combi.OnCombatExit();
        }
        combatants.Clear();
    }
}
