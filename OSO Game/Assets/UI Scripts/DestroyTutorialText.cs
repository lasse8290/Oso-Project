using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTutorialText : MonoBehaviour {

    private void OnMouseDown () {

        Destroy(this.transform.parent.gameObject);
    }
}
