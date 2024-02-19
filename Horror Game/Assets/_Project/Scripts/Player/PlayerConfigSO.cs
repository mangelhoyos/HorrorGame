using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "PlayerConfigFile_SO", menuName = "Player/Player Config")]
public class PlayerConfigSO : ScriptableObject
{

    [Title("Player Movement configuration parameters")]
    public float playerSpeed;
    public float gravity; //calculated for free fall speed
    public float groundCheckDistance;
    public LayerMask groundMask;

    [Title("Camera config parameters")]
    public float cameraSensitivity;
    [PropertyRange(60,80)] public float fovAmount;
    [HideInInspector] public bool isCursorLocked;

}
