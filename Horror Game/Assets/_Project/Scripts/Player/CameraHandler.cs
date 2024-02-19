using Sirenix.OdinInspector;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [Title("Player data")]
    [SerializeField, AssetSelector(Paths = "Assets/_Project/Data/Player")] PlayerConfigSO playerData;

    [Title("Camera configuration")]
    [SerializeField, SceneObjectsOnly] Transform playerBodyTransform;

    float xRotation = 0f;


    void Start()
    {
        SetFOV(playerData.fovAmount);
        SetCursorState(true);
    }

    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * playerData.cameraSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * playerData.cameraSensitivity * Time.deltaTime;

        if (mouseX != 0f || mouseY != 0f)
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBodyTransform.Rotate(Vector3.up, mouseX);
        } 
    }

    public void SetCursorState(bool isCursorLocked)
    {
        Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = isCursorLocked;
        playerData.isCursorLocked = isCursorLocked;
    }

    public void SetFOV(float fovValue)
    {
        Camera.main.fieldOfView = fovValue;
    }

    public void Choripan()
    {
        throw new System.NotImplementedException();
    }
}
