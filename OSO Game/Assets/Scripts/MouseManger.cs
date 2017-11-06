using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManger : MonoBehaviour {

    enum TilePlaceMode {
        none,   //0
        white,  //1
        blue,   //2
        red     //3
    }
    
    [SerializeField]
    TilePlaceMode tcp = TilePlaceMode.none;

    public void ChageTilePlaceMode (int i) {

        tcp = (TilePlaceMode)i;
    }

    Camera c;

    // Use this for initialization
    void Start () {
         c = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0)) {

            Vector3 m = c.ScreenToWorldPoint(Input.mousePosition);

            m.z = 0;

            Tile t = TileController.Instace.GetTileAt(m);

            if (t != null) {
                Debug.LogWarning(t.X + "," + t.Y);

                switch (tcp) {
                    case TilePlaceMode.none:

                        break;

                    case TilePlaceMode.white:

                        t.ChangeTileColor(Tile.TileColor.white, TileController.Instace.UpdateTileAt);
                        break;

                    case TilePlaceMode.blue:
                        t.ChangeTileColor(Tile.TileColor.blue, TileController.Instace.UpdateTileAt);
                        break;

                    case TilePlaceMode.red:
                        t.ChangeTileColor(Tile.TileColor.red, TileController.Instace.UpdateTileAt);
                        break;

                    default:
                        break;
                }

               
            }

            tcp = TilePlaceMode.none;
        }
	}
}