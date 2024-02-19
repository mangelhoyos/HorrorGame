using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "PlayerBobbing_SO", menuName = "Player/Player Bobbing")]
public class PlayerBobbing_SO : ScriptableObject
{
    [Title("Bobbing config parameters")]
    public bool bobbingIsEnabled = true;
    [PropertyRange(0f, 40f)] public float bobbingFrequency;
    [PropertyRange(0f, 0.05f)] public float bobbingAmplitude;
}
