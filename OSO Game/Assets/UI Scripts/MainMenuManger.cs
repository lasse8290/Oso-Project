using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManger : MonoBehaviour {

    public void Loadlevel(int lev) {

        DontDestroyOnLoad(this);

        SceneManager.UnloadSceneAsync(0);

        SceneManager.LoadSceneAsync(lev, LoadSceneMode.Single);

        //GameObject.Find("GameManger").GetComponent<TileController>().GenLevel(lev);
    }

    public void ExitApp () {
        Application.Quit();
    }
}
