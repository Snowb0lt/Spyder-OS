using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]private Color baseColor, OffsetColor;
    [SerializeField] private SpriteRenderer _renderer;

    [SerializeField] private GameObject _highlight;

    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? OffsetColor : baseColor;
    }
    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }
    private void OnMouseExit()
    {
        _highlight.SetActive(false); 
    }
}
