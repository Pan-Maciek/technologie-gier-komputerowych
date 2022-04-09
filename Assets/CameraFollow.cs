using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 

    // Update is called once per frame
    void Update() {
        transform.position = target.position;
    }
}
