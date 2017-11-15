using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InLevelManger : MonoBehaviour {

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
}
