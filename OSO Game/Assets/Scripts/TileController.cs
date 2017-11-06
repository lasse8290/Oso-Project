using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {

    public static TileController Instace { get; set; }

    public GameObject TileWhite;
    public GameObject TileBlue;
    public GameObject TileRed;


    Levels ls = new Levels();

    int sizex;

    public int SizeX { get { return sizex; } }

    int sizey;

    public int SizeY { get { return sizey; } }

    Tile[,] TilemapCruent;
    Tile[,] TilemapEnd;


    private void Awake () {

        Instace = this;
        Loadlevel(0);
    }

    void Loadlevel (int Level) {

        //get and set the level start and end
        ls.GetLevel(0, out TilemapCruent, out TilemapEnd);

        sizex = TilemapCruent.GetLength(0) - 1;
        sizey = TilemapCruent.GetLength(1) - 1;

        //spawn the tiles
        for (int x = 0; x < TilemapCruent.GetLength(0); x++) {
            for (int y = 0; y < TilemapCruent.GetLength(1); y++) {

                switch (TilemapCruent[x, y].Tilecolor) {
                    case Tile.TileColor.white:

                        var goW = Instantiate(TileWhite, new Vector3(x, y, 0), Quaternion.identity);

                        goW.name = string.Format("Tile:{0}_{1}, color: White", x, y);

                        break;

                    case Tile.TileColor.blue:
                        var goB = Instantiate(TileBlue, new Vector3(x, y, 0), Quaternion.identity);

                        goB.name = string.Format("Tile:{0}_{1}, color: White", x, y);

                        break;

                    case Tile.TileColor.red:
                        var goR = Instantiate(TileRed, new Vector3(x, y, 0), Quaternion.identity);

                        goR.name = string.Format("Tile:{0}_{1}, color: White", x, y);

                        break;

                    default:
                        break;
                }
            }
        }
    }


    public void UpdateTileAt (Tile t) {

        GameObject go = GameObject.Find(string.Format("Tile:{0}_{1}, color: {2}", t.X, t.Y, t.PreTilecolor));

        Destroy(go);

        switch (TilemapCruent[t.X, t.Y].Tilecolor) {
            case Tile.TileColor.white:

                var goW = Instantiate(TileWhite, new Vector3(t.X, t.Y, 0), Quaternion.identity);

                goW.name = string.Format("Tile:{0}_{1}, color: {2}", t.X, t.Y, t.Tilecolor);

                break;

            case Tile.TileColor.blue:

                var goB = Instantiate(TileBlue, new Vector3(t.X, t.Y, 0), Quaternion.identity);

                goB.name = string.Format("Tile:{0}_{1}, color: {2}", t.X, t.Y, t.Tilecolor);

                break;

            case Tile.TileColor.red:

                var goR = Instantiate(TileRed, new Vector3(t.X, t.Y, 0), Quaternion.identity);

                goR.name = string.Format("Tile:{0}_{1}, color: {2}", t.X, t.Y, t.Tilecolor);

                break;

            default:
                break;
        }
        CheckLevelComplete();
    }

    public bool CheckLevelComplete () {

        for (int x = 0; x < sizex; x++) {
            for (int y = 0; y < sizey; y++) {

                if (TilemapCruent[x, y] != TilemapEnd[x, y]) {
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

        return TilemapCruent[x, y];

    }

    /// <summary>
    /// get the tile at the passed X Y
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Tile GetTileAt (int x, int y) {

        if (x < 0 || y < 0 || y > sizey || x > sizex) {

            //Debug.LogErrorFormat("GetTile Overflow {0},{1}", x, y);
            return null;
        }

        return TilemapCruent[x, y];
    }
}
