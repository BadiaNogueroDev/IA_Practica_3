
using UnityEngine;

public class ControlScript : MonoBehaviour
{
    private Camera cam;
    private GameObject customerPrefab;
  

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        customerPrefab = Resources.Load<GameObject>("CUSTOMER");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey("c"))
        {
            var position = cam.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            GameObject customer = GameObject.Instantiate(customerPrefab);
            customer.transform.position = position;
        }
    }
}
