using UnityEngine;

public class Chest_Tutorial : Chest
{
    /* Doors */
    [SerializeField]
    private Door door1, door2, door3;

    /* Lights */
    [SerializeField]
    private SpriteRenderer lights1, lights2;
    [SerializeField]
    private Sprite lightsOnSprite;

    /* Screens */
    [SerializeField]
    private Animator screenAnim;
    [SerializeField]
    private SpriteRenderer blueScreen;
    [SerializeField]
    private Sprite emptyScreenSprite;

    protected override void OtherBehavior()
    {
        DoorBehavior();
        LightsBehavior();
        ScreenBehavior();
    }  

    private void DoorBehavior()
    {
        door1.SetDoorStatus(true);
        door2.SetDoorStatus(true);
        door3.SetDoorStatus(true);
    }

    private void LightsBehavior()
    {
        lights1.sprite = lightsOnSprite;
        lights2.sprite = lightsOnSprite;
    }
    private void ScreenBehavior()
    {
        screenAnim.enabled = false;
        blueScreen.sprite = emptyScreenSprite;
    }
}
