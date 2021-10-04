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
    [SerializeField] Image hpBar;

    [SerializeField] float spellChooseMaxTime; // Max time for buffer
    float spellChooseTime; // Time that counts down
    [SerializeField] float bufferMaxTime;
    float bufferTime; // Time for buffer state
    [SerializeField] float busyMaxTime; // Time between casting spell and being able to select a new spell
    float busyTime;
    float menuCountMaxTime = 1f; // Amount of time menu stays up after spells are selected, to give time for sounds to play.
    float menuCount;

    GameObject bulletPrefab;

    //projectiles
    [SerializeField] List<GameObject> bulletTypes;
    [SerializeField] Transform firePoint;

    [SerializeField] float health;
    [SerializeField] float maxHealth;

    //Animations
    [SerializeField] List<Sprite> walkingSprites;
    [SerializeField] List<Sprite> castingSprites;

    // Animation States
    SpriteAnimator walkAnim;
    SpriteAnimator castAnim;
    SpriteAnimator currentAnim;
    [SerializeField] SpriteRenderer spriteRenderer;

    Vector2 mousePosition;

    Element trigger1; // First element selected
    Element trigger2; // Second element selected

    //debug
    [SerializeField] bool debug;
    [SerializeField] int testBulletType;

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
        health = maxHealth;
    }

    private void Start()
    {
        walkAnim = new SpriteAnimator(walkingSprites, spriteRenderer, 0.16f);
        castAnim = new SpriteAnimator(castingSprites, spriteRenderer, 0.16f);

        currentAnim = walkAnim;
    }

    private void Update()
    {
        hpBar.rectTransform.localScale = new Vector3(GetNormalizedHP(), 1.0f, 1.0f);

        currentAnim.HandleUpdate();

        if (playerState == PlayerState.Idle)
        { // Player state idle
            spellChooseTime = spellChooseMaxTime;

            if (Input.GetKeyDown(KeyCode.LeftShift)) // Press shift to open spell menu
            {
                Debug.Log("Switching to spell choosing state");
                menuCanvas.gameObject.SetActive(true);

                trigger1 = null;
                trigger2 = null;

                for (int i = 0; i < menuChoices.Count; i++)
                {
                    // Randomize elements here
                    int randomElement = Random.Range(0, 4); // Get random int between 0-3
                    menuChoices[i].SetElementType(randomElement);
                    menuChoices[i].Triggered = false;
                    menuChoices[i].audioHasPlayed = false;
                    
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

                health -= 2f;
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
            menuCount -= Time.deltaTime;
            if (menuCount <= 0) {
                menuCanvas.gameObject.SetActive(false);
            }

            // Aiming
            cursor.gameObject.SetActive(true);
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            // Fire
            if (Input.GetMouseButtonDown(0)) { // Left click
                menuCanvas.gameObject.SetActive(false);
                currentAnim = castAnim;

                if (debug) bulletPrefab = bulletTypes[testBulletType];

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
            //Debug.Log("Kaboom!"); // Testing for spell cast

            cursor.gameObject.SetActive(false);
            busyTime -= Time.deltaTime;

            if (busyTime <= 0) {
                currentAnim = walkAnim;
                Debug.Log("Switching to Idle state");
                bulletPrefab = null; // reset bullet to null
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
                trigger2.Triggered = true;
            }
        }

        if (trigger1 != null && trigger2 != null)
        {


            // Get the spell from a 2D array with trigger1 and trigger 2 first, then execute the rest of the code.
            int bulletIndex = chart[trigger1.Type][trigger2.Type];


            bulletPrefab = bulletTypes[bulletIndex];

            menuCount = menuCountMaxTime;

            Debug.Log("Switching to casting spell state");
            playerState = PlayerState.CastingSpell;
        }
        else {
            spellChooseTime -= Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    float GetTimerBarScale() {
        float normalizedTime = (float)spellChooseTime / spellChooseMaxTime;

        return Mathf.Clamp(normalizedTime, 0, 1);
    }

    float GetNormalizedHP() {
        float normalizedHP = (float)health / maxHealth;

        return Mathf.Clamp(normalizedHP, 0, 1);
    }

    public Transform getFirePoint()
    {
        return firePoint;
    }

    static int[][] chart =
{
                         //FIR  WAT  WIN  EAR
       /*FIRE */ new int[]{0,   1,   3,   6  },
       /*WATER*/ new int[]{1,   2,   4,   7  },
       /*WIND */ new int[]{3,   4,   5,   8  },
       /*EARTH*/ new int[]{6,   7,   8,   9  },
    };

    public float Health => health;

}
