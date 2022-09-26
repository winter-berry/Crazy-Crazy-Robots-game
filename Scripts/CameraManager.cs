using UnityEngine;

public class CameraManager : MonoBehaviour
{
    /* Customizable */
    [SerializeField]
    private Transform lookAt;
    [SerializeField]
    private float boundX = 0.15f;
    [SerializeField]
    private float boundY = 0.05f;

    private void LateUpdate()
    {
        FollowObject();
    }

    private void FollowObject()
    {
        Vector3 delta = Vector3.zero;
        float deltaX = lookAt.position.x - transform.position.x;

        /* Object out of bounds */
        if (deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x < lookAt.position.x)
            {
                /* Move camera left */
                delta.x = deltaX - boundX;
            }

            else
            {
                /* Move camera right */
                delta.x = deltaX + boundX;
            }
        }

        float deltaY = lookAt.position.y - transform.position.y;

        /* Object out of bounds */
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                /* Move camera down */
                delta.y = deltaY - boundY;
            }

            else
            {
                /* Move camera up */
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
