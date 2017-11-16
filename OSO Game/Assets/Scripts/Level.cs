using System.Xml;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level {

    //Modifers
    //How far a tile will spread
    public int Spread = 0;
    
    //make tiles white
    public int Bombs = 0;

    public int TileBlue = 0;
    public int TileRed = 0;
    public int TileYellow = 0;

    public Tile[,] TileMapCruent;
    public Tile[,] TileMapEnd;

    public int LevelCount { get {

            return levels.Count;
        } }

    //B = blue R = Red Y = Yellow  P = Purple O = Orange G = Green  Bl = Black

    List<List<List<List<string>>>> levels = new List<List<List<List<string>>>> {

        //Level
        new List<List<List<string>>> {

            //Start Modifer and Tiles 0
            new List<List<string>> {

                new List<string>{"0","1"},      //Spread, Bombs      
                new List<string>{"2","2", "2"}  //Blue, Red, Yellow

            },

            //Level Start 1
            new List<List<string>>{

                new List<string> {"W","W","W","W","W"},
                new List<string> {"W","W","W","W","W"},
                new List<string> {"W","W","W","W","W"},
                new List<string> {"W","W","W","W","W"},
                new List<string> {"W","W","W","W","W"}

            },

            //Level Tile Modifers 2
            // Valid tokens No, BS, SS, MB, LB
            new List<List<string>> {

                new List<string>{ "No", "No", "No", "No", "No"},
                new List<string>{ "No", "No", "No", "No", "No"},
                new List<string>{ "No", "No", "BS", "No", "No"},
                new List<string>{ "No", "No", "No", "No", "No"},
                new List<string>{ "No", "No", "No", "No", "No" }

            },

            //Level End 3
            new List<List<string>> {

                new List<string>{"B","P","W","W","W"},
                new List<string>{"P","R","R","W","W"},
                new List<string>{"W","R","R","R","R"},
                new List<string>{"W","W","W","R","P"},
                new List<string>{"W","W","W","P","B"}

            }
        }
    };

    public Level (int LevelToLoad) {

        List<List<List<string>>> lev = levels[LevelToLoad];

        List<string> StartModifers = lev[0][0];

        //Set the modifer(s)
        Spread = int.Parse(StartModifers[0]);

        Bombs = int.Parse(StartModifers[1]);

        List<string> StartTiles = lev[0][1];

        //Set The Start Tiles
        TileBlue = int.Parse(StartTiles[0]);
        TileRed = int.Parse(StartTiles[1]);
        TileYellow = int.Parse(StartTiles[2]);


        //Read The tile color at start and store them
        List<List<string>> mapstart = lev[1];
        //Reveres the list so that it fits the stringmap
        mapstart.Reverse();

        Tile.TileColor[,] Tilecolormap = new Tile.TileColor[mapstart[0].Count, mapstart.Count];

        for (int x = 0; x < mapstart.Count; x++) {
            for (int y = 0; y < mapstart[0].Count; y++) { 

                switch (mapstart[x][y]) {
                    case "W":
                        Tilecolormap[x, y] = Tile.TileColor.White;
                        break;
                    case "B":
                        Tilecolormap[x, y] = Tile.TileColor.Blue;
                        break;
                    case "R":
                        Tilecolormap[x, y] = Tile.TileColor.Red;
                        break;
                    case "Y":
                        Tilecolormap[x, y] = Tile.TileColor.Yellow;
                        break;
                    case "P":
                        Tilecolormap[x, y] = Tile.TileColor.Purple;
                        break;
                    case "O":
                        Tilecolormap[x, y] = Tile.TileColor.Orange;
                        break;
                    case "G":
                        Tilecolormap[x, y] = Tile.TileColor.Purple;
                        break;
                    case "BL":
                        Tilecolormap[x, y] = Tile.TileColor.Black;
                        break;

                    default:
                        Debug.LogError("Invalid String token:" + mapstart[x][y]);
                        break;
                }
            }
        }

        //Read The Tile Modifers and store them
        List<List<string>> Modifers = lev[2];

        Tile.TileModifer[,] Tillemodifermap = new Tile.TileModifer[Modifers[0].Count, Modifers.Count];

        for (int x = 0; x < Modifers.Count; x++) {
            for (int y = 0; y < Modifers[0].Count; y++) {

                switch (Modifers[x][y]) {
                    case "No":
                        Tillemodifermap[x, y] = Tile.TileModifer.None;
                        break;
                    case "BS":
                        Tillemodifermap[x, y] = Tile.TileModifer.BiggerSpread;
                        break;
                    case "SS":
                        Tillemodifermap[x, y] = Tile.TileModifer.SmallSpread;
                        break;
                    case "MB":
                        Tillemodifermap[x, y] = Tile.TileModifer.MoreBombs;
                        break;
                    case "LB":
                        Tillemodifermap[x, y] = Tile.TileModifer.LessBomb;
                        break;
                    default:
                        Debug.LogError("Invalid String token:" + Modifers[x][y]);
                        break;
                }
            }
        }

        TileMapCruent = new Tile[Tilecolormap.GetLength(0), Tilecolormap.GetLength(1)];
        TileMapEnd = new Tile[Tilecolormap.GetLength(0), Tilecolormap.GetLength(1)];

        for (int x = 0; x < Tilecolormap.GetLength(0); x++) {
            for (int y = 0; y < Tilecolormap.GetLength(1); y++) {

                TileMapCruent[y, x] = new Tile(y, x, Tilecolormap[x, y], Tillemodifermap[x, y]);
            }
        }


        //Get and set the map end
        List<List<string>> mapend = lev[3];

        //Reveres the list so that it fits the stringmap
        mapend.Reverse();

        for (int x = 0; x < TileMapEnd.GetLength(0); x++) {
            for (int y = 0; y < TileMapEnd.GetLength(1); y++) {

                switch (mapend[y][x]) {

                    case "W":
                        TileMapEnd[x, y] = new Tile(x, y, Tile.TileColor.White);
                        break;
                    case "B":
                        TileMapEnd[x, y] = new Tile(x, y, Tile.TileColor.Blue);
                        break;
                    case "R":
                        TileMapEnd[x, y] = new Tile(x, y, Tile.TileColor.Red);
                        break;
                    case "Y":
                        TileMapEnd[x, y] = new Tile(x, y, Tile.TileColor.Yellow);
                        break;
                    case "P":
                        TileMapEnd[x, y] = new Tile(x, y, Tile.TileColor.Purple);
                        break;
                    case "O":
                        TileMapEnd[x, y] = new Tile(x, y, Tile.TileColor.Orange);
                        break;
                    case "G":
                        TileMapEnd[x, y] = new Tile(x, y, Tile.TileColor.Green);
                        break;
                    case "BL":
                        TileMapEnd[x, y] = new Tile(x, y, Tile.TileColor.Black);
                        break;


                    default:
                        Debug.LogError("Invalid String token:" + TileMapEnd[x, y]);
                        break;
                }
            }
        }
    }
}