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
    [SerializeField] Cursor cursor; // Cursor

    [SerializeField] Image timerBar;

    [SerializeField] float spellChooseMaxTime; // Max time for buffer
    float spellChooseTime; // Time that counts down
    [SerializeField] float bufferMaxTime;
    float bufferTime; // Time for buffer state
    [SerializeField] float busyMaxTime; // Time between casting spell and being able to select a new spell
    float busyTime;


    //debug purposes
    [SerializeField] List<GameObject> bulletTypes;
    [SerializeField] int testBulletType;
    [SerializeField] Transform firePoint;

    Vector2 mousePosition;

    Element trigger1; // First element selected
    Element trigger2; // Second element selected

    /* FOR CASTING SPELLS:
    - Make 4x4 2D array (index 0-3 both sides)
    - Create a list of all the spell GameObjects
    - In the 2D array, set the values of each cell to whatever corresponds to the index of the spell in the list.
    - Get the cell by taking trigger1.Type and trigger2.Type
    - Instantiate the spell
    */

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

            if (Input.GetKeyDown(KeyCode.LeftShift)) // Press shift to open spell menu
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

            ChooseElementsFromSpellList(); // Logic to select the spells

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
            cursor.gameObject.SetActive(true);
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            // Fire
            if (Input.GetMouseButtonDown(0)) { // Left click
                //set bullet
                GameObject bulletPrefab = bulletTypes[testBulletType];

                //fire bullet
                Vector3 targetDir = cursor.transform.position - firePoint.position;
                Quaternion bulletRot = Quaternion.LookRotation(Vector3.forward, targetDir);
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRot);

                mousePosition = worldPosition; // Use mousePosition as the trajectory of the spell
                Debug.Log("Switching to Busy state");
                busyTime = busyMaxTime;
                playerState = PlayerState.Busy;
            }

        }
        else if (playerState == PlayerState.Busy) {
            // Cast the spell, then after a slight delay switch back to Idle
            Debug.Log("Kaboom!"); // Testing for spell cast

            cursor.gameObject.SetActive(false);
            busyTime -= Time.deltaTime;

            if (busyTime <= 0) {
                Debug.Log("Switching to Idle state");
                playerState = PlayerState.Idle;
            }
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
                trigger2.Triggered = true;
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
                trigger2.Triggered = true;
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
                trigger2.Triggered = true;
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
            trigger1.Triggered = false;
            trigger2.Triggered = false;

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