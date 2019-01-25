using UnityEngine;


public class CollisionLink : MonoBehaviour
{

    [Header("Collision Link")]
    public CollisionComponent collisionComponent;


    //Collision Enter
    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision != null)
        {
            collisionComponent.lastCollision = _collision.gameObject;
        }
    }


    //Collision Exit
    private void OnCollisionExit(Collision _collision)
    {
        if (_collision != null && collisionComponent.lastCollision != null)
        {
            collisionComponent.lastCollision = null;
        }
    }



    //Trigger Enter
    private void OnTriggerEnter(Collider _collision)
    {
        if (_collision != null)
        {
            collisionComponent.lastTrigger = _collision.gameObject;
        }
    }

    //Trigger Exit
    private void OnTriggerExit(Collider _collision)
    {
        if (_collision != null && collisionComponent.lastTrigger != null)
        {
            collisionComponent.lastTrigger = null;
        }
    }

}
