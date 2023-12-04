using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]

public class Tile : MonoBehaviour
{
    //[SerializeField]private Color baseColor, OffsetColor;
    //[SerializeField] private SpriteRenderer _renderer;

    //[SerializeField] private GameObject _highlight;

    //public void Init(bool isOffset)
    //{
    //    _renderer.color = isOffset ? OffsetColor : baseColor;
    //}
    //private void OnMouseEnter()
    //{
    //    _highlight.SetActive(true);
    //}
    //private void OnMouseExit()
    //{
    //    _highlight.SetActive(false); 
    //}

    [Header("Tile Sprites")]
    [SerializeField] private Sprite unclickedTile, flaggedTile, mineTile, mineWrongTile, mineHitTile;
    [SerializeField] private List<Sprite> clickedTiles;

    private SpriteRenderer spriteRenderer;
    public bool flagged = false;
    public bool active = true;
    public bool isMine = false;
    public int mineCount = 0;


    [Header("Manager Via Code")]
    public SweeperManager sweepManager;

    private void Awake()
    {
        //This Should exist due to the RequireComponent Helper
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseOver()
    {
        if (active)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //LeftClick reveals Tile
                ClickedTile();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                //Place a flag or remove a flag
                flagged = !flagged;
                if (flagged)
                {
                    spriteRenderer.sprite = flaggedTile;
                }
                else
                {
                    spriteRenderer.sprite = unclickedTile;
                }

            }
        }
    }
    public void ClickedTile()
    {
        if (active && !flagged)
        {
            active = false;
            if (isMine)
            {
                //Uh Oh
                spriteRenderer.sprite = mineHitTile;
                UIManager._instance.SpecimenReleased();
            }
            else
            {
                //tile is safe
                spriteRenderer.sprite = clickedTiles[mineCount];
            }
            if (mineCount == 0)
            {
                SweeperManager._instance.ClickNeighbours(this);
            }
        }
    }
}
