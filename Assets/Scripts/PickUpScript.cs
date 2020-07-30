using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<PlayerHealth>())
        {
            gameObject.BroadcastMessage("PickUpStuff", other);
            Destroy(gameObject);
        }
    }
}
