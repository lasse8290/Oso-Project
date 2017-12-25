using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManger : MonoBehaviour {

    Camera Cam;

    void Start () {

        float x = TileController.Instace.SizeX / 2;
        float y = TileController.Instace.SizeY / 2;

        if (TileController.Instace.SizeX % 2 > 0) {
            x += 0.5f;
        }

        if (TileController.Instace.SizeY % 2 > 0) {
            y += 0.5f;
        }

        Cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        if (TileController.Instace.SizeX >= 8 || TileController.Instace.SizeY >= 8) {

            Cam.orthographicSize = 8;
        }

        Cam.transform.position = new Vector3( x, y, -10f);

    }
}