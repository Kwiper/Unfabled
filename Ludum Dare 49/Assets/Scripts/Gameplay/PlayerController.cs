using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState { 
    Idle, // Can go to choosing spell state in Idle
    ChoosingSpell, // Selects spell. Goes to buffer if fails to cast spell, or to CastingSpell if a spell is created.
    Buffer, // Buffer between choosing spell failing and switching to idle state
    CastingSpell, // During this state, the player aims to cast the spell.
    Busy // Player cannot do anything during this state.
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] Canvas menuCanvas; // Menu selection canvas
    [SerializeField] List<Element> menuChoices; // List of the selection UI stuff

    [SerializeField] Image timerBar;

    [SerializeField] float spellChooseMaxTime; // Max time for buffer
    float spellChooseTime; // Time that counts down
    [SerializeField] float bufferMaxTime;
    float bufferTime; // Time for buffer state

    Element trigger1;
    Element trigger2;

    PlayerState playerState; // Gets the player state enum

    private void Awake()
    {
        playerState = PlayerState.Idle;
    }



    private void Update()
    {
        if (playerState == PlayerState.Idle)
        { // Player state idle
            spellChooseTime = spellChooseMaxTime;

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("Switching to spell choosing state");
                menuCanvas.gameObject.SetActive(true);

                for (int i = 0; i < menuChoices.Count; i++)
                {
                    // Randomize elements here
                    int randomElement = Random.Range(0, 4); // Get random int between 0-3
                    menuChoices[i].SetElementType(randomElement);
                }

                playerState = PlayerState.ChoosingSpell;

            }

        }
        else if (playerState == PlayerState.ChoosingSpell)
        {
            timerBar.rectTransform.localScale = new Vector3(GetTimerBarScale(), 1.0f, 1.0f);

            ChooseElementsFromSpellList();

            if (spellChooseTime <= 0)
            {
                menuCanvas.gameObject.SetActive(false);
                Debug.Log("Switching to Buffer state");
                bufferTime = bufferMaxTime;
                playerState = PlayerState.Buffer;
            }

        }
        else if (playerState == PlayerState.Buffer)
        {
            bufferTime -= Time.deltaTime;
            if (bufferTime <= 0)
            {
                Debug.Log("Switching to Idle state");
                playerState = PlayerState.Idle;
            }

        }
        else if (playerState == PlayerState.CastingSpell)
        {
            // Aiming
        }
        else if (playerState == PlayerState.Busy) { 
            // Cast the spell, switch back to Idle
        }
    }

    void ChooseElementsFromSpellList() {
        if (Input.GetKeyDown(KeyCode.W) && !menuChoices[0].Triggered) { // Select the top element
            if (trigger1 == null)
            {
                trigger1 = menuChoices[0];
                trigger1.Triggered = true;
            }
            else {
                trigger2 = menuChoices[0];
                menuChoices[0].Triggered = true;
            }
        }        
        else if (Input.GetKeyDown(KeyCode.D) && !menuChoices[1].Triggered) { // Select the right element
            if (trigger1 == null)
            {
                trigger1 = menuChoices[1];
                trigger1.Triggered = true;
            }
            else {
                trigger2 = menuChoices[1];
                menuChoices[1].Triggered = true;
            }
        }        
        else if (Input.GetKeyDown(KeyCode.S) && !menuChoices[2].Triggered) { // Select the bottom element
            if (trigger1 == null)
            {
                trigger1 = menuChoices[2];
                trigger1.Triggered = true;
            }
            else {
                trigger2 = menuChoices[2];
                menuChoices[2].Triggered = true;
            }
        }        
        else if (Input.GetKeyDown(KeyCode.A) && !menuChoices[3].Triggered) { // Select the left element
            if (trigger1 == null)
            {
                trigger1 = menuChoices[3];
                trigger1.Triggered = true;
            }
            else {
                trigger2 = menuChoices[3];
                menuChoices[3].Triggered = true;
            }
        }

        if (trigger1 != null && trigger2 != null)
        {
            // Get the spell from a 2D array with trigger1 and trigger 2 first, then execute the rest of the code. This can't be done
            // until we actually have spells.

            trigger1 = null;
            trigger2 = null;

            menuCanvas.gameObject.SetActive(false);

            Debug.Log("Switching to casting spell state");
            playerState = PlayerState.CastingSpell;
        }
        else {
            spellChooseTime -= Time.deltaTime;
        }
    
    }

    float GetTimerBarScale() {
        float normalizedTime = (float) spellChooseTime / spellChooseMaxTime;

        return Mathf.Clamp(normalizedTime, 0, 1);
    }

}