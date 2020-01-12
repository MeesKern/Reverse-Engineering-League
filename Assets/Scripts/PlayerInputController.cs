using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public PlayerInput Current;
    public bool inputEnabled = true;


    // Start is called before the first frame update
    void Start()
    {
        Current = new PlayerInput();
    }

    // Update is called once per frame
    void Update()
    {
        // Retrieve all the player input so it can be placed in a struct and be used by other scripts
        Vector2 mouseLoc = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 mousePos = Input.mousePosition;
        bool leftMouseButtonInput = Input.GetButtonDown("LMB");
        bool rightMouseButtonInput = Input.GetButtonDown("RMB");
        //bool rightMouseButtonInput = Input.GetKeyDown(KeyCode.Mouse1);
        bool spell_1Input = Input.GetKeyDown(KeyCode.Q);
        bool spell_2Input = Input.GetKeyDown(KeyCode.W);
        bool spell_3Input = Input.GetKeyDown(KeyCode.E);
        bool spell_4Input = Input.GetKeyDown(KeyCode.R);

        bool stopMovingInput = Input.GetKeyDown(KeyCode.S);

        if (Input.GetAxisRaw("LMB") > 0)
        {
            leftMouseButtonInput = true;
        }
        if (Input.GetAxisRaw("RMB") > 0)
        {
            rightMouseButtonInput = true;
        }

        if (!inputEnabled)
        {
            leftMouseButtonInput = false;
            rightMouseButtonInput = false;
            spell_1Input = false;
            spell_2Input = false;
            spell_3Input = false;
            spell_4Input = false;
        }


        Current = new PlayerInput()
        {

            MouseLoc = mouseLoc,
            MousePos = mousePos,
            LeftMouseButtonInput = leftMouseButtonInput,
            RightMouseButtonInput = rightMouseButtonInput,
            Spell_1Input = spell_1Input,
            Spell_2Input = spell_2Input,
            Spell_3Input = spell_3Input,
            Spell_4Input = spell_4Input,
            StopMovingInput = stopMovingInput
        };
    }
}

// Make a struct with all the input which can be requested by other scripts to work with
public struct PlayerInput
{
    public Vector2 MouseLoc;
    public Vector3 MousePos;
    public bool LeftMouseButtonInput;
    public bool RightMouseButtonInput;
    public bool Spell_1Input;
    public bool Spell_2Input;
    public bool Spell_3Input;
    public bool Spell_4Input;
    public bool StopMovingInput;

}
