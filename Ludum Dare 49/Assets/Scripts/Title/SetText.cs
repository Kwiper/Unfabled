using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{
    [SerializeField] OnHover adventureHover;
    [SerializeField] OnHover survivalHover;

    [SerializeField] Text adventureText;
    [SerializeField] Text survivalText;
    [SerializeField] Text infoText;

    private void Start()
    {
        adventureText.color = new Color(1, 1, 1);
        survivalText.color = new Color(1, 1, 1);
    }

    private void Update()
    {
        adventureText.color = new Color(adventureHover.RGB, adventureHover.RGB, adventureHover.RGB);
        survivalText.color = new Color(survivalHover.RGB, survivalHover.RGB, survivalHover.RGB);

        if (adventureHover.GetMouseHover())
        {
            infoText.text = "Adventure through the woods and save your unstabled horse!";
        }
        else if (survivalHover.GetMouseHover())
        {
            infoText.text = "Survive for as long as you can!";
        }
        else if (!adventureHover.GetMouseHover() && !survivalHover.GetMouseHover()) {
            infoText.text = "Select a game mode";
        }

    }


}
