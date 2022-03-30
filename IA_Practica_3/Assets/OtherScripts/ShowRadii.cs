
using UnityEngine;

public class ShowRadii : MonoBehaviour {

    public float big;
    public float small;
	
	// Update is called once per frame
	void Update () {
        DebugExtension.DebugCircle(gameObject.transform.position, new Vector3(0, 0, 1), Color.blue, big);
        DebugExtension.DebugCircle(gameObject.transform.position, new Vector3(0, 0, 1), Color.red, small);
    }
}
