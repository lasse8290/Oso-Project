using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManger : MonoBehaviour {

    GameObject Cam;

    void Start () {

        Cam = GameObject.FindGameObjectWithTag("MainCamera");

        Cam.transform.position = new Vector3((TileController.Instace.SizeX / 2), (TileController.Instace.SizeY / 2), -10f);

    }
}