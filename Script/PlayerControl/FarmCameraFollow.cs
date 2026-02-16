using UnityEngine;

public class FarmCameraFollow : MonoBehaviour
{
    public Transform target;         
    public Vector3 offset = new Vector3(0f, 10f, -8f);
    public float followSpeed = 5f;
    public float rotateSpeed = 70f;   

    private float currentYaw = 0f;

    private void LateUpdate()
    {
        if (!target) return;

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            currentYaw += mouseX * rotateSpeed * Time.deltaTime;
        }

        float pitch = 35f;
        Quaternion rot = Quaternion.Euler(pitch, currentYaw, 0f);

        Vector3 desiredPos = target.position + rot * offset;

        transform.position = Vector3.Lerp(transform.position, desiredPos, followSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, followSpeed * Time.deltaTime);
    }
}
