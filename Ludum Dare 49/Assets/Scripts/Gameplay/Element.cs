using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    int type = 0; // 0 = fire; 1 = water; 2 = wind; 3 = earth;
    [SerializeField] List<string> names;
    string name;

    [SerializeField] Color highlightedColor;

    public List<Sprite> sprites;

    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        image.sprite = sprites[type];
        name = names[type];

        if (this.Triggered)
        {
            image.color = highlightedColor;
        }
        else {
            image.color = Color.white;
        }
    }

    public void SetElementType(int type) {
        this.type = type;
    }

    public bool Triggered { get; set; }

    public int Type => type;

    public string Name => name;

}
