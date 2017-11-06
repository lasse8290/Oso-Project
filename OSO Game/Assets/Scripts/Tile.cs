using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

    int x;
    public int X { get { return x; } }

    int y;
    public int Y { get { return y; } }

    public enum TileColor {
        white,
        blue,
        red
    }

    TileColor Tc = TileColor.white;
    TileColor Ptc;

    public TileColor Tilecolor { get { return Tc; } }
    public TileColor PreTilecolor { get { return Ptc; } }

    public Tile (int x, int y, TileColor TC) {

        this.x = x;
        this.y = y;

        Tc = TC;
        Ptc = TC;
    }

    public void ChangeTileColor (TileColor TC, Action<Tile> Callback) {

        Ptc = Tc;
        Tc = TC;

        Callback(this);
    }
}