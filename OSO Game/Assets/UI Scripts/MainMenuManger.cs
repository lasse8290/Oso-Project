using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManger : MonoBehaviour {

    public void Loadlevel(int lev) {

        DontDestroyOnLoad(this);

        SceneManager.UnloadSceneAsync(0);

        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

        Debug.Log(1 + "," + SceneManager.sceneCountInBuildSettings);

        SceneManager.sceneLoaded += (s,sm)=> {

            GameObject.Find("GameManger").GetComponent<TileController>().GenLevel(lev);

            Destroy(this.gameObject);
        };

    }

    public void ExitApp () {
        Application.Quit();
    }
}
