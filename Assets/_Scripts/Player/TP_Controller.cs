using UnityEngine;
using System.Collections;

public class TP_Controller : MonoBehaviour {
	/* (Basically is the "brain" of the 3rd person contoller)
	* Processes player inputs
	* Motion inputs into 3D Vector (move vector)
	* Initial setup check (look for camera) --if camera is gone, then stop working
	*/

	public static CharacterController _characterController;
	public static TP_Controller _instance;

	public float deadZone = 0.1f; // holds dead space (AKA responsiveness to input)
    

    void Awake() 
	{
		_characterController = GetComponent<CharacterController>();
		_instance = this;

		// Use Exsiting Or Create New Main Camera
		TP_Camera.UseExisitingOrCreateNewMainCamera();

    }

	void Update() 
	{
		// Check if main camera exists
		if (Camera.main == null) {
			return;
		}

		GetLocomotionInput ();
        HandleCameraInput();

        switch (TP_Status._instance.interactionState) {
            case InteractionState.Shop:
                HandleShopInput();
                break;
            case InteractionState.Construct:
                HandleConstructionInput();
                break;
            default:
                HandleActionInput();
                break;
        }

		// Tell TP_Motor to update
		TP_Motor._instance.UpdateMotor ();
	}


	// Gets user input for moving the character
	void GetLocomotionInput()
	{
		// Set verticalVelocity
		TP_Motor._instance.VerticalVelocity = TP_Motor._instance.MoveVector.y;

		// Zero out (reset) move vector
		TP_Motor._instance.MoveVector = Vector3.zero;

		// Verify that vertical motion is outside of dead space
		if (Input.GetAxis("Vertical") > deadZone || Input.GetAxis("Vertical") < deadZone)
		{
			// Apply vertical axis to the Z-Axis of MoveVector
			TP_Motor._instance.MoveVector += new Vector3(0, 0, Input.GetAxis("Vertical"));
		}
		
		// Horizontal input to X axis
		if (Input.GetAxis("Horizontal") > deadZone || Input.GetAxis("Horizontal") < deadZone)
		{
			TP_Motor._instance.MoveVector += new Vector3(Input.GetAxis("Horizontal"), 0, 0);

		}

		TP_Animator._instance.DetermineCurrentMoveDirection();


		// Rotate charcater based on direction user is inputting
		var characterRotation = Vector3.zero;
		switch (TP_Animator._instance.MoveDirection)
		{
			case TP_Animator.Direction.Forward:
				characterRotation += new Vector3(0,90,0);
				transform.rotation = Quaternion.Euler(characterRotation);
				break;
			case TP_Animator.Direction.Backward:
				break;
			case TP_Animator.Direction.Left:
				break;
			case TP_Animator.Direction.Right:
				break;
			default:
				break;
		}

	}
	
