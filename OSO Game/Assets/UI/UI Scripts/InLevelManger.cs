using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class InLevelManger : MonoBehaviour {

    bool MainMeunSelectOption = false;

    [SerializeField]
    GameObject SpeadCounter;

    [SerializeField]
    Sprite Sp;

    Texture2D Tx2d;

    [SerializeField]
    private Text SpeadCount = null;

    [SerializeField]
    private Text ChangeToBlue = null;

    [SerializeField]
    private Text ChangeToRed = null;

    [SerializeField]
    private Text ChangeToYellow = null;

    [SerializeField]
    private Text ChangeToBomb = null;

    private void Awake () {

        Tx2d = Sp.texture;

        SpeadCounter.GetComponent<RawImage>().texture = Tx2d;
    }

    private void OnGUI () {

        if (TileController.Instace.Level != null) {

            SpeadCount.text = TileController.Instace.Level.Spread.ToString();
            ChangeToBlue.text = TileController.Instace.Level.TileBlue.ToString();
            ChangeToRed.text = TileController.Instace.Level.TileRed.ToString();
            ChangeToYellow.text = TileController.Instace.Level.TileYellow.ToString();
            ChangeToBomb.text = TileController.Instace.Level.Bombs.ToString();
        }
    }

    public void ChangeMainMeunSelectOption(bool b) {

        MainMeunSelectOption = b;

    }

    public void ButtonPress (bool buttonPress) {

        if (MainMeunSelectOption) {

            if (buttonPress) {
                Debug.Log("MenuMainMenuLevelYes");

                TileController.Instace.DestroyLevel();

                SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);

                SceneManager.UnloadSceneAsync(1);

                Debug.Log(1 + "," + SceneManager.sceneCountInBuildSettings);
            }

            if (!buttonPress) {
                Debug.Log("MenuMainMenuLevelNo");
            }
        }

        else if (!MainMeunSelectOption) {

            if (buttonPress) {
                Debug.Log("MenuRestLevelYes");

                int i = TileController.Instace.Level.CruentLevelIndex;

                TileController.Instace.DestroyLevel();

                TileController.Instace.GenLevel(i);
            }

            if (!buttonPress) {
                Debug.Log("MenuRestLevelNo");
                buttonPress = false;
            }
        }
    }     
}