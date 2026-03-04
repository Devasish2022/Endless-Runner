using UnityEngine;

public abstract class ItemPickups : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5.0f;
    const string PLAYER_TAG = "Player";

    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            Destroy(gameObject);
            PickUp();
        }
    }

    protected abstract void PickUp();
}
