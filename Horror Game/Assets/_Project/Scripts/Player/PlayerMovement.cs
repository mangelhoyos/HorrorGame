using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Title("Player data")]
    [SerializeField, AssetSelector(Paths = "Assets/_Project/Data/Player")] PlayerConfigSO playerSO;

    [Title("Player configuration")]
    [SerializeField, SceneObjectsOnly] private CharacterController controller;
    [SerializeField, SceneObjectsOnly] private Transform groundCheckTransform;

    [Title("Bobbing setup")]
    [SerializeField, AssetSelector(Paths = "Assets/_Project/Data/Player")] PlayerBobbing_SO bobbingSO;
    [SerializeField, SceneObjectsOnly] private Transform cameraOffset;

    Vector3 initialOffsetPosition;
    Vector3 actualVelocity;
    bool isGrounded;

    private void Start()
    {
        initialOffsetPosition = cameraOffset.localPosition;
    }

    void Update()
    {
        TryMovement();
        ApplyFreeFall();
    }

    private void ApplyFreeFall()
    {
        isGrounded = Physics.CheckSphere(groundCheckTransform.position, playerSO.groundCheckDistance, playerSO.groundMask);

        if (isGrounded && actualVelocity.y < 0)
        {
            actualVelocity.y = -2f;
        }

        actualVelocity.y += playerSO.gravity * Time.deltaTime;

        controller.Move(actualVelocity * Time.deltaTime);
    }

    private void TryMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector2 axis = new Vector2(x, z);
        axis.Normalize();

        if (axis.magnitude > 0)
        {
            Vector3 moveDir = transform.right * axis.x + transform.forward * axis.y;

            controller.Move(moveDir * playerSO.playerSpeed * Time.deltaTime);
            ApplyBobbing();
        }
        else
        {
            ResetBobbing();
        }
    }

    private void ApplyBobbing()
    {
        if (!bobbingSO.bobbingIsEnabled)
            return;

        //Get footstep position
        Vector3 footStepPos = Vector3.zero;
        footStepPos.y += Mathf.Sin(Time.time * bobbingSO.bobbingFrequency) * bobbingSO.bobbingAmplitude;
        footStepPos.x += Mathf.Cos(Time.time * bobbingSO.bobbingFrequency / 2) * bobbingSO.bobbingAmplitude * 2;

        cameraOffset.localPosition += footStepPos;
    }

    private void ResetBobbing()
    {
        if (!bobbingSO.bobbingIsEnabled)
            return;

        cameraOffset.localPosition = Vector3.Lerp(cameraOffset.localPosition, initialOffsetPosition, 1 * Time.deltaTime);
    }

}
