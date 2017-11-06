using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels {

    Tile[,] Level01Start = new Tile[5, 5];
    Tile[,] Level01End   = new Tile[5, 5];

    Tile[,] Level02Start = new Tile[5, 5];
    Tile[,] Level02End   = new Tile[5, 5];

    public Levels () { 

        List<Tile[,]> ls = new List<Tile[,]> {
            Level01Start,
            Level01End,

            Level02Start,
            Level02End
        };

        for (int i = 0; i < ls.Count; i++) {
            for (int x = 0; x < ls[i].GetLength(0); x++) {
                for (int y = 0; y < ls[i].GetLength(1); y++) {

                    ls[i][x, y] = new Tile(x, y, Tile.TileColor.white);
                }
            }
        }            
    }

    public void GetLevel (int LevelToLoad, out Tile[,] start, out Tile[,] end) {

        switch (LevelToLoad) {

            case 0:

            start = Level01Start;
            end = Level01End;

            return;

            default:
            Debug.LogErrorFormat("Level:{0} Not Found", LevelToLoad);

            start = null;
            end = null;

            return;
        }

    }
}