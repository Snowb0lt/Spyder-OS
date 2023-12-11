using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDifficulty : MonoBehaviour
{
    public void RunTutorial()
    {
        SweeperManager._instance.RunTutorial(); //Runs A Tutorial Session
       // HideButtons();
    }
    public void EasyDifficulty()
    {
        SweeperManager._instance.CreateGameBoard(9, 9, 10); //Easy
        SweeperManager._instance.ResetGameState();
        //HideButtons();
    }
    public void InterDifficulty()
    {
        SweeperManager._instance.CreateGameBoard(16, 16, 40); //Intermediate
        SweeperManager._instance.ResetGameState();
       // HideButtons();
    }
    public void HardDifficulty()
    {
        SweeperManager._instance.CreateGameBoard(30,16,99); //Expert
        SweeperManager._instance.ResetGameState();
       // HideButtons();
    }
    public void NightDifficulty()
    {
        SweeperManager._instance.CreateGameBoard(30,30,180); //Nightmare
        SweeperManager._instance.ResetGameState();
        //HideButtons();
    }
    
    [SerializeField] private GameObject _choices;
    
    private void HideButtons()
    {
        _choices.SetActive(false);
    }
}
