using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManger : MonoBehaviour {

    enum TilePlaceMode {
        none,   //0
        Bomb,   //1
        blue,   //2
        red,    //3
        yellow  //4
    }
    
    [SerializeField]
    TilePlaceMode tcp = TilePlaceMode.none;

    SpriteRenderer SR;

    [SerializeField]
    Sprite BombSprite;
    [SerializeField]
    Sprite TileSprite;

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

                switch (tcp) {
                    case TilePlaceMode.none:
                        break;

                    case TilePlaceMode.Bomb:

                        if (TileController.Instace.Level.Bombs > 0) {

                            TileController.Instace.PlaceBombAt(t);

                            TileController.Instace.Level.Bombs--;

                        }

                        break;

                    case TilePlaceMode.blue:
                        if (TileController.Instace.Level.TileBlue > 0) {

                            TileController.Instace.PlaceTileAt(t, Tile.TileColor.Blue);

                            TileController.Instace.Level.TileBlue--;
                        }
                        break;

                    case TilePlaceMode.red:
                        if (TileController.Instace.Level.TileRed > 0) {

                            TileController.Instace.PlaceTileAt(t, Tile.TileColor.Red);

                            TileController.Instace.Level.TileRed--;
                        }
                        break;

                    case TilePlaceMode.yellow:
                        if (TileController.Instace.Level.TileYellow > 0) {

                            TileController.Instace.PlaceTileAt(t, Tile.TileColor.Yellow);

                            TileController.Instace.Level.TileYellow--;
                        }
                            break;

                    default:
                        break;
                }
            }

            tcp = TilePlaceMode.none;
        }

        Mp.z = -1f;

        switch (tcp) {
            case TilePlaceMode.none:

                if (SR.enabled != false) {
                    SR.enabled = false;
                }

                break;
            case TilePlaceMode.Bomb:

                if (SR.enabled != true || SR.sprite != BombSprite) {

                    transform.localScale = new Vector3(4,4,4);

                    SR.enabled = true;
                    SR.sprite = BombSprite;
                }
                break;
            case TilePlaceMode.blue:

                if (SR.enabled != true || SR.color != Color.blue) {

                    transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    SR.sprite = TileSprite;
                    SR.color = Color.blue;
                    SR.enabled = true;
                }

                break;
            case TilePlaceMode.red:

                if (SR.enabled != true || SR.color != Color.red) {

                    transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    SR.sprite = TileSprite;
                    SR.color = Color.red;
                    SR.enabled = true;
                }

                break;

            case TilePlaceMode.yellow:

                if (SR.enabled != true || SR.color != Color.yellow) {

                    transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    SR.sprite = TileSprite;
                    SR.color = Color.yellow;
                    SR.enabled = true;
                }

                break;
            default:
                break;
        }

        transform.position = Mp;
    }

    

}