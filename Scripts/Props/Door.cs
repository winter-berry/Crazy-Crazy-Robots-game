using UnityEngine;

public class Door : MonoBehaviour
{
    /* Customizable */
    [SerializeField]
    public bool readyToOpen = false;
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
        CheckDoorStatus();
    }

    public void SetDoorStatus(bool status)
    {
        readyToOpen = status;
    }

    protected void CheckDoorStatus()
    {
        if (readyToOpen)
        {
            anim.SetBool("IsActive", true);
            DoorBehavior();
        }

        else anim.SetBool("IsActive", false);
    }

    protected bool Detection()
    {
        bool inRange = false;

        foreach (GameObject obj in objectsToDetect)
        {
            /* Object in range */
            Vector3 objPos = GameObject.Find(obj.name).transform.position;
            inRange = (objPos - this.transform.position).sqrMagnitude < detectionRange * detectionRange;
        }

        return inRange;
    }

    /* Override */
    protected virtual void DetectedBehavior()
    {
        anim.SetBool("IsOpen", true);

        /* Object can go through */
        boxCollider.enabled = false;
    }

    /* Override */
    protected virtual void NotDetectedBehavior()
    {
        anim.SetBool("IsOpen", false);

        /* Object cannot go through */
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
