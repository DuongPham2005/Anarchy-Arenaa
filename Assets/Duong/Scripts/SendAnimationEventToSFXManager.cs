using UnityEngine;

public class SendAnimationEventToSFXManager : MonoBehaviour
{
    public PlayerPhotonSoundManager playerPhotonSoundManager;

    public void TriggerFootstepSFX()
    {
        playerPhotonSoundManager.PlayFootstepSFX();
    }
}
