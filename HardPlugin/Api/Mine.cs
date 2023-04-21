using UnityEngine;

namespace HardPlugin.Api
{
    public class Mine : MonoBehaviour
    {
       private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                HardPlugin.MineController.DeletePrimitive(gameObject);
            }
        }
    }
}