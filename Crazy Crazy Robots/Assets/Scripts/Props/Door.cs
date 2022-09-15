using UnityEngine;

public class Door : MonoBehaviour
{
    /* Customizable */
    [SerializeField]
    protected bool readyToOpen = false;
    [SerializeField]
    protected GameObject[] objectsToDetect;
    [SerializeField]
    protected float detectionRange = 1;

    /* Components */
    protected Animator anim;
    protected BoxCollider2D boxCollider;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        SetDoorStatus();
    }

    protected void SetDoorStatus()
    {
        if (readyToOpen)
        {
            anim.SetBool("DoorActive", true);
            DoorBehavior();
        }

        else anim.SetBool("DoorActive", false);
    }

    protected bool Detection()
    {
        bool inRange = false;

        foreach (GameObject obj in objectsToDetect)
        {
            /* Checks if a delcared object is in range */
            Vector3 objPos = GameObject.Find(obj.name).transform.position;
            inRange = (objPos - this.transform.position).sqrMagnitude < detectionRange * detectionRange;
        }

        return inRange;
    }

    /* Override */
    protected virtual void DetectedBehavior()
    {
        anim.SetBool("DoorOpen", true);

        /* Disable collider so that object can walk through*/
        boxCollider.enabled = false;
    }

    /* Override */
    protected virtual void NotDetectedBehavior()
    {
        anim.SetBool("DoorOpen", false);

        /* Disable collider so that object can walk through*/
        boxCollider.enabled = true;
    }

    protected void DoorBehavior()
    {
        if (Detection())
        {
            DetectedBehavior();
        }

        else NotDetectedBehavior();
    }
}
