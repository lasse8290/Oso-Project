using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManger : MonoBehaviour {

    int lev;


    public void Loadlevel (int lev) {

        DontDestroyOnLoad(this);

        this.lev = lev;

        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

        SceneManager.UnloadSceneAsync(0);

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;

    }

    private void SceneManager_sceneLoaded (Scene S, LoadSceneMode SM) {

        if (S.name == "Level 1") {

            TileController GO = GameObject.Find("GameManger").GetComponent<TileController>();

            GO.DebugMode = false;

            GO.GenLevel(lev);
        }

        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;

        Destroy(this.gameObject);
    }

    public void ExitApp () {
        Application.Quit();
    }
}
