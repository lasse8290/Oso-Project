using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManger : MonoBehaviour {

    public GameObject Canvans;
    public GameObject LevelButton;
    public GameObject NextLevelSC;

    int lev;

    public void Loadlevel (int lev) {

        DontDestroyOnLoad(this);

        this.lev = lev;

        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

        SceneManager.UnloadSceneAsync(0);

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;

    }

    private void Start () {

        XmlDocument doc = new XmlDocument();

        doc.Load(Application.dataPath + "\\mics files\\LevelFile.xml");
        
        foreach (XmlNode xn in doc) {

            if (xn.Name == "Levels") {

                for (int i = 0; i < xn.ChildNodes.Count; i++) {

                    var gobutton = Instantiate(LevelButton, new Vector3(0,0,0), Quaternion.identity);

                    gobutton.transform.SetParent(Canvans.transform);
                }
            }
        }
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
