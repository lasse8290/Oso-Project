using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManger : MonoBehaviour {

    enum TilePlaceMode {
        none,   //0
        white,  //1
        blue,   //2
        red,    //3
        yellow  //4
    }
    
    [SerializeField]
    TilePlaceMode tcp = TilePlaceMode.none;

    SpriteRenderer SR;

    public void ChageTilePlaceMode (int i) {

        tcp = (TilePlaceMode)i;
    }

    Camera c;

    // Use this for initialization
    void Start () {
         c = Camera.main;

        SR = this.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 Mp = c.ScreenToWorldPoint(Input.mousePosition);
        Mp.z = 0;

        if (Input.GetMouseButtonDown(0)) {

            Tile t = TileController.Instace.GetTileAt(Mp);

            if (t != null) {

                Debug.LogWarningFormat("{0},{1} Color: {2} Modifers {3}", t.X, t.Y, t.Tilecolor, t.Tilemodifer);

                switch (tcp) {
                    case TilePlaceMode.none:

                        break;

                    case TilePlaceMode.white:

                        TileController.Instace.PlaceTileAt(t, Tile.TileColor.White);
                        break;

                    case TilePlaceMode.blue:
                        TileController.Instace.PlaceTileAt(t, Tile.TileColor.Blue);
                        break;

                    case TilePlaceMode.red:
                        TileController.Instace.PlaceTileAt(t, Tile.TileColor.Red);
                        break;

                    case TilePlaceMode.yellow:
                        TileController.Instace.PlaceTileAt(t, Tile.TileColor.Yellow);
                        break;

                    default:
                        break;
                }
            }

            tcp = TilePlaceMode.none;
        }

        switch (tcp) {
            case TilePlaceMode.none:

                if (SR.enabled != false) {
                    SR.enabled = false;
                }

                break;
            case TilePlaceMode.white:

                if (SR.enabled != true || SR.color != Color.white) {

                    SR.color = Color.white;
                    SR.enabled = true;
                }
                break;
            case TilePlaceMode.blue:

                if (SR.enabled != true || SR.color != Color.blue) {
                    
                    SR.color = Color.blue;
                    SR.enabled = true;
                }

                break;
            case TilePlaceMode.red:

                if (SR.enabled != true || SR.color != Color.red) {

                    SR.color = Color.red;
                    SR.enabled = true;
                }

                break;

            case TilePlaceMode.yellow:

                if (SR.enabled != true || SR.color != Color.yellow) {

                    SR.color = Color.yellow;
                    SR.enabled = true;
                }

                break;
            default:
                break;
        }

        this.transform.position = Mp;
    }

    

}