    // Gets user input for the character action
    void HandleActionInput()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.K) || Input.GetButton("XboxA")) //Input.GetButton("Jump") makes Y be a jump :(
        {
            Jump();
        }
        if ((Input.GetKey(KeyCode.L) || Input.GetButton("XboxB")) && TP_Status._instance.flamethrowerObj.GetComponent<Flamethrower>().level > 0)
        {
            // Use/Hold down for flamethrower
            ActivateFlamethrower(true);
        }
        if ((Input.GetKeyUp(KeyCode.L) || Input.GetButtonUp("XboxB")) && TP_Status._instance.flamethrowerObj.GetComponent<Flamethrower>().level > 0)
        {
            // Stop flamethrower
            ActivateFlamethrower(false);
        }
        if (Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("XboxX"))
        {
            // Use rod
            ActivateRod();
        }
        if ((Input.GetKeyDown(KeyCode.I) || Input.GetButtonDown("XboxY")) && TP_Status._instance.isInRangeOfRunestone)
        {
            // Activate Shop InteractionState
            TP_Status._instance.interactionState = InteractionState.Shop;
            TP_Status._instance.currRunestoneInRange.GetComponent<Runestone>().SetShopText(true);
        }
    }

    void HandleShopInput()
    {
        if (Input.GetKeyDown(KeyCode.K) || Input.GetButtonDown("XboxA"))
        {
            // Continue text
        }
        if ((Input.GetKeyDown(KeyCode.L) || Input.GetButtonDown("XboxB")))
        {
            // Buy Construction
            TP_Status._instance.currRunestoneInRange.GetComponent<Runestone>().BuyConstruction();
            TP_Status._instance.interactionState = InteractionState.Construct;
            TP_Status._instance.currRunestoneInRange.GetComponent<Runestone>().SetInteractText(true);
            TP_Status._instance.currRunestoneInRange.GetComponent<Runestone>().SetShopText(false);
        }
        if (Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("XboxX"))
        {
            // Buy Spell
            TP_Status._instance.currRunestoneInRange.GetComponent<Runestone>().BuySpell();
        }
        if ((Input.GetKeyDown(KeyCode.I) || Input.GetButtonDown("XboxY")) && TP_Status._instance.isInRangeOfRunestone)
        {
            // Cancel Shop InteractionState
            TP_Status._instance.interactionState = InteractionState.Default;
            TP_Status._instance.currRunestoneInRange.GetComponent<Runestone>().SetInteractText(true);
            TP_Status._instance.currRunestoneInRange.GetComponent<Runestone>().SetShopText(false);
        }
    }
    void HandleConstructionInput()
    {
        if (Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("XboxX"))
        {
            // Drop Construction
            BuilderManager._instance.dropCurItem();
            // Reset interactionState
            TP_Status._instance.interactionState = InteractionState.Default;
            TP_Status._instance.currRunestoneInRange.GetComponent<Runestone>().SetInteractText(true);
            TP_Status._instance.currRunestoneInRange.GetComponent<Runestone>().SetShopText(false);
        }
        //if ((Input.GetKeyDown(KeyCode.L) || Input.GetButtonDown("XboxB")))
        //{
        //    // Cancel Construction
        //}
    }


    // Gets user input for character camera actions
    void HandleCameraInput()
	{
		if (Input.GetButton("Bumper Left"))
		{
			// TODO
			Debug.Log ("TODO: reset camera - see commented out link");
			// http://answers.unity3d.com/questions/405954/3rd-person-free-camera-based-in-3d-buzzs-tutorial.html
			TP_Camera._instance.PutCameraBehindCharacter();
		}
        if (Input.GetAxis("Trigger Left")!=0 || Input.GetKey(KeyCode.O))
		{
			TP_Camera._instance.RotateCameraLeft();
		}
		if (Input.GetAxis("Trigger Right")!=0 || Input.GetKey(KeyCode.U))
		{
			TP_Camera._instance.RotateCameraRight();
		}
	}

	void Jump()
	{
		// Basic action
		TP_Motor._instance.Jump();

		// TODO: Animations

		// TODO: Sound FXs

	}

    void ActivateFlamethrower(bool activate)
    {
        Flamethrower flamethrower = TP_Status._instance.flamethrowerObj.GetComponent<Flamethrower>();
        if (activate && flamethrower.level > 0)
        {
            flamethrower.Activate();
        }
        else
        {
            flamethrower.Disactivate();
        }
    }

    void ActivateRod()
    {
        RodAttack rod = TP_Status._instance.rodObj.GetComponent<RodAttack>();
        rod.ToggleActivatation();
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Target"))
        {
            TP_Status._instance.isInRangeOfRunestone = true;
            TP_Status._instance.currRunestoneInRange = col.gameObject;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Target"))
        {
            TP_Status._instance.isInRangeOfRunestone = false;

            if (TP_Status._instance.interactionState == InteractionState.Shop)
            {
                // Reset InteractionState in case it's set to Shop InteractionState
                TP_Status._instance.interactionState = InteractionState.Default;
                TP_Status._instance.currRunestoneInRange = null;
            }
        }
    }
}
