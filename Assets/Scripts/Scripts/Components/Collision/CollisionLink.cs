using UnityEngine;


public class CollisionLink : MonoBehaviour
{

    [Header("Collision Link")]
    public CollisionComponent collisionComponent;


    //Collision Enter
    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision != null)
        {
            collisionComponent.lastCollision = _collision.gameObject;
        }
    }


    //Collision Exit
    private void OnCollisionExit2D(Collision2D _collision)
    {
        if (_collision != null && collisionComponent.lastCollision != null)
        {
            collisionComponent.lastCollision = null;
        }
    }



    //Trigger Enter
    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision != null)
        {
            collisionComponent.lastTrigger = _collision.gameObject;
        }
    }

    //Trigger Exit
    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision != null && collisionComponent.lastTrigger != null)
        {
            collisionComponent.lastTrigger = null;
        }
    }

}
