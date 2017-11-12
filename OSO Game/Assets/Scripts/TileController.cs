using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {

    public static TileController Instace { get; set; }

    public GameObject TileWhite;
    public GameObject TileBlue;
    public GameObject TileRed;
    public GameObject TileYellow;

    public GameObject TileSpreadPlus;
    public GameObject TileSpreadMinus;

    List<GameObject> GoTiles = new List<GameObject>();

    Level level;

    int sizex;

    public int SizeX { get { return sizex; } }

    int sizey;

    public int SizeY { get { return sizey; } }

    private void Awake () {

        Instace = this;

    }

    private void Start () {
        GenLevel(0);
    }

    void GenLevel (int LevelToLoad) {

        level = new Level(LevelToLoad);

        sizex = level.TileMapCruent.GetLength(0);
        sizey = level.TileMapCruent.GetLength(1);

        //spawn the tiles
        for (int x = 0; x < level.TileMapCruent.GetLength(0); x++) {
            for (int y = 0; y < level.TileMapCruent.GetLength(1); y++) {

                switch (level.TileMapCruent[x, y].Tilecolor) {

                    case Tile.TileColor.White:

                        var goW = Instantiate(TileWhite, new Vector3(x, y, 0), Quaternion.identity);

                        goW.name = string.Format("Tile:{0}_{1}, color: {2}", x, y, level.TileMapCruent[x, y].Tilecolor);

                        GoTiles.Add(goW);

                        break;

                    case Tile.TileColor.Blue:
                        var goB = Instantiate(TileBlue, new Vector3(x, y, 0), Quaternion.identity);

                        goB.name = string.Format("Tile:{0}_{1}, color: {2}", x, y, level.TileMapCruent[x, y].Tilecolor);

                        GoTiles.Add(goB);

                        break;

                    case Tile.TileColor.Red:
                        var goR = Instantiate(TileRed, new Vector3(x, y, 0), Quaternion.identity);

                        goR.name = string.Format("Tile:{0}_{1}, color: {2}", x, y, level.TileMapCruent[x, y].Tilecolor);

                        GoTiles.Add(goR);

                        break;

                    case Tile.TileColor.Yellow:
                        var goY = Instantiate(TileYellow, new Vector3(x, y, 0), Quaternion.identity);

                        goY.name = string.Format("Tile:{0}_{1}, color: {2}", x, y, level.TileMapCruent[x, y].Tilecolor);

                        GoTiles.Add(goY);

                        break;

                    default:
                        break;
                }
            }
        }
    }

    public void PlaceTileAt (Tile t, Tile.TileColor TC) {

        GetTileAt(t.X, t.Y).ChangeTileColor(TC);

        if (level.Spread > 0 && level.Spread > -1) {

            for (int x = 0; x < level.Spread + 1; x++) {

                if (GetTileAt(t.X + x, t.Y) != null) {
                    GetTileAt(t.X + x, t.Y).ChangeTileColor(TC);
                }
                if (GetTileAt(t.X - x, t.Y) != null) {
                    GetTileAt(t.X - x, t.Y).ChangeTileColor(TC);
                }
                if (GetTileAt(t.X, t.Y + x) != null) {
                    GetTileAt(t.X, t.Y + x).ChangeTileColor(TC);
                }
                if (GetTileAt(t.X, t.Y - x) != null) {
                    GetTileAt(t.X, t.Y - x).ChangeTileColor(TC);
                }
            }
        }
    }


    public void UpdateTileAt (int x, int y) {

        List<GameObject> go = GoTiles.FindAll((g) => g.transform.position == new Vector3(x, y, 0));

        foreach (var g in go) {

            GoTiles.Remove(g);

            Destroy(g);
        }

        switch (level.TileMapCruent[x, y].Tilecolor) {
            case Tile.TileColor.White:

                var goW = Instantiate(TileBlue, new Vector3(x, y, 0), Quaternion.identity);

                goW.name = string.Format("Tile:{0}_{1}, color: {2}", x, y, GetTileAt(x, y).Tilecolor);

                GoTiles.Add(goW);
                break;

            case Tile.TileColor.Blue:

                var goB = Instantiate(TileBlue, new Vector3(x, y, 0), Quaternion.identity);

                goB.name = string.Format("Tile:{0}_{1}, color: {2}", x, y, GetTileAt(x, y).Tilecolor);

                GoTiles.Add(goB);
                break;

            case Tile.TileColor.Red:

                var goR = Instantiate(TileRed, new Vector3(x, y, 0), Quaternion.identity);

                goR.name = string.Format("Tile:{0}_{1}, color: {2}", x, y, GetTileAt(x, y).Tilecolor);

                GoTiles.Add(goR);

                break;
            case Tile.TileColor.Yellow:

                var goY = Instantiate(TileYellow, new Vector3(x, y, 0), Quaternion.identity);

                goY.name = string.Format("Tile:{0}_{1}, color: {2}", x, y, GetTileAt(x, y).Tilecolor);

                GoTiles.Add(goY);
                break;

            default:
                break;
        }

        if (CheckLevelComplete()) {
            Debug.LogWarning("True");
        }
    }

    public bool CheckLevelComplete () {

        for (int x = 0; x < sizex; x++) {
            for (int y = 0; y < sizey; y++) {

                if (level.TileMapCruent[x, y].Tilecolor != level.TileMapEnd[x, y].Tilecolor) {
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// Get the tile closest to the passed cords
    /// </summary>
    /// <param name="cord"></param>
    /// <returns></returns>
    public Tile GetTileAt (Vector3 cord) {

        int x = Mathf.FloorToInt(cord.x);
        int y = Mathf.FloorToInt(cord.y);

        if (x < 0 || y < 0 || y > sizey || x > sizex) {

            //Debug.LogErrorFormat("GetTile Overflow {0},{1}", x, y);
            return null;
        }

        return level.TileMapCruent[x, y];
    }

    /// <summary>
    /// get the tile at the passed X Y
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Tile GetTileAt (int x, int y) {

        if (x < 0 || y < 0 || y >= sizey || x >= sizex) {

            //Debug.LogErrorFormat("GetTile Overflow {0},{1}", x, y);
            return null;
        }

        return level.TileMapCruent[x, y];
    }
}
