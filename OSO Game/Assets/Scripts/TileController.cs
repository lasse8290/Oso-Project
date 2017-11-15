using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {

    public static TileController Instace { get; set; }

    public GameObject TileWhite;
    public GameObject TileBlue;
    public GameObject TileRed;
    public GameObject TileYellow;

    public GameObject TilePurple;
    public GameObject TileOrange;
    public GameObject TileGreen;

    public GameObject TileBlack;

    public GameObject TileSpreadPlus;
    public GameObject TileSpreadMinus;

    List<GameObject> GoTiles = new List<GameObject>();
    List<GameObject> GoModifers = new List<GameObject>();

    Level level;

    public Level Level { get { return level; } }

    [SerializeField]
    public int Spead;

    int sizex;

    public int SizeX { get { return sizex; } }

    int sizey;

    public int SizeY { get { return sizey; } }

    private void Awake () {

        Instace = this;

    }

    public void GenLevel (int LevelToLoad) {

        level = new Level(LevelToLoad);

        Spead = level.Spread;

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

                switch (level.TileMapCruent[x,y].Tilemodifer) {
                    case Tile.TileModifer.None:
                        break;
                    case Tile.TileModifer.BiggerSpread:
                        var goSB = Instantiate (TileSpreadPlus, new Vector3(x + 0.5f, y + 0.5f, -0.1f), Quaternion.identity);

                        goSB.name = string.Format("SpreadPlus{0},{1}", x, y);

                        GoModifers.Add(goSB);


                        break;
                    case Tile.TileModifer.SmallSpread:
                        var goSS = Instantiate(TileSpreadMinus, new Vector3(x + 0.5f, y + 0.5f, -0.1f), Quaternion.identity);

                        goSS.name = string.Format("SpreadMinus{0},{1}", x, y);

                        GoModifers.Add(goSS);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void PlaceTileAt (Tile t, Tile.TileColor TC) {

        GetTileAt(t.X, t.Y).ChangeTileColor(ColorCalutor(GetTileAt(t.X, t.Y).Tilecolor, TC));

        if (level.Spread > 0 && level.Spread > -1) {

            for (int x = 0; x < level.Spread + 1; x++) {

                if (GetTileAt(t.X + x, t.Y) != null) {
                    GetTileAt(t.X + x, t.Y).ChangeTileColor(ColorCalutor(GetTileAt(t.X + x, t.Y).Tilecolor, TC));
                }
                if (GetTileAt(t.X - x, t.Y) != null) {
                    GetTileAt(t.X - x, t.Y).ChangeTileColor(ColorCalutor(GetTileAt(t.X - x, t.Y).Tilecolor, TC));
                }
                if (GetTileAt(t.X, t.Y + x) != null) {
                    GetTileAt(t.X, t.Y + x).ChangeTileColor(ColorCalutor(GetTileAt(t.X, t.Y + x).Tilecolor, TC));
                }
                if (GetTileAt(t.X, t.Y - x) != null) {
                    GetTileAt(t.X, t.Y - x).ChangeTileColor(ColorCalutor(GetTileAt(t.X, t.Y - x).Tilecolor, TC));
                }
            }
        }
    }


    public void UpdateTileAt (int x, int y) {

        foreach (var g in GoModifers.FindAll((g) => g.transform.position == new Vector3(x + 0.5f, y + 0.5f, -0.1f))) {

            GoModifers.Remove(g);

            Destroy(g);
        }

        foreach (var g in GoTiles.FindAll((g) => g.transform.position == new Vector3(x, y, 0))) {

            GoTiles.Remove(g);

            Destroy(g);
        }

        switch (level.TileMapCruent[x,y].Tilemodifer) {
            case Tile.TileModifer.None:
                break;
            case Tile.TileModifer.BiggerSpread:
                level.Spread++;
                level.TileMapCruent[x, y].ChangeTileModifer(Tile.TileModifer.None);

                break;
            case Tile.TileModifer.SmallSpread:
                level.Spread--;
                level.TileMapCruent[x, y].ChangeTileModifer(Tile.TileModifer.None);
                
                break;
            default:
                break;
        }

        switch (level.TileMapCruent[x, y].Tilecolor) {
            case Tile.TileColor.White:

                var goW = Instantiate(TileWhite, new Vector3(x, y, 0), Quaternion.identity);

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

            case Tile.TileColor.Purple:

                var goP = Instantiate(TilePurple, new Vector3(x, y, 0), Quaternion.identity);

                goP.name = string.Format("Tile:{0}_{1}, color: {2}", x, y, GetTileAt(x, y).Tilecolor);

                GoTiles.Add(goP);
                break;

            case Tile.TileColor.Green:

                var goG = Instantiate(TileGreen, new Vector3(x, y, 0), Quaternion.identity);

                goG.name = string.Format("Tile:{0}_{1}, color: {2}", x, y, GetTileAt(x, y).Tilecolor);

                GoTiles.Add(goG);
                break;

            case Tile.TileColor.Orange:

                var goO = Instantiate(TileOrange, new Vector3(x, y, 0), Quaternion.identity);

                goO.name = string.Format("Tile:{0}_{1}, color: {2}", x, y, GetTileAt(x, y).Tilecolor);

                GoTiles.Add(goO);
                break;

            case Tile.TileColor.Black:

                var goBl = Instantiate(TileBlack, new Vector3(x, y, 0), Quaternion.identity);

                goBl.name = string.Format("Tile:{0}_{1}, color: {2}", x, y, GetTileAt(x, y).Tilecolor);

                GoTiles.Add(goBl);
                break;

            default:
                break;
        }

        if (CheckLevelComplete()) {
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

        if (x < 0 || y < 0 || y >= sizey || x >= sizex) {

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

    public Tile.TileColor ColorCalutor (Tile.TileColor TC1, Tile.TileColor TC2) {

        if (TC1 == Tile.TileColor.White) {
            Debug.Log(TC1 + "," + TC2 + "=" + TC1);
            return TC2;
        }

        else if (TC2 == Tile.TileColor.White) {
            Debug.Log(TC1 + "," + TC2 + "=" + TC1);
            return TC1;
        }

        else if (TC1 == TC2) {
            return TC1;
        }

        else if (TC1 == Tile.TileColor.Blue && TC2 == Tile.TileColor.Red || TC2 == Tile.TileColor.Blue && TC1 == Tile.TileColor.Red) {
            Debug.Log(TC1 + "," + TC2 + "= Blue");
            return Tile.TileColor.Purple;
        }

        else if (TC1 == Tile.TileColor.Yellow && TC2 == Tile.TileColor.Red || TC2 == Tile.TileColor.Yellow && TC1 == Tile.TileColor.Red) {
            Debug.Log(TC1 + "," + TC2 + "= Orange");
            return Tile.TileColor.Orange;
        }

        else if (TC1 == Tile.TileColor.Yellow && TC2 == Tile.TileColor.Blue || TC2 == Tile.TileColor.Yellow && TC1 == Tile.TileColor.Blue) {
            Debug.Log(TC1 + "," + TC2 + "= Green");
            return Tile.TileColor.Green;
        }

        else if (TC1 == Tile.TileColor.Purple && TC2 == Tile.TileColor.Red || TC2 == Tile.TileColor.Purple && TC1 == Tile.TileColor.Red) {
            Debug.Log(TC1 + "," + TC2 + "= Red");
            return Tile.TileColor.Red;
        }

        else if (TC1 == Tile.TileColor.Purple && TC2 == Tile.TileColor.Blue || TC2 == Tile.TileColor.Purple && TC1 == Tile.TileColor.Red) {
            Debug.Log(TC1 + "," + TC2 + "= Blue");
            return Tile.TileColor.Blue;
        }

        else if (TC1 == Tile.TileColor.Yellow && TC2 == Tile.TileColor.Green || TC2 == Tile.TileColor.Yellow && TC1 == Tile.TileColor.Green) {
            Debug.Log(TC1 + "," + TC2 + "= Yellow");
            return Tile.TileColor.Yellow;
        }

        else if (TC1 == Tile.TileColor.Green && TC2 == Tile.TileColor.Blue || TC2 == Tile.TileColor.Green && TC1 == Tile.TileColor.Blue) {
            Debug.Log(TC1 + "," + TC2 + "= Blue");
            return Tile.TileColor.Blue;
        }

        else if (TC1 == Tile.TileColor.Yellow && TC2 == Tile.TileColor.Orange || TC2 == Tile.TileColor.Yellow && TC1 == Tile.TileColor.Orange) {
            Debug.Log(TC1 + "," + TC2 + "= Yellow");
            return Tile.TileColor.Yellow;
        }

        else if (TC1 == Tile.TileColor.Orange && TC2 == Tile.TileColor.Red || TC2 == Tile.TileColor.Orange && TC1 == Tile.TileColor.Red) {
            Debug.Log(TC1 + "," + TC2 + "= Red");
            return Tile.TileColor.Red;
        }

        else if (TC1 == Tile.TileColor.Purple && TC2 == Tile.TileColor.Orange || TC2 == Tile.TileColor.Purple && TC1 == Tile.TileColor.Orange) {
            Debug.Log(TC1 + "," + TC2 + "= Black");
            return Tile.TileColor.Black;
        }

        else if (TC1 == Tile.TileColor.Purple && TC2 == Tile.TileColor.Green || TC2 == Tile.TileColor.Purple && TC1 == Tile.TileColor.Green) {
            Debug.Log(TC1 + "," + TC2 + "= Black");
            return Tile.TileColor.Black;
        }

        else if (TC1 == Tile.TileColor.Orange && TC2 == Tile.TileColor.Green || TC2 == Tile.TileColor.Orange && TC1 == Tile.TileColor.Green) {
            Debug.Log(TC1 + "," + TC2 + "= Black");
            return Tile.TileColor.Black;
        }
        else {

            Debug.LogWarning(TC1 + "," + TC2 + "= No valid Color Combo");

            return Tile.TileColor.Black;
        }
    }
}
