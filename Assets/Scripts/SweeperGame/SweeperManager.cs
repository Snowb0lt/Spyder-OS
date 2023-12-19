using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class SweeperManager : MonoBehaviour
{
    [SerializeField] private Transform tilePrefab, gameHolder;
    private int width, height;
    public int numMines;

    private List<Tile> tiles = new();

    private readonly float tileSize = 0.32f;

    public static SweeperManager _instance;

    private void Awake()
    {
        if (_instance == null || _instance != this)
        {
            _instance = this;
        }
    }

    private void Update()
    {
        if (UIManager._instance.timeLeft <= 0)
        {
            GameOver();
        }
        //Wins the game instantly (For testing)
        if (Input.GetKeyDown(KeyCode.P))
        {
            Victory();
        }
    }
    public void CreateGameBoard(int width, int height, int numMines)
    {
        //save the game parameters we're using.
        this.width = width;
        this.height = height;
        this.numMines = numMines;

        UIManager._instance.NumberofMinesDisplay(numMines);
        UIManager._instance.mineDisplay.SetActive(true);

        //create the array of tiles.
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                //Position the tile in the correct place (centered).
                Transform tileTransform = Instantiate(tilePrefab);
                tileTransform.parent = gameHolder;
                float xIndex = col - ((width -1)/2.0f);
                float yIndex = row - ((width -1)/2.0f);
                tileTransform.localPosition = new Vector2(xIndex * tileSize, yIndex * tileSize);

                //Keep a reference to the til for setting up the game
                Tile tile = tileTransform.GetComponent<Tile>();
                tiles.Add(tile);

            }
        }
    }

    public void ClickNeighbours(Tile tile)
    {
        int location = tiles.IndexOf(tile);
        foreach (int pos in GetNeighbours(location))
        {
            tiles[pos].ClickedTile();
        }
    }

    public void ResetGameState()
    {
        //Randomly shuffle the tile posiiton to get indices for mine positions.
        int[] minePositions = (int[])Enumerable.Range(0, tiles.Count).OrderBy(x => Random.Range(0.0f, 1.0f)).ToArray(); 

        for (int i = 0; i < numMines; i++)
        {
            int pos = minePositions[i];
            tiles[pos].isMine = true;
        }
        for (int i = 0; i < tiles.Count; ++i)
        {
            tiles[i].mineCount = HowManyMines(i);
        }
    }

    //Given a location work out how many mines are surrounding it
    private int HowManyMines(int location)
    {
        int count = 0;
        foreach (int pos in GetNeighbours(location))
        {
            if (tiles[pos].isMine)
            {
                count++;
            }
        }
        return count;
    }

    private List<int> GetNeighbours(int pos)
    {
        List<int> neighbours = new();
        int row = pos / width;
        int col = pos % width;

        //(0,0) is bottom left
        if (row < (height - 1))
        {
            neighbours.Add(pos + width); //North
            if (col > 0)
            {
                neighbours.Add(pos + width - 1); //North-West
            }
            if (col < (width - 1))
            {
                neighbours.Add(pos + width + 1); //North-East
            }
        }
        if (col > 0)
        {
            neighbours.Add(pos - 1); //West
        }
        if (col < (width - 1))
        {
            neighbours.Add(pos + 1); //East
        }

        if (row > 0)
        {
            neighbours.Add(pos - width); //South
            if (col > 0)
            {
                neighbours.Add(pos - width - 1); //South-West
            }
            if (col < (width - 1))
            {
                neighbours.Add(pos - width + 1); //South-East
            }
        }


        return neighbours;
    }
    [Header("GameOver Items")]
    [SerializeField] private List<GameObject> Clear;
    [SerializeField] private GameObject blackScreen;

    [Header("Specimen Loose")]
    [SerializeField] private Animation SpecimenAnim;
    [SerializeField] private GameObject Specimen;

    [SerializeField] private GameObject SpecimenHiss, BackgroundNoise;
    public void GameOver()
    {
        //Screen Goes black
        foreach (GameObject obj in Clear)
        {
            obj.SetActive(false);
        }
        SpecimenHiss.SetActive(true);
        Invoke("KillPlayer", 1.5f);
        //Animations
        SpecimenAnim.Play("taunt");
        Invoke("CTBGameOver", 2);
    }
    private void KillPlayer()
    {
        Specimen.SetActive(true);
    }
    private void CTBGameOver()
    {
        blackScreen.SetActive(true);
        UIManager._instance.Invoke("ShowGameOverScreen", 2);
    }

    public void CheckGameWon()
    {
        int count = 0;

        foreach (Tile tile in tiles)
        {
            
            if (tile.active && !tile.isMine)
            {
                count++;
            }
        }
        if (count == 0)
        {
            Victory();
        }
    }
    [Header("Winning")]
    [SerializeField] private UnityEvent startCongrats;
    private void Victory()
    {
        Debug.Log("Winner! Move on");
        UIManager._instance.SpecimenContained();
        foreach (Transform tile in gameHolder.transform)
        {
            Destroy(tile.gameObject);
        }
        UIManager._instance.GameWon();
        startCongrats.Invoke();
    }



    public void ExpandIfFlagged(Tile tile)
    {
        int location = tiles.IndexOf(tile);
        int flag_count = 0;
        foreach (int pos in GetNeighbours(location)) 
        {
            if (tiles[pos].flagged)
            {
                flag_count++;
            }
            if (flag_count == tile.mineCount)
            {
                ClickNeighbours(tile);
            }
        }
    }

    //public void ResetSpeciesTracker()
    //{
    //    if (UIManager._instance.isSpecimenOut)
    //    {
    //        UIManager._instance.isSpecimenOut = false;
    //    }
    //}

    //Run a tutorial for the game
    public void RunTutorial()
    {

    }
}
