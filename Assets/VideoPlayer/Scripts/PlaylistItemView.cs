using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaylistItemView : MonoBehaviour
{
    public Image Preview => preview;
    public Text Title => title;
    public Button Button => button;

    [SerializeField] private Image preview;
    [SerializeField] private Text title;
    [SerializeField] private Button button;

    public void Select()
    {
        button.image.color = button.colors.pressedColor;
    }

    public void Deselect()
    {
        button.image.color = button.colors.normalColor;
    }
}
