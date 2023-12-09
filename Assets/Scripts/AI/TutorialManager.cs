using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    //Teach Player About the Tiles
    [SerializeField] private GameObject tilePrefab,gameHolder;

    //Holds the other Dialogues of the tutorial
    [SerializeField] private GameObject tutorialPt2;

    public void TeachTiles()
    {
        Instantiate(tilePrefab, gameHolder.transform);

        if (tilePrefab.GetComponent<Tile>().active == false)
        {
            Destroy(tilePrefab);
            AISpeech._instance.StartDialogue(tutorialPt2.GetComponent<Dialogue>());
        }
    }

    public void TeachMultiUnlock()
    {

    }

}
