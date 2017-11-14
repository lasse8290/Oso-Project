using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

    int x;
    public int X { get { return x; } }

    int y;
    public int Y { get { return y; } }

    public enum TileModifer {
        None,
        BiggerSpread,
        SmallSpread   
    }

    public TileModifer tm = TileModifer.None;

    public TileModifer Tilemodifer { get { return tm; } }

    public bool HasModifer {
        get {
            if (tm != TileModifer.None) {
                return true;
            }
            else {
                return false;
            }
        }
    }

    public enum TileColor {
        White,

        Blue,
        Red,
        Yellow,

        Purple,
        Orange,
        Green,

        Black
    }

    TileColor Tc = TileColor.White;
    TileColor Ptc;

    public TileColor Tilecolor { get { return Tc; } }
    public TileColor PreTilecolor { get { return Ptc; } }

    public Tile (int x, int y, TileColor TC, TileModifer TM = TileModifer.None) {

        this.x = x;
        this.y = y;

        Tc = TC;
        Ptc = TC;

        tm = TM;
    }

    public void ChangeTileModifer (TileModifer TM) {

        tm = TM;

        TileController.Instace.UpdateTileAt(x, y);
    }

    public void ChangeTileColor (TileColor TC) {

        Ptc = Tc;
        Tc = TC;

        TileController.Instace.UpdateTileAt(x,y);
    }
}