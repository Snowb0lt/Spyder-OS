using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class SweeperManager : MonoBehaviour
{
    [SerializeField] private Transform tilePrefab, gameHolder;
    private int width, height, numMines;

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
    }
    public void CreateGameBoard(int width, int height, int numMines)
    {
        //save the game parameters we're using.
        this.width = width;
        this.height = height;
        this.numMines = numMines;

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

    [SerializeField] private List<GameObject> Clear;

    [SerializeField] private Animation SpecimenAnim;
    [SerializeField] private GameObject Specimen;

    [SerializeField] private GameObject SpecimenHiss;
    public void GameOver()
    {
        //Screen Goes black
        foreach (GameObject obj in Clear)
        {
            obj.SetActive(false);
        }
        SpecimenHiss.SetActive(true);
        
        //Spider...
        //Movement

        Invoke("KillPlayer", 3);
        //Animations
        SpecimenAnim.Play("taunt");


        //Spider Screech
        
        //DeathSound

        //GameOver Text
    }
    private void KillPlayer()
    {
        Specimen.SetActive(true);
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
            Debug.Log("Winner! Move on");
            foreach (Tile tile in tiles)
            {
                tile.active = false;
                tile.SetFlaggedIfMine();
            }
            UIManager._instance.SpecimenContained();
        }
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
}
