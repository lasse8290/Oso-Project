using System.Xml;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level {

    public int CruentLevelIndex;

    bool isTutorial = false;

    public bool IsTutorial { get { return isTutorial; } }

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

    public Level (int LevelToLoad) {

        if (LevelToLoad == 0) {
            isTutorial = true;
        }

        CruentLevelIndex = LevelToLoad;

        XmlDocument Doc = new XmlDocument();

        Doc.Load(Application.dataPath + "\\mics files\\LevelFile.xml");

        XmlNode level = GetLevelformFile(Doc, LevelToLoad);

        foreach (XmlNode node in level.ChildNodes) {

            //asing modifer from xml doc
            if (node.Name == "Modifers") {
                foreach (XmlElement elem in node.ChildNodes) {

                    if (elem.Name == "Spread") {
                        Spread = int.Parse(elem.InnerText);
                    }

                    if (elem.Name == "Bombs") {
                        Bombs = int.Parse(elem.InnerText);
                    }

                    //set the tiles the play start with
                    if (elem.Name == "StartTiles") {

                        int i = 0;
                        foreach (XmlElement starttiles in elem) {

                            switch (i) {
                                case 0:

                                    TileBlue = int.Parse(starttiles.InnerText);
                                    i++;
                                    break;
                                case 1:

                                    TileRed = int.Parse(starttiles.InnerText);
                                    i++;
                                    break;
                                case 2:

                                    TileYellow = int.Parse(starttiles.InnerText);
                                    i++;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }

            if (node.Name == "TileMap") {

                //Load the Tilemap from file

                List<Tile.TileColor> mapStartColor = new List<Tile.TileColor>();
                List<Tile.TileModifer> mapModifers = new List<Tile.TileModifer>();
                List<Tile.TileColor> mapEndColor = new List<Tile.TileColor>();

                int x;
                int y;

                //asing the x & y
                x = node.ChildNodes[1].ChildNodes[0].InnerText.Split(',').Length - 1;
                y = node.ChildNodes[1].ChildNodes.Count;

                Debug.LogFormat("Level:{0} Size: {1},{2}", level.Attributes[0].Value, x, y);

                //TileColors and modifers for the tilemap
                foreach (XmlNode item in node) {

                    if (item.Name == "TileMapStart") {

                        if (item.ChildNodes.Count != y || item.ChildNodes[0].InnerText.Split(',').Length - 1 != x) {

                            Debug.LogErrorFormat("SIZE OF TileMapStart:{0},{1} NOT THE SAME: {2},{3}"
                                                , item.ChildNodes[0].InnerText.Split(',').Length - 1, item.ChildNodes.Count, x, y);
                        }

                        var lst = item.InnerText.Split(',').ToList();

                        for (int n = 0; n < lst.Count; n++) {

                            switch (lst[n].Trim()) {
                                case "W":
                                    mapStartColor.Add(Tile.TileColor.White);
                                    break;
                                case "B":
                                    mapStartColor.Add(Tile.TileColor.Blue);
                                    break;
                                case "R":
                                    mapStartColor.Add(Tile.TileColor.Red);
                                    break;
                                case "Y":
                                    mapStartColor.Add(Tile.TileColor.Yellow);
                                    break;
                                case "P":
                                    mapStartColor.Add(Tile.TileColor.Purple);
                                    break;
                                case "O":
                                    mapStartColor.Add(Tile.TileColor.Orange);
                                    break;
                                case "G":
                                    mapStartColor.Add(Tile.TileColor.Green);
                                    break;
                                case "K":
                                    mapStartColor.Add(Tile.TileColor.Black);
                                    break;
                            }
                        }
                    }

                    if (item.Name == "TileMapModifers") {

                        if (item.ChildNodes.Count != y || item.ChildNodes[0].InnerText.Split(',').Length - 1 != x) {

                            Debug.LogErrorFormat("SIZE OF TileMapModifer:{0},{1} NOT THE SAME: {2},{3}"
                                                , item.ChildNodes[0].InnerText.Split(',').Length - 1, item.ChildNodes.Count, x, y);
                        }

                        var lst = item.InnerText.Split(',').ToList();

                        for (int n = 0; n < lst.Count; n++) {

                            switch (lst[n].Trim()) {
                                case "No":
                                    mapModifers.Add(Tile.TileModifer.None);
                                    break;
                                case "BS":
                                    mapModifers.Add(Tile.TileModifer.BiggerSpread);
                                    break;
                                case "SS":
                                    mapModifers.Add(Tile.TileModifer.SmallSpread);
                                    break;
                                case "MB":
                                    mapModifers.Add(Tile.TileModifer.MoreBombs);
                                    break;
                                case "LB":
                                    mapModifers.Add(Tile.TileModifer.LessBomb);
                                    break;
                                default:
                                    Debug.LogError("Invalid String token:" + lst[n]);
                                    break;
                            }
                        }
                    }

                    if (item.Name == "TileMapEnd") {

                        if (item.ChildNodes.Count != y || item.ChildNodes[0].InnerText.Split(',').Length - 1 != x) {

                            Debug.LogErrorFormat("SIZE OF TileMapEnd:{0},{1} NOT THE SAME: {2},{3}"
                                                , item.ChildNodes[0].InnerText.Split(',').Length - 1, item.ChildNodes.Count, x, y);
                        }

                        var lst = item.InnerText.Split(',').ToList();

                        for (int n = 0; n < lst.Count; n++) {

                            switch (lst[n].Trim()) {
                                case "W":
                                    mapEndColor.Add(Tile.TileColor.White);
                                    break;
                                case "B":
                                    mapEndColor.Add(Tile.TileColor.Blue);
                                    break;
                                case "R":
                                    mapEndColor.Add(Tile.TileColor.Red);
                                    break;
                                case "Y":
                                    mapEndColor.Add(Tile.TileColor.Yellow);
                                    break;
                                case "P":
                                    mapEndColor.Add(Tile.TileColor.Purple);
                                    break;
                                case "O":
                                    mapEndColor.Add(Tile.TileColor.Orange);
                                    break;
                                case "G":
                                    mapEndColor.Add(Tile.TileColor.Green);
                                    break;
                                case "K":
                                    mapEndColor.Add(Tile.TileColor.Black);
                                    break;
                            }
                        }
                    }
                }

                TileMapCruent = new Tile[x, y];
                TileMapEnd = new Tile[x, y];

                int xx = 0;
                int yy = y - 1;

                //loop to creating the start tiles
                for (int n = 0; n < x * y; n++) {

                    if (xx == x) {
                        xx = 0;
                        yy--;
                    }

                    TileMapCruent[xx, yy] = new Tile(xx, yy, mapStartColor[n], mapModifers[n]);

                    xx++;
                }

                //loop to create the end tile array
                xx = 0;
                yy = y - 1;

                for (int n = 0; n < x * y; n++) {

                    if (xx == x) {
                        xx = 0;
                        yy--;
                    }

                    TileMapEnd[xx, yy] = new Tile(xx, yy, mapEndColor[n]);

                    xx++;
                }
            }
        }
    }

    XmlNode GetLevelformFile (XmlDocument Doc, int index) {

        foreach (XmlNode XN in Doc) {
                foreach (XmlNode node in XN) {

                if (node.Attributes != null) {

                    foreach (XmlAttribute att in node.Attributes) {

                        if (int.Parse(att.Value) == index) {

                            return node;
                        }
                    }
                }
            }
        }

        Debug.LogError("Level:" + index + ", NOT FOUND");

        return null;
    }
}