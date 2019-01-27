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
            collisionComponent.lastCollisionObject = _collision.gameObject;
            collisionComponent.collision = _collision;
        }
    }


    //Collision Exit
    private void OnCollisionExit2D(Collision2D _collision)
    {
        if (_collision != null)
        {
            collisionComponent.lastCollisionObject = null;
            collisionComponent.backupCollisionObject = null;
            collisionComponent.collision = null;
        }
    }



    //Trigger Enter
    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision != null)
        {
            collisionComponent.lastTriggerObject = _collision.gameObject;
            collisionComponent.trigger = _collision;
        }
    }

    //Trigger Exit
    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision != null)
        {
            collisionComponent.lastTriggerObject = null;
            collisionComponent.backupTriggerObject = null;
            collisionComponent.trigger = null;
        }
    }

}
