using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManger : MonoBehaviour {

    public void Loadlevel(int lev) {

        DontDestroyOnLoad(this);

        SceneManager.UnloadSceneAsync(0);

        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

        SceneManager.sceneLoaded += (s,sm)=> {

            TileController TCgo = GameObject.Find("GameManger").GetComponent<TileController>();

            TCgo.DebugMode = false;

            TCgo.GenLevel(lev);

            Destroy(this.gameObject);

        };
    }

    public void ExitApp () {
        Application.Quit();
    }
}
