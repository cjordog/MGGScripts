using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour {

    struct MovementAxes
    {
        public string horizontal;
        public string vertical;

        public MovementAxes(string h, string v)
        {
            horizontal = h;
            vertical = v;
        }

        public static void Swap(ref MovementAxes a, ref MovementAxes b)
        {
            MovementAxes temp = b;
            b = a;
            a = temp;
        }
    }

    struct ActionButtons
    {
        public string action1;
        public string action2;

        public ActionButtons(string a1, string a2)
        {
            action1 = a1;
            action2 = a2;
        }

        public static void Swap(ref ActionButtons a, ref ActionButtons b)
        {
            ActionButtons temp = b;
            b = a;
            a = temp;
        }
    }


    [Tooltip("The \"number\" of the player. " +
        "Represents which controller or keyboard controls to use. " +
        "May not correspond to who is considered \"first player.\"")]
    [Range(1, 4)]
	[SerializeField]
    private int playerNumber = 1;
	public int PlayerNumber {
		get { return playerNumber; }
		set {
			playerNumber = value;
			ResetInputNames();
		}
	}

    [Tooltip("Whether or not this player's input should be gotten " +
        "from a controller")]
	[SerializeField]
    private bool useController = true;
	public bool UseController {
		get { return useController; }
		set {
			useController = value;
			ResetInputNames();
		}
	}

	private int PlayerIndex {
		get { return playerNumber - 1; }
	}

    private MovementAxes movement;
    private ActionButtons actions;


    void Awake()
    {
        ResetInputNames();
    }

    public float GetHorizontal()
    {
        return Input.GetAxisRaw(movement.horizontal);
    }

    public float GetVertical()
    {
        return Input.GetAxisRaw(movement.vertical);
    }

    public bool GetAction1()
	{
        return Input.GetButtonDown(actions.action1);
    }

    public bool GetAction2()
    {
        return Input.GetButtonDown(actions.action2);
    }

    public void ResetInputNames()
    {
		string[] joystickNames = Input.GetJoystickNames();
		if(useController && playerNumber > joystickNames.Length) {
			Debug.LogWarning(joystickNames.Length + " controllers are connected\n" +
				"Cannot use player number " + playerNumber + " with a controller");
			actions.action1 = actions.action2 = "";
			return;
		}

        string suffix = useController ? "-GP" : "";

        movement.horizontal = "LeftHorizontal-" + playerNumber +
            suffix;
        movement.vertical = "LeftVertical-" + playerNumber +
            suffix;

		if(useController && (joystickNames[PlayerIndex] ==
			"Sony Computer Entertainment Wireless Controller" ||
			joystickNames[PlayerIndex] == "Unknown Wireless Controller")) {
			actions.action1 = "Action2-" + playerNumber + suffix;
			actions.action2 = "Action2-" + playerNumber + suffix + "-SONY";
		}
		else {
	        actions.action1 = "Action1-" + playerNumber + suffix;
	        actions.action2 = "Action2-" + playerNumber + suffix;
		}
    }

    private void SwapMovementAxes(ref MovementAxes other)
    {
        MovementAxes.Swap(ref movement, ref other);
    }

    private void SwapActionButtons(ref ActionButtons other)
    {
        ActionButtons.Swap(ref actions, ref other);
    }
}
