using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger _instance;

    private void Awake()
    {
        if( _instance == null || _instance !=this)
        {
            _instance = this;
        }
    }
    public void BackToMenu()
    {
        MoveScene(0);
    }
    public void ToMainGame()
    {
        MoveScene(1);
    }

    private void MoveScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    } 
}
