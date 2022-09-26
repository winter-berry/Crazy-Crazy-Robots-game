
public class Portal : Door
{
    protected override void DetectedBehavior()
    {
        anim.SetBool("IsOpen", true);
        boxCollider.enabled = true;
    }
}
