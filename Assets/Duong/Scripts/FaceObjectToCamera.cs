using UnityEngine;

public class FaceObjectToCamera : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);

    }
}
