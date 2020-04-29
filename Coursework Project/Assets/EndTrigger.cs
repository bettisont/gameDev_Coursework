
using UnityEngine;

public class EndTrigger : MonoBehaviour
{

    public GameManager gameManager;


    private void OnTriggerEnter(Collider other)
    {
        if (targetsShotUIController.totalTargets == targetsShotUIController.destroyedCount)
        {
            targetsShotUIController.destroyedCount = 0;
            gameManager.CompleteLevel();
        }
        Debug.Log("You havent killed all targets");
    }
}
