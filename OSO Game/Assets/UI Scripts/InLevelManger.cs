using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InLevelManger : MonoBehaviour {

    [SerializeField]
    private Text ChangeToBlue = null;

    [SerializeField]
    private Text ChangeToRed = null;

    [SerializeField]
    private Text ChangeToYellow = null;

    [SerializeField]
    private Text ChangeToBomb = null;

    private void OnGUI () {
        ChangeToBlue.text = TileController.Instace.Level.TileBlue.ToString();
        ChangeToRed.text = TileController.Instace.Level.TileRed.ToString();
        ChangeToYellow.text = TileController.Instace.Level.TileYellow.ToString();
        ChangeToBomb.text = TileController.Instace.Level.Bombs.ToString();
       
    }
}
